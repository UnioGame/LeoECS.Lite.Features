namespace Game.Ecs.Presets.Components
{
    using System;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public struct LightPreset
    {
        public Color Color;
        public float Intencivity;
        public float ShadowStrength;
        public float Range;
        public float SpotAngle;
        public LightType Type;
    }
}