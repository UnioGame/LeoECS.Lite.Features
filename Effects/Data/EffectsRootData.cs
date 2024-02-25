namespace Game.Ecs.Effects.Data
{
    using System;
    using Sirenix.OdinInspector;

    [Serializable]
    public class EffectsRootData
    {
        [ListDrawerSettings(ListElementLabelName = "name")]
        public EffectRootKey[] roots = Array.Empty<EffectRootKey>();
    }
}