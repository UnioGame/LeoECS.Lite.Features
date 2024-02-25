namespace Game.Code.DataBase.Runtime
{
    using System;
    using Abstract;
    using Object = UnityEngine.Object;

    [Serializable]
    public class UnityObjectDatabaseRecord : IGameDatabaseRecord
    {
        public Object asset;

        public string Name => asset.name;

        public string Id => asset == null ? DatabaseRecordConstants.EmptyId : asset.name;

        public bool IsMatch(string searchString)
        {
            if (string.IsNullOrEmpty(searchString)) return true;
            if (Id.IndexOf(searchString) >= 0) return true;
            return false;
        }
    }
}