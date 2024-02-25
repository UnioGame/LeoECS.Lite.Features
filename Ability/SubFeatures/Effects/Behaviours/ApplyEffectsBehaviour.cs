namespace Game.Ecs.Ability.SubFeatures.Effects.Behaviours
{
    using System;
    using System.Collections.Generic;
    using Code.Configuration.Runtime.Ability.Description;
    using Code.Configuration.Runtime.Effects.Abstract;
    using Core.Components;
    using Ecs.Effects.Components;
    using Leopotam.EcsLite;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public sealed class ApplyEffectsBehaviour : IAbilityBehaviour
    {
        [FormerlySerializedAs("_effects")] 
        [SerializeReference] 
        public List<IEffectConfiguration> effects = new List<IEffectConfiguration>();
        
        public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
        {
            var effectsPool = world.GetPool<EffectsComponent>();
            ref var effectsComponent = ref effectsPool.Add(abilityEntity);
            effectsComponent.Effects.AddRange(effects);
        }
    }
}