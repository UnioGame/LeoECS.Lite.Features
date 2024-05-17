namespace Game.Ecs.AI.Data
{
    public static class AiConstants
    {
        public const int PriorityNever = -1;
        public const float MoveDistanceFault = 0.9f;

        public static readonly AiPlannerData Never = new AiPlannerData()
        {
            Priority = PriorityNever
        };
        
    }
}