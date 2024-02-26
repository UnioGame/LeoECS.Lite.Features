namespace Game.Ecs.Gameplay.Tutorial.Components
{
	using System;

	/// <summary>
	/// Mark tutorial trigger point as completed.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct CompletedTutorialTriggerPointComponent
	{
		
	}
}