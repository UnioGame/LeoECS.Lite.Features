namespace Game.Ecs.Gameplay.Death.Aspects
{
    using System;
    using Characteristics.Health.Components;
    using Core.Components;
    using Core.Death.Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

    [Serializable]
    public class DeathAspect : EcsAspect
    {
        public EcsPool<PrepareToDeathComponent> PrepareToDeath;
        public EcsPool<PlayableDirectorComponent> Director;
        public EcsPool<DeadAnimationEvaluateComponent> Evaluate;
        public EcsPool<DeathAnimationComponent> Animation;
        public EcsPool<AwaitDeathCompleteComponent> AwaitDeath;
        public EcsPool<DisabledComponent> Disabled;
        public EcsPool<DeathCompletedComponent> Completed;
        public EcsPool<ChampionComponent> Champion;
    }
}