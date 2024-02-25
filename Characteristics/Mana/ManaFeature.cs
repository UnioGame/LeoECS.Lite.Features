namespace Game.Ecs.Characteristics.Mana
{
	using Base;
	using Components;
	using Cysharp.Threading.Tasks;
	using Feature;
	using Leopotam.EcsLite;
	using Leopotam.EcsLite.ExtendedSystems;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Characteristics/Mana Feature")]
	public sealed class ManaFeature : CharacteristicFeature
	{
		public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			ecsSystems.AddCharacteristic<ManaComponent>();
			// Recalculate max mana value. Use this request RecalculateMaxManaRequest when you want to recalculate max mana value.
			ecsSystems.Add(new RecalculateManaSystem());
			
			return UniTask.CompletedTask;
		}
	}
}