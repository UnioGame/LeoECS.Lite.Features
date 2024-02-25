namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Components
{
	using System;
	using Code.Configuration.Runtime.Ability.Description;

	/// <summary>
	/// Says that this ability can switch between other abilities.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct AbilitySwitcherComponent
	{
	}
}