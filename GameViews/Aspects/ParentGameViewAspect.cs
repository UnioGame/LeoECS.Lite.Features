namespace Game.Ecs.Gameplay.LevelProgress.Aspects
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Components;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class ParentGameViewAspect : EcsAspect
    {
        public EcsPool<GameViewParentComponent> Parent;
        public EcsPool<ActiveGameViewComponent> ActiveView;
        public EcsPool<GameObjectComponent> View;
        
        //requests
        public EcsPool<ActivateGameViewRequest> Activate;
        public EcsPool<DisableActiveGameViewRequest> Disable;
    }
}