namespace Game.Ecs.Gameplay.Tutorial.Triggers.DistanceTrigger
{
	using Abstracts;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;
    
	public class DistanceTriggerConfiguration : TutorialTrigger
	{
		#region Inspector

		public float distance = 1f;

		#endregion
		
		protected override void Composer(EcsWorld world, int entity)
		{
			ref var distanceComponent = ref world.AddComponent<DistanceTriggerPointComponent>(entity);
			distanceComponent.TriggerDistance = distance;
		}
	}
}