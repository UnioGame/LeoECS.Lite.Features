namespace Ability.Tools
{
    using Cysharp.Threading.Tasks;
    using Game.Ecs.Ability.Common.Components;
    using Game.Ecs.Animations.Components.Requests;
    using Leopotam.EcsLite;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

    public class AnimationEventToECS : MonoBehaviour
    {
        private int _parentEntityId = -1;
        private EcsWorld _world;
        private EcsPool<AnimationTriggerRequest> _animationEventPool;
        private EcsPool<AbilityInHandLinkComponent> _abilityInHandLinkPool;
        private const float Timeout = 10;
        public async void Awake()
        {
            var time = Time.time;
            await UniTask.WaitUntil(() => LeoEcsGlobalData.World != null || time + Timeout < Time.time)
                .ContinueWith(() => _world = LeoEcsGlobalData.World);
            _abilityInHandLinkPool ??= _world.GetPool<AbilityInHandLinkComponent>();
            _animationEventPool ??= _world.GetPool<AnimationTriggerRequest>(); 
            _parentEntityId = gameObject.GetParentEntity();
        }

        public void OnAnimationEvent()
        {
            //todo вместо кучи if можно смотреть по какой именно причине мы завершили таску WaitUntil в Awake
            if (_world == null)
            {
                GameLog.LogWarning("No world found for animation event");
                return;
            }
            if (_parentEntityId == -1)
            {
                _parentEntityId = gameObject.GetParentEntity();
                if (_parentEntityId == -1)
                {
                    GameLog.LogWarning("No parent entity found for animation event");
                    return;
                }
            }
            var abilityInHandLinkComponent = _abilityInHandLinkPool.Get(_parentEntityId);
            if(!abilityInHandLinkComponent.AbilityEntity.Unpack(_world, out var abilityEntity)) return;
            _animationEventPool.Add(abilityEntity);
        }
    }
}