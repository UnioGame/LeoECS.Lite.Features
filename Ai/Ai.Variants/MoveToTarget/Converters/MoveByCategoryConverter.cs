namespace Game.Ecs.GameAi.MoveToTarget.Converters
{
    using System;
    using System.Linq;
    using AI.Abstract;
    using Components;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UniModules.UniCore.GizmosTools.Shapes;
    using UnityEngine;

    [Serializable]
    public class MoveByCategoryConverter : ComponentPlannerConverter<MoveByCategoryComponent>, 
        IMoveByConverter,ILeoEcsGizmosDrawer
    {
        public Color rangeGizmosColor = Color.red;

        public bool drawGizmos = true;
        
        public void DrawGizmos(GameObject target)
        {
            if (!drawGizmos) return;
            
            var componentData = data;
            var maxRange = componentData.FilterData.Max(x => x.SensorDistance);
            var rotation = target.transform.rotation;
            var position = target.transform.position;
            
            GizmosShape.DrawCircle(position,rotation,maxRange, rangeGizmosColor);
        }
    }
}