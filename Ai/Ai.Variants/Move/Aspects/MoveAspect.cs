using Game.Ecs.GameAi.Move.Components;
using Leopotam.EcsLite;

namespace Ai.Ai.Variants.MoveToTarget.Aspects
{
    using System;
    using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;
    
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public class MoveAspect : EcsAspect
    {
        public EcsPool<MovePlannerComponent> Planner;
        public EcsPool<MoveActionComponent> MoveAction;
        public EcsPool<MoveToTargetComponent> Target;

        public EcsPool<MoveToDefaultTargetComponent> DefaultTarget;
        public EcsPool<MoveToChaseTargetComponent> ChaseTarget;
        public EcsPool<MoveToSpawnPositionComponent> SpawnPosition;
    }
}