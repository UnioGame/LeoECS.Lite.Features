namespace Game.Ecs.AI.Data
{
    public static class AiConstants
    {
        public const int PriorityNever = -1;

        public static readonly AiPlannerData Never = new AiPlannerData()
        {
            Priority = PriorityNever
        };
        
    }
}