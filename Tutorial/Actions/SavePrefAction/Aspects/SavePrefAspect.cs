namespace Game.Ecs.Gameplay.Tutorial.Actions.SavePrefAction.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// ADD DESCRIPTION HERE
	/// </summary>
	[Serializable]
	public class SavePrefAspect : EcsAspect
	{
		public EcsPool<SavePrefComponent> SavePref;
		public EcsPool<CompletedSavePrefComponent> CompletedSavePref;
	}
}