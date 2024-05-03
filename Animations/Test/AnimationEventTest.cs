namespace Animations.Test
{
    using Game.Ecs.Animations.Components.Requests;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public class AnimationEventTest : MonoBehaviour
    {
        private int _parentEntityId = -1;
        private EcsWorld _world;
        private EcsPool<AnimationTriggerRequest> _animationEventPool;
        // private EcsPool<AbilityInHandLinkComponent> _abilityInHandLinkPool;
        
        private void Start()
        {
            _parentEntityId = gameObject.GetParentEntity();
            _world = LeoEcsGlobalData.World;
        }

        public void OnAnimationEvent()
        {
            if (_parentEntityId == -1) 
                return;
            _world.GetComponent<AbilityInHandLinkComponent>(_parentEntityId);


            // var ecsPool = _animationEventPool.Add(abilityEnitty);
            // ref var animationEvent = ref ecsPool.GetOrAddComponent(entity);
        }
    }
}