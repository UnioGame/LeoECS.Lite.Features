namespace Game.Ecs.Characteristics.ArmorResist
{
	using System;
	using Base;
	using Components;
	using Cysharp.Threading.Tasks;
	using Feature;
	using Leopotam.EcsLite;
	using Leopotam.EcsLite.ExtendedSystems;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Characteristics/Armor Resist Feature", fileName = "Armor Resist Feature")]
	public class ArmorResistFeature : CharacteristicFeature<ArmorResistEcsFeature>
	{
	}
	
	[Serializable]
	public sealed class ArmorResistEcsFeature : CharacteristicEcsFeature
	{
		protected override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			// add armor resist component to unit
			ecsSystems.AddCharacteristic<ArmorResistComponent>();
			// update armor resist value
			ecsSystems.Add(new UpdateArmorResistSystem());
			return UniTask.CompletedTask;
		}
	}
}