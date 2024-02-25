namespace Game.Ecs.GameEffects.LevitationEffect.Components
{
	using UnityEngine;

	/// <summary>
	/// Data for levitation effect.
	/// </summary>
	public struct LevitationEffectComponent
	{
		public float Height;
		public float Duration;
		public Vector3 Rotation;
		public float DurationRotation;
	}
}