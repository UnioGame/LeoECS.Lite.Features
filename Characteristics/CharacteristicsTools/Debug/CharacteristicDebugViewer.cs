namespace Game.Editor.Runtime.CharacteristicsViewer
{
    using System;
    using Ecs.Characteristics.Base.Components;
    using Ecs.Characteristics.Base.Components.Requests;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class CharacteristicDebugViewer<TCharacteristic> :
        EcsCharacteristicDebugView,
        ISearchFilterable
        where TCharacteristic : struct
    {

        private EcsPool<CharacteristicComponent<TCharacteristic>> _pool;
        private EcsWorld _world;
        private EcsPackedEntity _packedEntity;

        public bool IsReady => _world != null && _world.IsAlive();
        
        public CharacteristicDebugViewer(string name,EcsWorld world,int entity)
        {
            Name = name;
            
            _world = world;
            _packedEntity = world.PackEntity(entity);
            _pool = world.GetPool<CharacteristicComponent<TCharacteristic>>();
        }

        public override CharacteristicValue CreateView()
        {
            IsActive = false;
            var result = new CharacteristicValue();
            
            if (!IsReady) return result; 
            if(!_packedEntity.Unpack(_world,out var entity)) return result;
            IsActive = _pool.Has(entity);

            if (!IsActive) return result;
            
            var component = _pool.Get(entity);
            result.Value = component.Value;
            result.MaxValue = component.MaxValue;
            result.MinValue = component.MinValue;
            result.BaseValue = component.BaseValue;

            return result;
        }
        
        public override void Recalculate()
        {
            if (!_packedEntity.Unpack(_world, out var entity)) return;
            var requestComponent = _world.AddComponent<RecalculateCharacteristicSelfRequest<TCharacteristic>>(entity);
        }

        public bool IsMatch(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) return true;
            if (Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)) return true;
            if (typeof(TCharacteristic).Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)) return true;

            return false;
        }
    }
}