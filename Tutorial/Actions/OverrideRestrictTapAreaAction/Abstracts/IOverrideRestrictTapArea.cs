namespace Game.Ecs.Gameplay.Tutorial.Actions.OverrideRestrictTapAreaAction.Abstracts
{
	using Leopotam.EcsLite;

	public interface IOverrideRestrictTapArea
	{
		void ComposeEntity(EcsWorld world, int entity);
	}
}