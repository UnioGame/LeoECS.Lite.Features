namespace Game.Ecs.Gameplay.ChampionHealingFeature.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Effects;
    using Effects.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;
    using UnityEngine.Serialization;

    /// <summary>
    /// apply effect on convert
    /// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class InitializeEffectConverter : GameObjectConverter
    {
        [FormerlySerializedAs("effectConfiguration")]
        [SerializeReference]
        public List<EffectConfiguration> effectConfigurations;
        
        protected override void OnApply(GameObject target, EcsWorld world, int entity)
        {
            foreach (var effectConfiguration in effectConfigurations)
            {
                var effectEntity = world.NewEntity();
                ref var effectComponent = ref world.GetOrAddComponent<EffectComponent>(effectEntity);
                ref var applyEffectRequest = ref world.GetOrAddComponent<ApplyEffectSelfRequest>(effectEntity);

                effectComponent.Destination = world.PackEntity(entity);
                effectComponent.Source = world.PackEntity(entity);
                
                effectConfiguration.ComposeEntity(world,effectEntity);
            }
        }
    }
}