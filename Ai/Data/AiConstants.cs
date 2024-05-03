namespace Game.Ecs.AI.Data
{
    public static class AiConstants
    {
        //Priority
        public const int PriorityNever = -1;

        public static readonly AiPlannerData Never = new AiPlannerData()
        {
            Priority = PriorityNever
        };
        
    }
}