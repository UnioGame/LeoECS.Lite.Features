namespace Game.Ecs.AI.Abstract
{
    using Data;

    public interface IAiPlannerData
    {
        ref AiPlannerData PlannerData { get; }
    }
}