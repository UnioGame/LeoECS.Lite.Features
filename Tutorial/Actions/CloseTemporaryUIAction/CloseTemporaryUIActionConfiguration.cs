namespace Game.Ecs.Gameplay.Tutorial.Actions.CloseTemporaryUIAction
{
	using System;
	using Abstracts;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;

	[Serializable]
	public class CloseTemporaryUIActionConfiguration : ITutorialAction
	{
		public void ComposeEntity(EcsWorld world, int entity)
		{
			world.AddComponent<CloseTemporaryUIActionComponent>(entity);
		}
	}
}