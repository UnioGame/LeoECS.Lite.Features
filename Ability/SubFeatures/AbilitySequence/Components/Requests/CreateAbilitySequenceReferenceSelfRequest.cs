namespace Game.Ecs.Ability.SubFeatures.AbilitySequence
{
    using System;
    using System.Collections.Generic;
    using Data;
    using Game.Code.Configuration.Runtime.Ability;
    using Leopotam.EcsLite;

    /// <summary>
    /// create new ability sequence
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CreateAbilitySequenceReferenceSelfRequest : IEcsAutoReset<CreateAbilitySequenceReferenceSelfRequest>
    {
        public EcsPackedEntity Owner;
        public AbilitySequenceReference Reference;
        
        public void AutoReset(ref CreateAbilitySequenceReferenceSelfRequest c)
        {
            c.Owner = default;
            c.Reference = null;
        }
    }
    
    /// <summary>
    /// create new ability sequence
    /// </summary>
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct CreateAbilitySequenceSelfRequest : IEcsAutoReset<CreateAbilitySequenceSelfRequest>
    {
        public string Name;
        public EcsPackedEntity Owner;
        public List<int> Abilities;
        
        public void AutoReset(ref CreateAbilitySequenceSelfRequest c)
        {
            c.Abilities ??= new List<int>();
            c.Abilities.Clear();
            c.Name = string.Empty;
        }
    }
}