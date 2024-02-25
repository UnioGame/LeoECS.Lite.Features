namespace Game.Ecs.Characteristics.ArmorResist.Components
{
	using System;

	/// <summary>
	/// Armor Resist value
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct ArmorResistComponent
	{
		public float Value;
	}
}