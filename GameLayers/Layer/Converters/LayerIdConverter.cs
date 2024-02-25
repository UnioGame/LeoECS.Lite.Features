namespace Game.Ecs.GameLayers.Layer.Converters
{
    using System;
    using System.Threading;
    using Code.GameLayers.Layer;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public sealed class LayerIdConverter : GameObjectConverter
    {
        [SerializeField]
        public LayerId layer;

        protected sealed override void OnApply(GameObject target, EcsWorld world, int entity)
        {
            ref var layerIdComponent = ref world.AddComponent<LayerIdComponent>(entity);
            layerIdComponent.Value = layer;
        }
    }
}
