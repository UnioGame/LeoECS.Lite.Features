namespace Game.Ecs.Ability.SubFeatures.AbilitySwitcher.Switchers.TimerForAbilitySwitcher.Components
{
	using System;
	using UnityEngine.Serialization;

	/// <summary>
	/// Timer for ability switcher component.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct TimerForAbilitySwitcherComponent
	{
		public float Delay;
		public float DumpTime;
		public bool IsUnscaledTime;
	}
}