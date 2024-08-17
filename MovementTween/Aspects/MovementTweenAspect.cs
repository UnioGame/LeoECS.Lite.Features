namespace Game.Ecs.Movement.Aspects
{
    using System;
    using Characteristics.Speed.Components;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Components;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class MovementTweenAspect : EcsAspect
    {
        public MovementTweenTrackAspect Track;
        
        public EcsPool<MovementTweenAgentComponent> Agent;
        public EcsPool<MovementTweenDataComponent> Tween;
        public EcsPool<TransformComponent> Transform;
        public EcsPool<TransformPositionComponent> TransformPosition;
        public EcsPool<SpeedComponent> Speed;
        public EcsPool<TransformPositionComponent> Position;
        
        //requests
        public EcsPool<RotateToPointSelfRequest> RotateTo;
    }
}