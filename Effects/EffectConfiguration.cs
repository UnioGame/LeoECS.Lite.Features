namespace Game.Ecs.Effects
{
    using System;
    using Code.Configuration.Runtime.Effects;
    using Code.Configuration.Runtime.Effects.Abstract;
    using Components;
    using Data;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using Time.Service;
    using UniGame.AddressableTools.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Serialization;

    [Serializable]
    public abstract class EffectConfiguration : IEffectConfiguration
    {
        private const string SpawnParentKey = "spawn parent";
        
        [OnValueChanged("CheckViewLiveTime")]
        [FormerlySerializedAs("_duration")]
        [Min(-1.0f)]
        [SerializeField]
        public float duration;
        [FormerlySerializedAs("_periodicity")]
        [Min(-1.0f)]
        [SerializeField]
        public float periodicity = -1.0f;
        
        [Min(0f)]
        [SerializeField]
        public float delay;
        
        [FormerlySerializedAs("_targetType")] [SerializeField]
        public TargetType targetType = TargetType.Target;

        [FormerlySerializedAs("_viewPrefab")] 
        [SerializeField]
        public GameObject viewPrefab;

        public AssetReferenceGameObject view;
        
        [OnValueChanged("CheckViewLiveTime")]
        public bool trimToDuration;
        private void CheckViewLiveTime()
        {
            if (!trimToDuration) return;
            viewLifeTime = duration;
        }
        
        [HideIf("trimToDuration")]
        [FormerlySerializedAs("_viewLifeTime")]
        [Min(-1.0f)]
        [SerializeField]
        public float viewLifeTime;

        [TitleGroup(SpawnParentKey)]
        public bool spawnAtRoot;
        
        [TitleGroup(SpawnParentKey)]
        [FormerlySerializedAs("_viewInstanceType")] 
        [SerializeField]
        [HideIf(nameof(spawnAtRoot))]
        public ViewInstanceType viewInstanceType = ViewInstanceType.Body;

        [TitleGroup(SpawnParentKey)]
        [ShowIf(nameof(spawnAtRoot))]
        public EffectRootId effectRoot;
        
        
        [Tooltip("Если true, то визуал эффекта привязывается к источнику. " +
                 "Т.е. то, что создало эффект. Умрёт источник - умрёт и эффект. Если не истекло время жизни.\n" +
                 "Если false, то визуал эффекта привязывается к цели. " +
                 "Т.е. тому, на кого наложили эффект. Умрёт цель - умрёт и эффект. Если не истекло время жизни.")]
        public bool attachToSource;

        public TargetType TargetType => targetType;

        public void ComposeEntity(EcsWorld world, int effectEntity)
        {
            var delayedPool = world.GetPool<DelayedEffectComponent>();
            if (delay > 0f && !delayedPool.Has(effectEntity))
            {
                ref var delayed = ref delayedPool.Add(effectEntity);
                delayed.Delay = delay;
                delayed.LastApplyingTime = GameTime.Time;
                delayed.Configuration = this;
                return;
            }
            var durationPool = world.GetPool<EffectDurationComponent>();
            
            ref var durationComponent = ref durationPool.Add(effectEntity);
            durationComponent.Duration = this.duration;
            durationComponent.CreatingTime = Time.time;

            var periodicityPool = world.GetPool<EffectPeriodicityComponent>();
            ref var periodicityComponent = ref periodicityPool.Add(effectEntity);
            periodicityComponent.Periodicity = this.periodicity;
            var isValidEffectView = view != null && view.RuntimeKeyIsValid() || viewPrefab!=null;
 
            if (isValidEffectView)
            {
                ref var effectViewComponent = ref world.AddComponent<EffectViewDataComponent>(effectEntity);
                effectViewComponent.View = view;
                effectViewComponent.ViewPrefab = viewPrefab;
                effectViewComponent.LifeTime = viewLifeTime;
                effectViewComponent.ViewInstanceType = viewInstanceType;
                effectViewComponent.AttachToSource = attachToSource;
                effectViewComponent.UseEffectRoot = spawnAtRoot;
                effectViewComponent.EffectRoot = effectRoot;
            }

            if (spawnAtRoot)
            {
                ref var rootTargetComponent = ref world.AddComponent<EffectRootIdComponent>(effectEntity);
                rootTargetComponent.Value = effectRoot;
            }

            Compose(world, effectEntity);
        }

        protected abstract void Compose(EcsWorld world, int effectEntity);
    }
}