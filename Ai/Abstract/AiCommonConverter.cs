using UnityEngine.Serialization;

namespace Game.Ecs.AI.Abstract
{
    using System;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    
    [Serializable]
    public class AiCommonConverter<TComponent> : EcsComponentConverter, IAiCommonConverter
        where TComponent : struct, IApplyableComponent<TComponent>
    {
        [FormerlySerializedAs("_value")]
        [SerializeField] public TComponent Value;
        
        public override void Apply(EcsWorld world, int entity)
        {
            ref var component = ref world.AddComponent<TComponent>(entity);
            Value.Apply(ref component);
        }
    }
}