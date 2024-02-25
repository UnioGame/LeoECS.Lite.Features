namespace Game.Ecs.Input
{
    using Leopotam.EcsLite;

    public static class InputHelper
    {
        public static void ProcessBeginUserInput<T>(EcsWorld world, EcsFilter filter, ref bool state) where T : struct
        {
            if(state)
                return;

            state = true;
            
            var beginUserInputPool = world.GetPool<T>();

            foreach (var entity in filter)
            {
                beginUserInputPool.Add(entity);
            }
        }
        
        public static void ProcessEndUserInput<T>(EcsWorld world, EcsFilter filter, ref bool state) where T : struct
        {
            if (!state)
                return;
            
            state = false;

            var endUserInputPool = world.GetPool<T>();

            foreach (var entity in filter)
            {
                endUserInputPool.Add(entity);
            }
        }
    }
}