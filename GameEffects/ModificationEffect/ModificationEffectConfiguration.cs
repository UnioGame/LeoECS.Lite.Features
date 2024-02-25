namespace Game.Ecs.GameEffects.ModificationEffect
{
    using System;
    using System.Collections.Generic;
    using Characteristics.Base.Modification;
    using Components;
    using Effects;
    using Leopotam.EcsLite;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public sealed class ModificationEffectConfiguration : EffectConfiguration
    {
        [FormerlySerializedAs("_modificationHandlers")] 
        [SerializeReference]
        public List<ModificationHandler> modificationHandlers = new List<ModificationHandler>();
        
        protected override void Compose(EcsWorld world, int effectEntity)
        {
            var modificationPool = world.GetPool<ModificationEffectComponent>();
            ref var modification = ref modificationPool.Add(effectEntity);
            
            var handlers = modification.ModificationHandlers;
            handlers.AddRange(modificationHandlers);
        }
    }
}