namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Switchers.UsageCounterForAbilitySwitcher.Components
{
	using System;
	using Code.Configuration.Runtime.Ability.Description;

	/// <summary>
	/// Says that ability can be used after count of usages
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct UsageCounterForAbilitySwitcherComponent
	{
		public AbilityId abilityId;
		public int baseCount;
		public int count;
	}
}