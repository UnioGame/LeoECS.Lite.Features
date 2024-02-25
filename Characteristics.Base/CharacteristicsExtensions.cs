namespace Game.Ecs.Characteristics.Base
{
    using System.Runtime.CompilerServices;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Components.Requests.OwnerRequests;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using RealizationSystems;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public static class CharacteristicsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEcsSystems AddCharacteristic<TCharacteristic>(this IEcsSystems ecsSystems)
            where TCharacteristic : struct
        {
            //remove changed marker
            ecsSystems.DelHere<CharacteristicChangedComponent<TCharacteristic>>();
            //update characteristic value by source event
            ecsSystems.DelHere<CharacteristicValueChangedEvent<TCharacteristic>>();
            ecsSystems.Add(new DetectCharacteristicChangedSystem<TCharacteristic>());

            //create health characteristic
            ecsSystems.Add(new ChangeTargetCharacteristicMaxLimitationSystem<TCharacteristic>());
            ecsSystems.Add(new ChangeTargetCharacteristicMinLimitationSystem<TCharacteristic>());
            ecsSystems.Add(new ChangeTargetCharacteristicValueSystem<TCharacteristic>());
            ecsSystems.Add(new ChangeTargetCharacteristicBaseSystem<TCharacteristic>());
            
            ecsSystems.Add(new CreateCharacteristicValueSystem<TCharacteristic>());
            
            ecsSystems.Add(new AddCharacteristicModificationSystem<TCharacteristic>());
            ecsSystems.DelHere<AddModificationRequest<TCharacteristic>>();
            
            ecsSystems.Add(new ResetTargetCharacteristicSystem<TCharacteristic>());
            ecsSystems.Add(new ResetTargetCharacteristicMaxLimitSystem<TCharacteristic>());
            ecsSystems.Add(new ResetTargetCharacteristicModificationsSystem<TCharacteristic>());
            
            ecsSystems.Add(new RemoveCharacteristicModificationSystem<TCharacteristic>());
            ecsSystems.DelHere<RemoveCharacteristicModificationRequest<TCharacteristic>>();
            //convert recalculate health request to recalculate characteristic request
            ecsSystems.Add(new RecalculateCharacteristicSystem<TCharacteristic>());
            
            ecsSystems.DelHere<CreateCharacteristicRequest<TCharacteristic>>();
            ecsSystems.DelHere<ResetCharacteristicMaxLimitSelfRequest<TCharacteristic>>();
            ecsSystems.DelHere<ChangeMinLimitSelfRequest<TCharacteristic>>();
            ecsSystems.DelHere<ChangeCharacteristicValueRequest<TCharacteristic>>();
            ecsSystems.DelHere<ChangeMaxLimitSelfRequest<TCharacteristic>>();
            ecsSystems.DelHere<ChangeCharacteristicBaseRequest<TCharacteristic>>();
            ecsSystems.DelHere<ResetCharacteristicSelfRequest<TCharacteristic>>();
            ecsSystems.DelHere<ResetCharacteristicModificationsSelfRequest<TCharacteristic>>();
            
            return ecsSystems;
        }
    }
}