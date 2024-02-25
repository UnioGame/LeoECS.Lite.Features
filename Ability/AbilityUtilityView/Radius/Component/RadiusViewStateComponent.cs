namespace Game.Ecs.Ability.AbilityUtilityView.Radius.Component
{
    using System.Collections.Generic;
    using Leopotam.EcsLite;
    using UnityEngine;

    public struct RadiusViewStateComponent : IEcsAutoReset<RadiusViewStateComponent>
    {
        public Dictionary<EcsPackedEntity, GameObject> RadiusViews;
        
        public void AutoReset(ref RadiusViewStateComponent c)
        {
            c.RadiusViews ??= new Dictionary<EcsPackedEntity, GameObject>();
            c.RadiusViews.Clear();
        }
    }
}