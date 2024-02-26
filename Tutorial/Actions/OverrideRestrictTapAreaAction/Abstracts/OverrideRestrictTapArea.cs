namespace Game.Ecs.Gameplay.Tutorial.Actions.OverrideRestrictTapAreaAction.Abstracts
{
	using Leopotam.EcsLite;

	public abstract class OverrideRestrictTapArea : IOverrideRestrictTapArea
	{
		public void ComposeEntity(EcsWorld world, int entity)
		{
			Composer(world, entity);
		}
		
		protected abstract void Composer(EcsWorld world, int entity);
	}
}