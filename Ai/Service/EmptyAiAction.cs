namespace Game.Ecs.AI.Service
{
    using System;
    using Abstract;
    using Leopotam.EcsLite;

    [Serializable]
    public sealed class EmptyAiAction : IAiAction
    {
        public static EmptyAiAction EmptyAction = new EmptyAiAction();
        
        public static AiActionResult EmptyAiActionResult = new AiActionResult();

        public AiActionResult Execute(EcsSystems systems, int entity)
        {
            return new AiActionResult() { ActionStatus = AiActionStatus.Complete };
        }
    }
}