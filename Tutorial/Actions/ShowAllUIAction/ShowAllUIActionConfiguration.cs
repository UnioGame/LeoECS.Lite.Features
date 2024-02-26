namespace Game.Ecs.Gameplay.Tutorial.Actions.ShowAllUIAction
{
	using System;
	using Abstracts;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;
    
	public class ShowAllUIActionConfiguration : TutorialAction
	{
		protected override void Composer(EcsWorld world, int entity)
		{
			world.AddComponent<ShowAllUIActionComponent>(entity);
		}
	}
}