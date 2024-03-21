namespace Game.Ecs.Characteristics.Mana
{
	using System;
	using Base;
	using Components;
	using Cysharp.Threading.Tasks;
	using Feature;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Characteristics/Mana Feature")]
	public sealed class ManaFeature : CharacteristicFeature<ManaEcsFeature>
	{
	}
	
	[Serializable]
	public class ManaEcsFeature : CharacteristicEcsFeature
	{
		protected sealed override UniTask OnInitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			ecsSystems.AddCharacteristic<ManaComponent>();
			// Recalculate max mana value. Use this request RecalculateMaxManaRequest when you want to recalculate max mana value.
			ecsSystems.Add(new RecalculateManaSystem());
			
			return UniTask.CompletedTask;
		}
	}
}