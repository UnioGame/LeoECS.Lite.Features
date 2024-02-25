namespace Game.Ecs.GameResources.Aspects
{
    using System;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using Modules.UnioModules.UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;

    [Serializable]
    public class GameResourceTaskAspect : EcsAspect
    {
        public EcsPool<GameResourceHandleComponent> Handle;
        public EcsPool<GameResourceTaskComponent> Task;
        public EcsPool<GameResourceResultComponent> Result;
        public EcsPool<TransformParentComponent> Parent;
        public EcsPool<ParentEntityComponent> ParentEntity;
        public EcsPool<PositionComponent> Position;
        public EcsPool<RotationComponent> Rotation;
        public EcsPool<ScaleComponent> Scale;
        public EcsPool<EntityComponent> Target;
        
        //optional
        public EcsPool<GameObjectComponent> GameObject;
        public EcsPool<GameResourceTaskCompleteComponent> Complete;
        
        //event
        public EcsPool<GameResourceTaskCompleteSelfEvent> CompleteEvent;
        
        //requests
        public EcsPool<GameResourceSpawnRequest> SpawnRequest;
    }
}