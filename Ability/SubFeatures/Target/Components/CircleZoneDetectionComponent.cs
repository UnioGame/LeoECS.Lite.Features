namespace Game.Ecs.Ability.SubFeatures.Target.Components
{
	using Unity.Mathematics;

	/// <summary>
	/// The ability has a detection zone
	/// </summary>
	public struct CircleZoneDetectionComponent
	{
		public float2 Offset;
		public float Radius;
	}
}