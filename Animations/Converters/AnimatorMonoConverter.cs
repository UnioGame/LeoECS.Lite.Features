namespace Animations.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Animations.AnimationData.Data;
    using Game.Code.Animations;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEditor.Animations;
    using UnityEngine;
#if UNITY_EDITOR
#endif

    public sealed class AnimatorMonoConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        private Animator _animator;

        #region editor

        [Button]
        public void UpdateAnimatorStates()
        {
#if UNITY_EDITOR
            AnimationMap.Animations.Clear();
            Debug.Log($"Animator {_animator.name}");
            var animatorRuntimeAnimatorController = _animator.runtimeAnimatorController as AnimatorController;
            for (int i = 0; i < _animator.layerCount; i++)
            {
                var layerName = _animator.GetLayerName(i);
                var animationsNames = animatorRuntimeAnimatorController.layers[i].stateMachine.states.Select(x => x.state.name);
                foreach (var name in animationsNames)
                {
                    AnimationMap.Animations.Add(new AnimationKeyValuePair
                    {
                        key = name
                    });
                }
                
            }
#endif
        }
        public AnimationMap AnimationMap;        

        #endregion
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var animatorPool = world.GetPool<AnimatorComponent>();

            ref var animator = ref animatorPool.GetOrAddComponent(entity);
            animator.Value = _animator;
        }
    }

    [Serializable]
    public struct AnimationMap
    {
        public List<AnimationKeyValuePair> Animations;
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