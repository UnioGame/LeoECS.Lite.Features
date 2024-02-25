namespace Game.Ecs.GameResources.Aspects
{
    using System;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using UnityEngine;

    [Serializable]
    public class GameResourceAspect : EcsAspect
    {
        public EcsPool<GameSpawnedResourceComponent> SpawnedResource;
        public EcsPool<GameResourceIdComponent> Resource;
        public EcsPool<UnityObjectComponent> Object;
        public EcsPool<GameSpawnCompleteComponent> Complete;
        public EcsPool<GameResourceSourceLinkComponent> SourceLink;

        //optional
        public EcsPool<GameObjectComponent> GameObject;
        public EcsPool<OwnerComponent> Owner;
        public EcsPool<ParentEntityComponent> Parent;
        public EcsPool<GameResourceSelfTargetComponent> Target;
        
        //requests
        public EcsPool<GameResourceSpawnRequest> Spawn;
    }
}