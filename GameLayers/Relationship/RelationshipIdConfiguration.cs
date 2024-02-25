namespace Game.Code.GameLayers.Relationship
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Relationship Id Configuration", menuName = "Game/Configurations/Game Layers/Relationship Id Configuration")]
    public sealed class RelationshipIdConfiguration : ScriptableObject
    {
        public const int MaxRelationshipsCount = 32;
        
        [SerializeField]
        private string[] _relationships = new string[MaxRelationshipsCount];

        public IReadOnlyList<string> Relationships => _relationships;

        public string GetRelationshipName(int relationshipIndex)
        {
            if (relationshipIndex < 0 || relationshipIndex >= _relationships.Length)
                return string.Empty;

            return _relationships[relationshipIndex];
        }

        public string GetRelationshipNameByValue(int relationshipValue)
        {
            var index = (int)Mathf.Log(relationshipValue, 2);
            return index >= _relationships.Length || index < 0 ? string.Empty : _relationships[index];
        }

        public int GetRelationshipValue(int relationshipIndex)
        {
            if (relationshipIndex < 0 || relationshipIndex >= _relationships.Length)
                return 0;

            return (int)Mathf.Pow(2, relationshipIndex);
        }
    }
}