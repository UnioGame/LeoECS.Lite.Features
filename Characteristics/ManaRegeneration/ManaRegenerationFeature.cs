namespace Game.Ecs.Characteristics.ManaRegeneration
{
	using Base;
	using Components;
	using Cysharp.Threading.Tasks;
	using Feature;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Characteristics/Mana Regeneration Feature")]
	public sealed class ManaRegenerationFeature : CharacteristicFeature
	{
		public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
		{
			ecsSystems.AddCharacteristic<ManaRegenerationComponent>();
			// Recalculate mana regeneration value. Use this request RecalculateManaRegenerationRequest when you want to recalculate mana regeneration value.
			ecsSystems.Add(new RecalculateManaRegeneration());
			// Mana regeneration. Uses request ChangeManaRequest when you want to change mana value.
			// Inside uses a timer. 
			ecsSystems.Add(new ProcessManaRegenerationSystem());
			return UniTask.CompletedTask;
		}
	}
}