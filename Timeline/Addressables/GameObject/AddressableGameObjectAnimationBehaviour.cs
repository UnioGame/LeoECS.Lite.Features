namespace Game.Code.Timeline.Addressables
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using Sirenix.OdinInspector;
    using UniGame.AddressableTools.Runtime;
    using UniGame.Core.Runtime;
    using UniGame.Runtime.ObjectPool.Extensions;
    using UniModules.UniGame.Core.Runtime.DataFlow.Extensions;
    using UnityEngine;

    [Serializable]
    public class AddressableGameObjectAnimationBehaviour : AddressableAnimationBehaviour
    {
        [DrawWithUnity]
        public List<AddressableGameObjectData> assets = new List<AddressableGameObjectData>();

        public override void Load(GameObject source,float inputWeight, float progress)
        {
            if (source == null) return;
            
            var lifeTime = source.GetAssetLifeTime();
            
            foreach (var asset in assets)
            {
                LoadGameObjectAsync(asset,source,lifeTime).Forget();
            }
        }

        private async UniTask LoadGameObjectAsync(AddressableGameObjectData data,GameObject source,ILifeTime lifeTime)
        {
            var reference = data.asset;
            var sourceAsset = await reference.LoadAssetTaskAsync<GameObject>(lifeTime);
            if (!data.makeInstance) return;

            var parent = data.useParent ? source.transform : null;
            var asset = sourceAsset.Spawn(data.position, Quaternion.Euler(data.rotation), parent, data.stayWorldPosition);
            asset.transform.localScale = data.scale;
            asset.gameObject.SetActive(true);
        }
    }
}