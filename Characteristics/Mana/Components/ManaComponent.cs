namespace Game.Ecs.Characteristics.Mana.Components
{
	using System;
	using UnityEngine.Serialization;

#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;
#endif
	
	/// <summary>
	/// Mana value and max mana value.
	/// </summary>
	[Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	public struct ManaComponent
	{
		public float Mana;
		public float MaxMana;
	}
}