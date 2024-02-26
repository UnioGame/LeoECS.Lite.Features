namespace Game.Ecs.Gameplay.FreezingTime.Components
{
	using UnityEngine.Serialization;

	/// <summary>
	/// Says that time should be un/frozen.
	/// </summary>
	public struct FreezingTimeRequest
	{
		public float Duration;
		public float TimeScale;
	}
}