namespace Game.Ecs.GameAi.ActivateAbility.Converters
{
    using System;
    using AI.Abstract;
    using Components;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UniModules.UniCore.GizmosTools.Shapes;
    using UnityEngine;

    [Serializable]
    public class AbilityByRangeConverter : ComponentPlannerConverter<AbilityByRangeComponent>, 
        IAbilityByConverter, ILeoEcsGizmosDrawer
    {
        public bool drawGizmos = true;
        public Color rangeGizmosColor = Color.red;
        
        public void DrawGizmos(GameObject target)
        {
            if (!drawGizmos) return;
            
            var maxRange = data.Radius;
            var rotation = target.transform.rotation;
            var position = target.transform.position + data.Center;
            
            GizmosShape.DrawCircle(position, rotation, maxRange, rangeGizmosColor);
        }
    }
}