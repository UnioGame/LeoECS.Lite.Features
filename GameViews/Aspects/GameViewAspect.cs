namespace Game.Ecs.Gameplay.LevelProgress.Aspects
{
    using System;
    using Components;
    using Core.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class GameViewAspect : EcsAspect
    {
        public EcsPool<GameViewComponent> View;
        public EcsPool<GameObjectComponent> GameObject;
        public EcsPool<OwnerComponent> Owner;
    }
}