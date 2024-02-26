namespace Game.Ecs.Gameplay.Tutorial.Components
{
	using System;
	using System.Collections.Generic;
	using Abstracts;
	using Leopotam.EcsLite;

	/// <summary>
	/// Stores tutorial actions.
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct TutorialActionsComponent : IEcsAutoReset<TutorialActionsComponent>
	{
		public List<ITutorialAction> Actions;
		
		public void AutoReset(ref TutorialActionsComponent c)
		{
			c.Actions ??= new List<ITutorialAction>();
			c.Actions.Clear();
		}
	}
}