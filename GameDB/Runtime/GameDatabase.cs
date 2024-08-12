namespace Game.Code.DataBase.Runtime
{
    using System;
    using System.Collections.Generic;
    using Abstract;
    using Cysharp.Threading.Tasks;
    using Sirenix.OdinInspector;
    using UniGame.AddressableTools.Runtime;
    using UniGame.Core.Runtime;
    using UnityEngine.AddressableAssets;
    using Object = UnityEngine.Object;

    [Serializable]
    public class GameDatabase : IGameDatabase
    {
        #region inspector
        
        [InlineEditor()]
        public List<GameResourceLocation> fallBackLocations = new List<GameResourceLocation>();

        [InlineProperty]
        public List<AssetReferenceT<GameDataCategory>> categories = new List<AssetReferenceT<GameDataCategory>>();

        #endregion

        private List<GameDataCategory> _categories = new();

        public async UniTask<IGameDatabase> Initialize(ILifeTime lifeTime)
        {
            var tasks = categories.Select(x =>
                x.LoadAssetInstanceTaskAsync(lifeTime, true));
            
            var categoriesAssets = await UniTask.WhenAll(tasks);
            
            foreach (var categoryInstance in categoriesAssets)
                _categories.Add(categoryInstance);

            return this;
        }

        public async UniTask<GameResourceResult> LoadAsync<TAsset>(string resourceId,ILifeTime lifeTime)
            where TAsset : Object
        {
            resourceId = resourceId.TrimEnd(' ');
            
            var record = Find(resourceId);
            
            var resource = record.Resource;
            var location = record.ResourceLocation;
            var loadFallBack = resource == EmptyRecord.Value || location == null;
            
            var assetResult = loadFallBack
                ? await LoadFallbackResourceAsync<TAsset>(resourceId,lifeTime) 
                : await location.LoadAsync<TAsset>(resource.Id,lifeTime);
            
            if(!assetResult.Complete) return assetResult;

            return assetResult;
        }

        public CategoryResource Find(string id)
        {
            foreach (var category in _categories)
            {
                var record = category.Find(id);
                if (record != EmptyRecord.Value) return new CategoryResource()
                {
                    ResourceLocation = category.ResourceLocation,
                    Resource = record
                };
            }

            return new CategoryResource()
            {
                Resource = EmptyRecord.Value
            };
        }

        private async UniTask<GameResourceResult> LoadFallbackResourceAsync<TAsset>(
            string resourceId,
            ILifeTime lifeTime) where TAsset : Object
        {
            foreach (var resourceLocation in fallBackLocations)
            {
                var resource = await resourceLocation.LoadAsync<TAsset>(resourceId,lifeTime);
                if(!resource.Complete) continue;
                return resource;
            }
            
            return GameResourceResult.FailedResourceResult;
        }
        
        [Serializable]
        public struct CategoryResource
        {
            public IGameResourceLocation ResourceLocation;
            public IGameDatabaseRecord Resource;
        }

    }
}


