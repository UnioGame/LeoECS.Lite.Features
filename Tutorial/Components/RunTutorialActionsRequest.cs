namespace Game.Ecs.Gameplay.Tutorial.Components
{
	using System;
	using Leopotam.EcsLite;

	/// <summary>
	/// Says to run tutorial actions.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct RunTutorialActionsRequest
	{
		public EcsPackedEntity Source;
	}
}