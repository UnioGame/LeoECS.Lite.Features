using System;
using Game.Ecs.AI.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Ecs.AI.Configurations
{
    [Serializable]
    public class AiCommonPlanners
    {
        [SerializeReference]
        [InlineEditor]
        public IAiCommonConverter commonAiConverters;
    }
}