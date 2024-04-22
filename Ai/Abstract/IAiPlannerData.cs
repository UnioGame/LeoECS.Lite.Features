namespace Game.Ecs.AI.Abstract
{
    using Service;

    public interface IAiPlannerData
    {
        ref AiPlannerData PlannerData { get; }
    }
}