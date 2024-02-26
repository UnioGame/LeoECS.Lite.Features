namespace Game.Ecs.Gameplay.Tutorial.Actions.OverrideRestrictTapAreaAction
{
	using Abstracts;
	using Components;
	using Leopotam.EcsLite;
	using Tutorial.Abstracts;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	public class OverrideRestrictTapAreaActionConfiguration : TutorialAction
	{
		#region Inspector
		
		[SerializeReference]
		public IOverrideRestrictTapArea OverrideRestrictTapArea;

		#endregion
		protected override void Composer(EcsWorld world, int entity)
		{
			ref var overriderComponent = ref world.AddComponent<OverrideRestrictTapAreaActionComponent>(entity);
			overriderComponent.Value = OverrideRestrictTapArea;
		}
	}
}