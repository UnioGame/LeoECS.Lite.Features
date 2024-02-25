namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Systems
{
    using System;
    using Ability.Tools;
    using Aspects;
    using Code.Configuration.Runtime.Ability.Description;
    using Code.Services.AbilityLoadout.Data;
    using Components;
    using AbilitySequence;
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
    public class CreateAbilitySequenceByIdSystem : IEcsInitSystem, IEcsRunSystem
    {
        private AbilityTools _tools;
        private EcsWorld _world;
        private EcsFilter _createRequestFilter;
        
        private AbilitySequenceAspect _aspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _tools = _world.GetGlobal<AbilityTools>();
            
            _createRequestFilter = _world
                .Filter<CreateAbilitySequenceByIdSelfRequest>()
                .Exc<CreateAbilitySequenceSelfRequest>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _createRequestFilter)
            {
                ref var requestComponent = ref _aspect.CreateById.Get(requestEntity);

                if (requestComponent.Abilities.Count <= 0)
                {
                    _aspect.CreateById.Del(requestEntity);
                    continue;
                }

                ref var createSequenceRequest = ref _aspect
                    .Create
                    .Add(requestEntity);

                createSequenceRequest.Owner = requestComponent.Owner;
                createSequenceRequest.Name = requestComponent.Name;
                
                foreach (AbilityId abilityId in requestComponent.Abilities)
                {
                    var abilityEntity = _tools.EquipAbilityById(ref requestComponent.Owner, abilityId, AbilitySlotId.EmptyAbilitySlot);
                    createSequenceRequest.Abilities.Add(abilityEntity);
                }
            }
        }
    }
}