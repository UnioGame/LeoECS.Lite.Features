namespace Game.Code.DataBase.Runtime
{
    using Abstract;
    using Cysharp.Threading.Tasks;
    using UniGame.Core.Runtime;
    using UnityEngine;

    public abstract class GameResourceLocation : ScriptableObject,IGameResourceLocation
    {
        public abstract UniTask<GameResourceResult> LoadAsync<TAsset>(string resource, ILifeTime lifeTime)
            where TAsset : Object;
    }

    
}