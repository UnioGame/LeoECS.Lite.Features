namespace Ability.Test
{
    using Game.Ecs.Ability.Common.Components;
    using Game.Ecs.Animations.Components.Requests;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;
    
    public class AnimationEventTest : MonoBehaviour
    {
        private int _parentEntityId = -1;
        private EcsWorld _world;
        private EcsPool<AnimationTriggerRequest> _animationEventPool;
        private EcsPool<AbilityInHandLinkComponent> _abilityInHandLinkPool;
        private bool _initializedFlag = false;
        
        public void OnAnimationEvent()
        {
            if (!_initializedFlag)
            {
                _parentEntityId = gameObject.GetParentEntity();
                if(_parentEntityId == -1) return;
                _world = LeoEcsGlobalData.World;
                _abilityInHandLinkPool ??= _world.GetPool<AbilityInHandLinkComponent>();
                _animationEventPool ??= _world.GetPool<AnimationTriggerRequest>();
                _initializedFlag = true;
            }
            var abilityInHandLinkComponent = _abilityInHandLinkPool.Get(_parentEntityId);
            if(!abilityInHandLinkComponent.AbilityEntity.Unpack(_world, out var abilityEntity)) return;
            _animationEventPool.Add(abilityEntity);
        }
    }
}