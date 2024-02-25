namespace Game.Ecs.Animations.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Components.Requests;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.AddressableTools.Runtime;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.Shared.Runtime.Timeline;
    using UniModules.UniGame.Core.Runtime.DataFlow.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Playables;
    using UnityEngine.Serialization;
    using UnityEngine.Timeline;

    public sealed class SpawnAnimationMonoConverter : MonoLeoEcsConverter
    {
        [FormerlySerializedAs("_spawnAnimation")] 
        [SerializeField]
        public PlayableAsset spawnAnimation;

        [SerializeField]
        public PlayableDirector playableDirector;
        
        [FormerlySerializedAs("_disableParts")] 
        [SerializeField]
        public bool disableParts = true;
        
        // TODO: надо будет продумать момент анимации, сейчас она запускается через n кадров, что дает эффект "мигания"
        [FormerlySerializedAs("_disabledParts")]
        [Required]
        [SerializeField]
        public GameObject[] disabledParts;
        
        [FormerlySerializedAs("_playOnConvert")] 
        [SerializeField]
        public bool playOnConvert = true;
        
        private void Awake()
        {
            if(!disableParts || IsEnabled == false) return;
            
            foreach (var disabledPart in disabledParts)
            {
#if UNITY_EDITOR
                if (disabledPart == null)
                {
                    Debug.LogError($"PREFAB: {name} DISABLED PART IN NULL");
                    continue;
                }
#endif
                disabledPart.SetActive(false);
            }
        }

        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            if (!playOnConvert) return;
 
            var animationCreateRequest = world.NewEntity();
            ref var createAnimationRequest = ref world.AddComponent<CreateAnimationPlayableSelfRequest>(animationCreateRequest);
            ref var playAnimationSelfRequest = ref world.AddComponent<PlayAnimationSelfRequest>(animationCreateRequest);
            
            createAnimationRequest.Animation = spawnAnimation;
            createAnimationRequest.Owner = world.PackEntity(entity);
            createAnimationRequest.Target = world.PackEntity(entity);
            createAnimationRequest.WrapMode = DirectorWrapMode.None;
            createAnimationRequest.Duration = 0;
            createAnimationRequest.BindingData = null;

            ActivateAfterSpawn((float)spawnAnimation.duration).Forget();
        }
        
        private async UniTask ActivateAfterSpawn(float delay)
        {
            return;
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            foreach (var disabledPart in disabledParts)
            {
                if (disabledPart == null)
                    continue;
                disabledPart.SetActive(true);
            }
        }

        [Button]
        public void Test()
        {
            // Get the PlayableGraph from the PlayableDirector
            playableDirector.playableAsset = spawnAnimation;
            playableDirector.RebuildGraph();
            
            var binding = playableDirector.GetGenericBinding(spawnAnimation);
            var graph = playableDirector.playableGraph;
            var animator = gameObject.GetComponent<Animator>();

            var track = spawnAnimation.GetTrack<AnimationTrack>();
            playableDirector.SetGenericBinding(track,animator);
        }
    }
    
    [Serializable]
    public sealed class SpawnAnimationConverter : LeoEcsConverter
    {
        [SerializeField]
        public AssetReferenceT<PlayableAsset> spawnAnimation;

        [Required]
        [SerializeField]
        private GameObject[] disabledParts;
        
        [SerializeField]
        private bool playOnConvert = true;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            foreach (var disabledPart in disabledParts)
            {
#if UNITY_EDITOR
                if (disabledPart == null)
                {
                    Debug.LogError($"PREFAB: {target.name} DISABLED PART IN NULL",target);
                    continue;
                }
#endif
                disabledPart.SetActive(false);
            }
            
            if (!playOnConvert) return;
            
            ActivateAnimationAsync(target, world, entity).Forget();
        }

        private async UniTask ActivateAnimationAsync(GameObject target, EcsWorld world, int entity)
        {
            var lifeTime = target.GetAssetLifeTime();
            var animation = spawnAnimation
                .LoadAssetForCompletion(lifeTime);
            
            CreateAnimationEntity(animation,target,world,entity);

            await ActivateAfterSpawn((float)animation.duration)
                .AttachExternalCancellation(lifeTime.Token);
        }

        private void CreateAnimationEntity(PlayableAsset animation,GameObject target, EcsWorld world, int entity)
        {
            var animationCreateRequest = world.NewEntity();
            ref var createAnimationRequest = ref world.AddComponent<CreateAnimationPlayableSelfRequest>(animationCreateRequest);
            ref var playAnimationSelfRequest = ref world.AddComponent<PlayAnimationSelfRequest>(animationCreateRequest);
            
            createAnimationRequest.Animation = animation;
            createAnimationRequest.Owner = world.PackEntity(entity);
            createAnimationRequest.Target = world.PackEntity(entity);
            createAnimationRequest.WrapMode = DirectorWrapMode.None;
            createAnimationRequest.Duration = 0;
            createAnimationRequest.BindingData = null;
        }
        
        
        private async UniTask ActivateAfterSpawn(float delay)
        {
            return;
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            foreach (var disabledPart in disabledParts)
            {
                if (disabledPart == null)
                    continue;
                disabledPart.SetActive(true);
            }
        }
    }
}