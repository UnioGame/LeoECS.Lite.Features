namespace Game.Ecs.Gameplay.Tutorial.Actions.HealingChampionAction.Components
{
	using System;

	/// <summary>
	/// Healing Data
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct HealingChampionActionComponent
	{
		public float HealPeriod;
		public float HealDuration;
		public float HealOverMax;
	}
}