namespace Game.Ecs.Effects.Components
{
    using Code.Configuration.Runtime.Effects;
    using Data;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Serialization;

    /// <summary>
    /// Отображение эффекта и длительность отображения.
    /// </summary>
    public struct EffectViewDataComponent
    {
        public AssetReferenceGameObject View;
        public GameObject ViewPrefab;
        public float LifeTime;
        public ViewInstanceType ViewInstanceType;
        public bool UseEffectRoot;
        public EffectRootId EffectRoot;
        public bool AttachToSource;
    }
}