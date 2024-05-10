using UniGame.LeoEcs.Shared.Abstract;

namespace Game.Ecs.AI.Components
{
    using System;
    using global::Characteristics.Radius.Abstract;

    [Serializable]
    public struct AiSensorComponent : IApplyableComponent<AiSensorComponent>, IRadius
    {
        public float Radius => Range;
        
        public float Range;
        public void Apply(ref AiSensorComponent component)
        {
            component.Range = Range;
        }
    }
}