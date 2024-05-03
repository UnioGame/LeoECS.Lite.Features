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
        IMoveByConverter
    {
    }
}