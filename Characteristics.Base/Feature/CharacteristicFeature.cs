namespace Game.Ecs.Characteristics.Feature
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public abstract class CharacteristicFeature<TFeature> : BaseLeoEcsFeature
        where TFeature : CharacteristicEcsFeature,new()
    {
        private TFeature _feature = new TFeature();
        
        public sealed override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            return _feature.InitializeFeatureAsync(ecsSystems);
        }
    }
    
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public abstract class CharacteristicEcsFeature : LeoEcsFeature
    {
        
    }
}