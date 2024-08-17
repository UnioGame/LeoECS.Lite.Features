namespace Game.Ecs.Movement.Converters
{
    using System;
    using Characteristics.Base.Components.Requests.OwnerRequests;
    using Characteristics.Speed.Components;
    using Code.Configuration.Runtime.Entity.Movement;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public sealed class MovementTweenAgentConverter : LeoEcsConverter
    {
        [InlineProperty]
        [HideLabel]
        public MovementData movementData = new();

        public int trackId;
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var transformPositionComponent = ref world.GetOrAddComponent<TransformPositionComponent>(entity);
            ref var directionComponent = ref world.GetOrAddComponent<TransformDirectionComponent>(entity);
            ref var scaleComponent = ref world.GetOrAddComponent<TransformScaleComponent>(entity);
            ref var rotationComponent = ref world.GetOrAddComponent<TransformRotationComponent>(entity);
            ref var tweenData = ref world.GetOrAddComponent<MovementTweenDataComponent>(entity);
            ref var movementAgent = ref world.GetOrAddComponent<MovementAgentComponent>(entity);
            ref var tweenAgent = ref world.GetOrAddComponent<MovementTweenAgentComponent>(entity);
            
            tweenAgent.TrackId = trackId;
            tweenAgent.Track = default;
            
            var requestEntity = world.NewEntity();
            ref var speedRequestComponent = ref world
                .GetOrAddComponent<ChangeCharacteristicBaseRequest<SpeedComponent>>(requestEntity);
            
            speedRequestComponent.Value = movementData.speed;
            speedRequestComponent.Target = world.PackEntity(entity);
            speedRequestComponent.Source = world.PackEntity(entity);

            ref var rotationSpeedComponent = ref world.GetOrAddComponent<AngularSpeedComponent>(entity);
            rotationSpeedComponent.Value = movementData.angularSpeed;
            
            ref var instantRotateComponentComponent = ref world.GetOrAddComponent<InstantRotateComponent>(entity);
            instantRotateComponentComponent.Value = movementData.instantRotation;
            
            ref var stepComponent  = ref world.GetOrAddComponent<StepMovementComponent>(entity);
            stepComponent.Value = movementData.navMeshStep;

            ref var animationInfo = ref world.GetOrAddComponent<MovementAnimationInfoComponent>(entity);
            animationInfo.RunSpeed = movementData.animationRunSpeed;
            animationInfo.MaxRunSpeed = movementData.maxAnimationRunSpeed;
            
            var transform = target.transform;
            
            transformPositionComponent.Position = transform.position;
            directionComponent.Forward = transform.forward;
            directionComponent.Up = transform.up;
            directionComponent.Right = transform.right;
            scaleComponent.Scale = transform.lossyScale;
            scaleComponent.LocalScale = transform.localScale;
            rotationComponent.Quaternion = transform.rotation;
            rotationComponent.LocalRotation = transform.localRotation;
            rotationComponent.Euler = transform.eulerAngles;
        }
        
        
    }
}