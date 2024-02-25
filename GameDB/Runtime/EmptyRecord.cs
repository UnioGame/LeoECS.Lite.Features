namespace Game.Code.DataBase.Runtime
{
    using Abstract;

    public class EmptyRecord : IGameDatabaseRecord
    {
        public readonly static EmptyRecord Value = new EmptyRecord();

        public const string IdKey = nameof(EmptyRecord);

        public string Name => string.Empty;

        public string Id => IdKey;
        public bool IsMatch(string searchString) => false;
    }
}