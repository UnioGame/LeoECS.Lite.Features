using UniGame.LeoEcs.Shared.Abstract;

namespace Game.Ecs.AI.Components
{
    using System;

    [Serializable]
    public struct AiSensorComponent : IApplyableComponent<AiSensorComponent>
    {
        public float Range;
        
        public void Apply(ref AiSensorComponent component)
        {
            component.Range = Range;
        }
    }
}