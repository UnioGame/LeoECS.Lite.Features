namespace Game.Ecs.Gameplay.ArmorResist
{
	using Cysharp.Threading.Tasks;
	using Damage;
	using Leopotam.EcsLite;
	using Systems;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Feature/Damage/Armor Resist Feature", fileName = "Armor Resist Feature")]
	public sealed class ArmorResistFeature : DamageSubFeature
	{
		public sealed override UniTask BeforeDamageSystem(IEcsSystems ecsSystems)
		{
			// recalculate damage by armor resist
			ecsSystems.Add(new RecalculatedDamageArmorResistSystem());
			
			return UniTask.CompletedTask;
		}
	}
}