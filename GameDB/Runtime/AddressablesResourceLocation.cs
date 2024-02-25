namespace Game.Code.DataBase.Runtime
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UniGame.AddressableTools.Runtime;
    using UniGame.Core.Runtime;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using Object = UnityEngine.Object;

    [CreateAssetMenu(menuName = "Game/GameDatabase/Locations/"+nameof(AddressablesResourceLocation),fileName = nameof(AddressablesResourceLocation))]
    public class AddressablesResourceLocation : GameResourceLocation
    {
        public static Type ComponentType = typeof(Component);
        
        public override async UniTask<GameResourceResult> LoadAsync<TAsset>(string resource,ILifeTime lifeTime)
        {
            var addressableResult = await resource.LoadAssetTaskAsync<TAsset>(lifeTime);

            var result = new GameResourceResult()
            {
                Complete = addressableResult.Complete,
                Error = addressableResult.Error,
                Exception = addressableResult.Exception,
                Result = addressableResult.Result
            };
            
            return result;
        }

    }

}