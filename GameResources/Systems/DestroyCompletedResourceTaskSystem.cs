namespace Game.Ecs.GameResources.Systems
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.Di;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class DestroyCompletedResourceTaskSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilterInject<
            Inc<GameResourceTaskCompleteSelfEvent,GameResourceTaskCompleteComponent>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
                _world.DelEntity(entity);
        }
    }
}