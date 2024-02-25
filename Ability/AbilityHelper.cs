namespace Game.Ecs.Ability
{
    using Unity.Mathematics;

    public static class AbilityHelper
    {
        public static float3 FindGravityCenter(float3[] points,int count)
        {
            var centerGravity = float3.zero;

            for (int i = 0; i < count; i++)
                centerGravity += points[i];
            
            return centerGravity / (float)count;
        }
        
    }
}