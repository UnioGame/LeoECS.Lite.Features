namespace Game.Ecs.Movement
{
    using Systems;
    using Systems.Converters;
    using Systems.NavMesh;
    using Systems.Transform;
    using Components;
    using Core.Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;
    
    [CreateAssetMenu(menuName = "Game/Feature/Movement/NavMesh Movement Feature", fileName = "NavMesh Movement Feature")]
    public sealed class NavMeshMovementFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new StopNavMeshAgentSystem());
            ecsSystems.Add(new ProcessImmobilityStatusSystem());
            
            ecsSystems.DelHere<VelocityComponent>();
            ecsSystems.Add(new DirectionVelocityConvertSystem());
            
            ecsSystems.Add(new VelocityNavMeshTargetConvertSystem());

            ecsSystems.Add(new NavMeshTargetMovementSystem());
            ecsSystems.DelHere<MovementPointRequest>();
            
            ecsSystems.Add(new NavMeshMovementSystem());

            ecsSystems.Add(new EndInputStopNavMeshAgentConvertSystem());

            ecsSystems.Add(new RotationToPointSystem());
            ecsSystems.DelHere<RotateToPointSelfRequest>();

            ecsSystems.Add(new ComeToTheEndOfSystem());
            ecsSystems.Add(new RevokeComeToTheEndOfSystem());
            ecsSystems.DelHere<RevokeComeToEndOfRequest>();
            
            ecsSystems.Add(new NavMeshAgentStopSimulationSystem());
            ecsSystems.DelHere<MovementStopRequest>();

            ecsSystems.Add(new DisableNavMeshAgentSystem());
            ecsSystems.Add(new SetNavigationStatusByRequestSystem());
            ecsSystems.Add(new MakeKinematicByRequestSystem());
            
            return UniTask.CompletedTask;
        }
    }
}