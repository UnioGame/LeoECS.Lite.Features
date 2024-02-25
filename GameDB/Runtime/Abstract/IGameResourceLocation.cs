namespace Game.Code.DataBase.Runtime.Abstract
{
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UniGame.Core.Runtime;
    using UnityEngine;

    public interface IGameResourceLocation
    {
        UniTask<GameResourceResult> LoadAsync<TAsset>(string resource,ILifeTime lifeTime) where TAsset : Object;
    }
}