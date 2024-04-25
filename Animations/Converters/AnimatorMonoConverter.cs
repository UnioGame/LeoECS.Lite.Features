namespace Animations.Converters
{
    using System;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
#if UNITY_EDITOR
#endif

    public sealed class AnimatorMonoConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        public Animator animator;
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var animatorPool = world.GetPool<AnimatorComponent>();

            ref var animator = ref animatorPool.GetOrAddComponent(entity);
            animator.Value = this.animator;
        }
    }


    [Serializable]
    public sealed class AnimatorConverter : LeoEcsConverter,IConverterEntityDestroyHandler
    {
        [SerializeField]
        public Animator animator;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var animatorPool = world.GetPool<AnimatorComponent>();

            ref var animatorComponent = ref animatorPool.GetOrAddComponent(entity);
            animatorComponent.Value = animator;
        }

        public void OnEntityDestroy(EcsWorld world, int entity)
        {
            world.TryRemoveComponent<AnimatorComponent>(entity);
        }
    }
}