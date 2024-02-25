namespace Game.Ecs.Gameplay.Dodge
{
    using Cysharp.Threading.Tasks;
    using Damage;
    using Damage.Systems;
    using Leopotam.EcsLite;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Damage/Damage Shield Feature",fileName = "Damage Shield Feature")]
    public class ShieldFeature  : DamageSubFeature
    {
        public sealed override UniTask BeforeDamageSystem(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new CheckDamageShieldSystem());
            
            return UniTask.CompletedTask;
        }
    }
}
