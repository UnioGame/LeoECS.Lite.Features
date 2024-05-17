using UniGame.LeoEcs.Shared.Abstract;

namespace Game.Ecs.AI.Components
{
    using System;

    [Serializable]
    public struct AiSensorRangeComponent : IApplyableComponent<AiSensorRangeComponent>
    {
        public float Range;
        
        public void Apply(ref AiSensorRangeComponent component)
        {
            component.Range = Range;
        }
    }
}