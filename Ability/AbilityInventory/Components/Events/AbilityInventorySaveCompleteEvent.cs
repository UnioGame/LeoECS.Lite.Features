namespace Game.Ecs.AbilityInventory.Components
{
	using Leopotam.EcsLite;
	using Unity.IL2CPP.CompilerServices;

	/// <summary>
	/// Save to profile complete
	/// </summary>
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public struct AbilityInventorySaveCompleteEvent
	{
		public EcsPackedEntity Value;
	}
}