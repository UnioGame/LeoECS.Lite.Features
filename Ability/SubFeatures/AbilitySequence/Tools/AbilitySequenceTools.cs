namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Tools
{
    using System.Collections.Generic;
    using Aspects;
    using Code.Configuration.Runtime.Ability.Description;
    using Data;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    using UnityEngine.Serialization;

    [ECSDI]
    public class AbilitySequenceTools: IEcsInitSystem
    {
        public const string SequenceNameTemplate = "{0}_{1}_ability_sequence";
        
        private EcsWorld _world;
        
        public AbilitySequenceAspect sequenceAspect;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }
        
        public int CreateAbilitySequence(int ownerEntity,IReadOnlyList<AbilityId> abilityIds,string name)
        {
            var requestEntity = _world.NewEntity();
            ref var createRequest = ref sequenceAspect.CreateById.Add(requestEntity);
            createRequest.Owner = _world.PackEntity(ownerEntity);
            createRequest.Abilities.Clear();
            createRequest.Abilities.AddRange(abilityIds);
            createRequest.Name = name;
            return requestEntity;
        }
        
        public int CreateAbilitySequence(ref EcsPackedEntity ownerEntity,AbilitySequenceReference sequence)
        {
            var requestEntity = _world.NewEntity();
            ref var createRequest = ref sequenceAspect.CreateByReference.Add(requestEntity);
            createRequest.Owner = ownerEntity;
            createRequest.Reference = sequence;
            return requestEntity;
        }


    }
}