namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Systems
{
    using System;
    using Ability.Tools;
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UniGame.LeoEcs.Shared.Extensions;

    /// <summary>
    /// create ability sequence
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    [ECSDI]
    public class CreateAbilitySequenceByRequestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _createRequestFilter;
        private AbilityTools _abilityTools;
        
        private AbilitySequenceAspect _aspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _abilityTools = _world.GetGlobal<AbilityTools>();
            
            _createRequestFilter = _world
                .Filter<CreateAbilitySequenceSelfRequest>()
                .Exc<AbilitySequenceAwaitComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var sequenceEntity in _createRequestFilter)
            {
                ref var requestComponent = ref _aspect.Create.Get(sequenceEntity);
                
                if (requestComponent.Abilities.Count <= 0) continue;
                
                if (!requestComponent.Owner.Unpack(_world, out var ownerEntity))
                {
                    _aspect.CreateById.Del(sequenceEntity);
                    continue;
                }
                
                ref var dataComponent = ref _aspect.Sequence.Add(sequenceEntity);
                ref var ownerComponent = ref _aspect.Owner.Add(sequenceEntity);
                ref var nameComponent = ref _aspect.Name.Add(sequenceEntity);
                ref var awaitComponent = ref _aspect.Await.Add(sequenceEntity);

                //reset sequence data
                dataComponent.Abilities.Clear();
                dataComponent.Abilities.AddRange(requestComponent.Abilities);
                dataComponent.Index = 0;
                dataComponent.ActiveAbility = -1;
                dataComponent.Id = requestComponent.Name.GetHashCode();
                
                ownerComponent.Value = requestComponent.Owner;
                nameComponent.Value = requestComponent.Name;

                var abilityIds = requestComponent.Abilities;
                var abilityCount = abilityIds.Count;

                for (var i = 0; i < abilityCount; i++)
                {
                    var sequenceNodeEntity = abilityIds[i];
                    ref var nodeComponent = ref _aspect.Node.GetOrAddComponent(sequenceNodeEntity);

                    nodeComponent.Order = i;
                    nodeComponent.SequenceEntity = sequenceEntity;
                }
            }
        }

    }
}