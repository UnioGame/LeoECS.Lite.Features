namespace Game.Code.GameLayers.Layer
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Layer Id Configuration", menuName = "Game/Configurations/Game Layers/Layer Id Configuration")]
    public sealed class LayerIdConfiguration : ScriptableObject
    {
        public const int MaxLayerCount = 32;
        
        private LayerId[] _layerIds;
        
        [SerializeField]
        private string[] _layers = new string[MaxLayerCount];

        public LayerId[] LayersIds => _layerIds = _layerIds == null || _layerIds.Length == 0 ? CreateLayerIds() : _layerIds;

        public IReadOnlyList<string> LayerNames => _layers;

        public string GetLayerName(int layerIndex)
        {
            if (layerIndex < 0 || layerIndex >= _layers.Length)
                return string.Empty;

            return _layers[layerIndex];
        }

        public string GetLayerNameByValue(int layerValue)
        {
            var index = (int)Mathf.Log(layerValue, 2);
            return index >= _layers.Length || index < 0 ? string.Empty : _layers[index];
        }

        public int GetLayerValue(int layerIndex)
        {
            if (layerIndex < 0 || layerIndex >= _layers.Length)
                return 0;

            return (int)Mathf.Pow(2, layerIndex);
        }

        public static LayerId[] CreateLayerIds()
        {
            var layerIds = new LayerId[MaxLayerCount];
            for (var i = 0; i < MaxLayerCount; i++)
            {
                layerIds[i] = (LayerId) Mathf.Pow(2, i);
            }

            return layerIds;
        }
    }
}