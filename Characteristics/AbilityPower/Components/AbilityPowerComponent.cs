namespace Game.Ecs.Characteristics.AbilityPower.Components
{
	using System;
	using UnityEngine.Serialization;

#if ENABLE_IL2CPP
	using Unity.IL2CPP.CompilerServices;
#endif
	
	/// <summary>
	/// Ability Power value
	/// </summary>
	[Serializable]
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	public struct AbilityPowerComponent
	{
		public float Value;
	}
}