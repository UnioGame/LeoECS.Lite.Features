namespace Game.Ecs.Characteristics.ManaRegeneration.Components
{
	using System;

#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;
#endif
	
	/// <summary>
	/// Mana regeneration value and base value.
	/// </summary>
	[Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	public struct ManaRegenerationComponent
	{
		public float Value;
	}
}