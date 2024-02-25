namespace Game.Ecs.Effects.Components
{
    using Code.Configuration.Runtime.Effects;
    using Data;
    using UnityEngine;

    /// <summary>
    /// Отображение эффекта и длительность отображения.
    /// </summary>
    public struct EffectViewDataComponent
    {
        public GameObject View;
        public float LifeTime;
        public ViewInstanceType ViewInstanceType;
        public bool UseEffectRoot;
        public EffectRootId EffectRoot;
        public bool AttachToSource;
    }
}