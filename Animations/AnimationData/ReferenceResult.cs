namespace Game.Code.Animations
{
    using System;
    using Object = UnityEngine.Object;

    [Serializable]
    public struct ReferenceResult
    {
        public static readonly ReferenceResult None = new ReferenceResult()
        {
            key = 0,
            isResolved = false,
            result = null,
        };
        
        public Object result;
        public bool isResolved;
        public int key;
    }
}