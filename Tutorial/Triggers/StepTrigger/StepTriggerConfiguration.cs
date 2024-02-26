namespace Game.Ecs.Gameplay.Tutorial.Triggers.StepTrigger
{
	using Abstracts;
	using ActionTools;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	public class StepTriggerConfiguration : TutorialTrigger
	{
		#region Inspector

		public int Level;
		public int Stage;
		[SerializeReference]
		public TutorialKey StepKey;

		#endregion
		protected override void Composer(EcsWorld world, int entity)
		{
			ref var stepTriggerComponent = ref world.AddComponent<StepTriggerComponent>(entity);
			stepTriggerComponent.StepKey = StepKey;
			stepTriggerComponent.Level = Level;
			stepTriggerComponent.Stage = Stage;
		}
	}
}