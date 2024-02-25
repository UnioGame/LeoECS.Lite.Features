namespace Game.Ecs.AbilityInventory.Components
{
	using System;
	using Code.Configuration.Runtime.Ability;
	using Unity.IL2CPP.CompilerServices;
	using UnityEngine.AddressableAssets;

	/// <summary>
	/// configuration data of ability
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct AbilityConfigurationComponent
	{
		public AbilityConfiguration Value;
	}
	
	/// <summary>
	/// Ability configuration
	/// </summary>
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public struct AbilityConfigurationReferenceComponent
	{
		public AssetReferenceT<AbilityConfiguration> AbilityConfiguration;
	}
}