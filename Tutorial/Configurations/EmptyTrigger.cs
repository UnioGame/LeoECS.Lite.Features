namespace Game.Ecs.Gameplay.Tutorial.Configurations
{
	using Abstracts;
	using Leopotam.EcsLite;

	public class EmptyTrigger : ITutorialTrigger
	{
		public void ComposeEntity(EcsWorld world, int entity)
		{
		}
	}
}