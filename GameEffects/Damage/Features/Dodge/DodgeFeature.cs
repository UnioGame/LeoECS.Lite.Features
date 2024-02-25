namespace Game.Ecs.Gameplay.Dodge
{
    using Cysharp.Threading.Tasks;
    using Damage;
    using Events;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Systems;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Damage/Damage Dodge Feature",fileName = "Damage Dodge Feature")]
    public class DeathFeature  : DamageSubFeature
    {
        public sealed override UniTask BeforeDamageSystem(IEcsSystems ecsSystems)
        {
            ecsSystems.DelHere<MissedEvent>();
            //if unit ready to death then create KillRequest
            ecsSystems.Add(new CheckDamageDodgeSystem());
            
            return UniTask.CompletedTask;
        }
    }
}
