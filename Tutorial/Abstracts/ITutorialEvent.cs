namespace Game.Ecs.Gameplay.Tutorial.Abstracts
{
	using Leopotam.EcsLite;

	/// <summary>
	/// 
	/// </summary>
	public interface ITutorialEvent
	{
		void ComposeEntity(EcsWorld world, int entity);
	}
}