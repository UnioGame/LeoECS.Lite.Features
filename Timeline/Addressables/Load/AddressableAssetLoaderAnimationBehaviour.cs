namespace Game.Code.Timeline.Addressables
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Sirenix.OdinInspector;
    using UniGame.AddressableTools.Runtime;
    using UniModules.UniGame.Core.Runtime.DataFlow.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using Object = UnityEngine.Object;

    [Serializable]
    public class AddressableAssetLoaderAnimationBehaviour : AddressableAnimationBehaviour
    {
        [DrawWithUnity]
        public List<AssetReference> assets = new List<AssetReference>();

        
        public override void Load(GameObject source,float inputWeight, float progress)
        {
            var lifeTime = source.GetAssetLifeTime();
            
            foreach (var asset in assets)
                asset.LoadAssetTaskAsync<Object>(lifeTime).Forget();
        }
    }
}