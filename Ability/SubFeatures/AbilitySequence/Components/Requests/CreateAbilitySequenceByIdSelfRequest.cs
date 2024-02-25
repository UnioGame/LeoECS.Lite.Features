namespace Game.Ecs.Ability.SubFeatures.AbilitySequence
{
    using System;
    using System.Collections.Generic;
    using Game.Code.Configuration.Runtime.Ability.Description;
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
    public struct CreateAbilitySequenceByIdSelfRequest : IEcsAutoReset<CreateAbilitySequenceByIdSelfRequest>
    {
        public string Name;
        public EcsPackedEntity Owner;
        public List<AbilityId> Abilities;
        
        public void AutoReset(ref CreateAbilitySequenceByIdSelfRequest c)
        {
            c.Abilities ??= new List<AbilityId>();
            c.Abilities.Clear();
            c.Name = string.Empty;
        }
    }
}