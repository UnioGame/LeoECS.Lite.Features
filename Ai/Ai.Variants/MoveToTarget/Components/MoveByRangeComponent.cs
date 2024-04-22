namespace Game.Ecs.GameAi.MoveToTarget.Components
{
    using System;
    using System.Collections.Generic;
    using Code.Configuration.Runtime.Effects.Abstract;
    using UniGame.LeoEcs.Shared.Abstract;
    using UnityEngine;

    [Serializable]
    public struct MoveByRangeComponent : IApplyableComponent<MoveByRangeComponent>
    {
        private static readonly List<IEffectConfiguration> EmptyEffects = new List<IEffectConfiguration>();
        
        public float Priority;
        public Vector3 Center;
        public float Radius;
        public float MinDistance;
        public List<IEffectConfiguration> Effects;

        public void Apply(ref MoveByRangeComponent component)
        {
            EmptyEffects.Clear();
            
            component.Effects = EmptyEffects;
            component.Center = Center;
            component.Radius = Radius;
            component.Priority = Priority;
            component.MinDistance = MinDistance;
        }
    }
}
