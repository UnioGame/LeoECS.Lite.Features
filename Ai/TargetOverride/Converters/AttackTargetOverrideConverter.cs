namespace Game.Ecs.AI.TargetOverride.Converters
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public class AttackTargetOverrideConverter : ITargetOverrideConverter
    {
        public void Apply(EcsWorld world, int entity)
        {
            world.AddComponent<OnDamageTargetOverrideComponent>(entity);
        }
    }
}