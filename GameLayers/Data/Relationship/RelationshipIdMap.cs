namespace Game.Code.GameLayers.Relationship
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Relationship Id Map", menuName = "Game/Configurations/Game Layers/Relationship Id Map")]
    public sealed class RelationshipIdMap : ScriptableObject
    {
        public const int MaxRowAndColumnCount = 32;

        private RelationshipId[,] _relationshipMatrix;
        
        [SerializeField]
        private RelationshipColumn[] _relationshipsMap = new RelationshipColumn[MaxRowAndColumnCount];

        public RelationshipId[,] RelationshipMatrix
        {
            get => _relationshipMatrix = _relationshipMatrix == null || _relationshipMatrix.Length == 0 
                ?  CreateMatrix()
                : _relationshipMatrix;
            set => UpdateFromMatrix(value);
        }

        private RelationshipId[,] CreateMatrix()
        {
            var matrix = new RelationshipId[MaxRowAndColumnCount, MaxRowAndColumnCount];
            for (var y = 0; y < MaxRowAndColumnCount; y++)
            {
                var relationshipsRow = _relationshipsMap[y].Row;
                for (var x = 0; x < MaxRowAndColumnCount; x++)
                {
                    matrix[x, y] = relationshipsRow[x];
                }
            }

            return matrix;
        }

        private void UpdateFromMatrix(RelationshipId[,] matrix)
        {
            for (var y = 0; y < MaxRowAndColumnCount; y++)
            {
                for (var x = 0; x < MaxRowAndColumnCount; x++)
                {
                    var relationship = matrix[x, y];
                    _relationshipsMap[y].Row[x] = relationship;
                }
            }
        }

        [Serializable]
        internal sealed class RelationshipColumn
        {
            public RelationshipId[] Row = new RelationshipId[MaxRowAndColumnCount];
        }
    }
}