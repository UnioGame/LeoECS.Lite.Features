namespace Game.Ecs.Gameplay.FreezingTime.Aspects
{
	using System;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Freezing time aspect
	/// </summary>
	[Serializable]
	public class FreezingTimeAspect : EcsAspect
	{
		// Says that time should be un/frozen.
		public EcsPool<FreezingTimeRequest> freezingTimeRequest;
		
		// Says that un/freezing time completed.
		public EcsPool<FreezingTimeCompletedEvent> freezingTimeCompletedEvent;
	}
}