namespace Game.Code.DataBase.Runtime.Abstract
{
    using System.Collections.Generic;

    public interface IGameDataCategory
    {
        public string Category { get; }
        
        public IReadOnlyList<IGameDatabaseRecord> Records { get; }

        public IGameDatabaseRecord Find(string id);

        /// <summary>
        /// editor only
        /// </summary>
        public void FillCategory();
    }
}