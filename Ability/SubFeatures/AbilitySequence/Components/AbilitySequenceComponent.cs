namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Components
{
    using System;
    using System.Collections.Generic;
    using Leopotam.EcsLite;

    /// <summary>
    /// data of ability sequence
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct AbilitySequenceComponent : IEcsAutoReset<AbilitySequenceComponent>
    {
        public int Id;
        public List<int> Abilities;
        public int Index;
        public int NextAbilityIndex;
        public int ActiveAbility;
        
        public void AutoReset(ref AbilitySequenceComponent c)
        {
            c.Abilities ??= new List<int>();
            c.Abilities.Clear();
            c.Index = 0;
            c.ActiveAbility = -1;
            c.NextAbilityIndex = 0;
        }
    }
}