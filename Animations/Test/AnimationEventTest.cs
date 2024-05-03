namespace Animations.Test
{
    using Components.Requests;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    public class AnimationEventTest : MonoBehaviour
    {
        public void OnAnimationEvent()
        {
            if (!gameObject.TryGetParentEntity(out var entity))
            {
#if UNITY_EDITOR
                Debug.LogError($"Parent Entity not found for {gameObject.name}");
#endif
                return;
            }

            var ecsPool = LeoEcsGlobalData.World.GetPool<AnimationMilestoneComposeComponent>();
            ref var animationEvent = ref ecsPool.GetOrAddComponent(entity);
        }
    }
}