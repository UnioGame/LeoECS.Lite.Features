namespace Game.Ecs.Gameplay.Dodge
{
    using Cysharp.Threading.Tasks;
    using Damage;
    using Damage.Systems;
    using Leopotam.EcsLite;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Damage/Damage Block Feature",fileName = "Damage Block Feature")]
    public class BlockFeature  : DamageSubFeature
    {
        public sealed override UniTask BeforeDamageSystem(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new CheckDamageBlockSystem());
            
            return UniTask.CompletedTask;
        }
    }
}
