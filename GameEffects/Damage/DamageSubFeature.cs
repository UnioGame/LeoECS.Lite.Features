namespace Game.Ecs.Gameplay.Damage
{
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime;

    public abstract class DamageSubFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask BeforeDamageSystem(IEcsSystems ecsSystems)
        {
            return UniTask.CompletedTask;
        }
        
        public virtual UniTask AfterDamageSystem(IEcsSystems ecsSystems)
        {
            return UniTask.CompletedTask;
        }
        
    }
}