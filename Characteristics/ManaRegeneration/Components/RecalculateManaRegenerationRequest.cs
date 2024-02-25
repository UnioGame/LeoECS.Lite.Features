namespace Game.Ecs.Characteristics.ManaRegeneration.Components
{
	using Leopotam.EcsLite;

	/// <summary>
	/// Recalculate mana regeneration value.
	/// </summary>
	public struct RecalculateManaRegenerationRequest
	{
		public EcsPackedEntity Source;
	}
}