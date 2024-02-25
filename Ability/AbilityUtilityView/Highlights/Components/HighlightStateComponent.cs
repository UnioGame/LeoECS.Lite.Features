namespace Game.Ecs.Ability.AbilityUtilityView.Highlights.Components
{
    using System.Collections.Generic;
    using Leopotam.EcsLite;
    using UnityEngine;

    public struct HighlightStateComponent : IEcsAutoReset<HighlightStateComponent>
    {
        public Dictionary<EcsPackedEntity, GameObject> Highlights;
        
        public void AutoReset(ref HighlightStateComponent c)
        {
            c.Highlights ??= new Dictionary<EcsPackedEntity, GameObject>();
            c.Highlights.Clear();
        }
    }
}