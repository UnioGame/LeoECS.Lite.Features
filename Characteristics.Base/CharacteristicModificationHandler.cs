namespace Game.Ecs.Characteristics.Base
{
    using System;
    using Components;
    using Components.Requests.OwnerRequests;
    using Leopotam.EcsLite;
    using Modification;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public abstract class CharacteristicModificationHandler<TCharacteristic> : ModificationHandler
        where TCharacteristic : struct
    {
        public override void AddModification(EcsWorld world,int sourceEntity, int destinationEntity)
        {
            if(!world.HasComponent<CharacteristicComponent<TCharacteristic>>(destinationEntity))
                return;

            var modification = new Modification.Modification()
            {
                counter = 1,
                allowedSummation = allowedSummation,
                isPercent = isPercent,
                baseValue = value,
                isMaxLimitModification = isMaxLimitModification,
            };
            
            var entity = world.NewEntity();
            ref var request = ref world.AddComponent<AddModificationRequest<TCharacteristic>>(entity);
            request.ModificationSource = world.PackEntity(sourceEntity);
            request.Target = world.PackEntity(destinationEntity);
            request.Modification = modification;
        }

        public override void RemoveModification(EcsWorld world,int source, int destinationEntity)
        {
            if(!world.HasComponent<TCharacteristic>(destinationEntity))
                return;
            
            var entity = world.NewEntity();
            ref var removeRequest = ref world
                .AddComponent<RemoveCharacteristicModificationRequest<TCharacteristic>>(entity);
            removeRequest.Source = world.PackEntity(source);
            removeRequest.Target = world.PackEntity(destinationEntity);
        }
    }
}