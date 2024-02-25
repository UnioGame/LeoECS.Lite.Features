namespace Game.Ecs.ViewControl.Components
{
    using System.Collections.Generic;
    using Leopotam.EcsLite;
    using UnityEngine;

    internal struct ViewDataComponent : IEcsAutoReset<ViewDataComponent>
    {
        public Dictionary<GameObject, EcsPackedEntity> Views;

        public void AutoReset(ref ViewDataComponent c)
        {
            c.Views ??= new Dictionary<GameObject, EcsPackedEntity>();
            c.Views.Clear();
        }
    }
}