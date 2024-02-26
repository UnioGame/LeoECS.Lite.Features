namespace Game.Ecs.Gameplay.Tutorial.Triggers.ActionTrigger.Components
{
	using System;
	using ActionTools;

	/// <summary>
	/// Request to execute action.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct ActionTriggerRequest
	{
		public ActionId ActionId;
	}
}