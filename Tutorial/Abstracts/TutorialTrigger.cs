namespace Game.Ecs.Gameplay.Tutorial.Abstracts
{
	using System;
	using Components;
	using Leopotam.EcsLite;
	using Sirenix.OdinInspector;
	using Time.Service;
	using UnityEngine;

	[Serializable]
	public abstract class TutorialTrigger : ITutorialTrigger
	{
		#region Inspector
        
		public float delay;

		#endregion
		
		public void ComposeEntity(EcsWorld world, int entity)
		{
			var delayedPool = world.GetPool<DelayedTutorialComponent>();
			if (delay > 0f && !delayedPool.Has(entity))
			{
				ref var delayed = ref delayedPool.Add(entity);
				delayed.Delay = delay;
				delayed.LastApplyingTime = Time.unscaledTime;
				delayed.Context = this;
				return;
			}
			
			Composer(world, entity);
		}
		
		protected abstract void Composer(EcsWorld world, int entity);
	}
}