namespace Game.Ecs.Characteristics.ArmorResist.Aspects
{
	using System;
	using Base.Components;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Bootstrap.Runtime.Abstract;

	/// <summary>
	/// Armor resist aspect
	/// </summary>
	[Serializable]
	public class ArmorResistAspect : EcsAspect
	{
		// characteristics marker
		public EcsPool<CharacteristicComponent<ArmorResistComponent>> Characteristic;
		// armor resist value
		public EcsPool<ArmorResistComponent> ArmorResist;
	}
}