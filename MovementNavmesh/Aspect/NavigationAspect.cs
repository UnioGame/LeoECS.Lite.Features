﻿namespace Game.Ecs.Movement.Aspect
{
    using System;
    using Characteristics.Speed.Components;
    using Components;
    using Core.Components;
    using Gameplay.LevelProgress.Components;
    using Input.Components.Direction;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class NavigationAspect : EcsAspect
    {
        public EcsPool<NavMeshAgentComponent> Agent;
        public EcsPool<RigidbodyComponent> Rigidbody;
        public EcsPool<TransformComponent> Transform;
        public EcsPool<TransformPositionComponent> Position;
        public EcsPool<ActiveGameViewComponent> ActiveGameView;
        public EcsPool<VelocityComponent> Velocity;
        public EcsPool<SpeedComponent> Speed;
        public EcsPool<AngularSpeedComponent> RotationSpeed;
        public EcsPool<MovementStopRequest> NavMeshAgentStop;
        public EcsPool<ImmobilityComponent> Immobility;
        public EcsPool<DirectionInputEvent> Direction;
        public EcsPool<StepMovementComponent> StepMovement;
        public EcsPool<MovementPointRequest> MovementTargetPoint;
        public EcsPool<ComePointComponent> ComePoint;
        public EcsPool<InstantRotateComponent> InstantRotate;

        //requests
        public EcsPool<RotateToPointSelfRequest> RotateTo;
        public EcsPool<SetNavigationStatusSelfRequest> SetNavigationStatus;
        public EcsPool<SetKinematicSelfRequest> SetKinematicStatus;
        


    }
}