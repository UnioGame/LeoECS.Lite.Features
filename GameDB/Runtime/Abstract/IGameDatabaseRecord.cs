namespace Game.Code.DataBase.Runtime.Abstract
{
    using Sirenix.OdinInspector;

    public interface IGameDatabaseRecord : ISearchFilterable
    {
        public string Name { get; }
        public string Id { get; }
    }
}