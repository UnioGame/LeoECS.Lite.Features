namespace Game.Code.DataBase.Runtime
{
    using System;
    using Object = UnityEngine.Object;

    [Serializable]
    public struct GameResourceResult
    {
        public const string ResourceError = "Game Resource Not Found";
        public static GameResourceResult FailedResourceResult = new GameResourceResult()
        {
            Complete = false,
            Error = ResourceError,
            Result = null
        };
        
        public Object Result;
        public bool Complete;
        public string Error;
        public Exception Exception;
    }
}