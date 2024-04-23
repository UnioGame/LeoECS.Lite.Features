namespace Game.Ecs.GameAi.ActivateAbility.Components
{
    using System;
    using Leopotam.EcsLite;
    using TargetSelection;
    /// <summary>
    /// data for ability range action
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilityRangeActionDataComponent : IEcsAutoReset<AbilityRangeActionDataComponent>
    {
        public EcsPackedEntity[] Values;
        public int Count;
        
        public void AutoReset(ref AbilityRangeActionDataComponent c)
        {
            c.Values ??= new EcsPackedEntity[TargetSelectionData.MaxTargets];
            c.Count = 0;
        }
    }
}