namespace Game.Ecs.Gameplay.Tutorial.Actions.SavePrefAction
{
	using Abstracts;
	using ActionTools;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;

	public class SavePrefActionConfiguration : TutorialAction
	{
		#region Inspector

		public TutorialKey StepKey;

		#endregion
		protected override void Composer(EcsWorld world, int entity)
		{
			ref var savePrefComponent = ref world.AddComponent<SavePrefComponent>(entity);
			savePrefComponent.Value = StepKey;
		}
	}
}