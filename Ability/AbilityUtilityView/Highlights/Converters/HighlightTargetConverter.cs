namespace Game.Ecs.Ability.AbilityUtilityView.Highlights.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using UniGame.AddressableTools.Runtime;
    using UniGame.Core.Runtime;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniModules.UniGame.Core.Runtime.DataFlow.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [Serializable]
    public sealed class HighlightTargetConverter : LeoEcsConverter
    {
        [SerializeField]
        public AssetReferenceGameObject highlight;

        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            LoadHighlightAsync(world,entity,target.GetAssetLifeTime()).Forget();
        }

        private async UniTask LoadHighlightAsync(EcsWorld world,int entity,ILifeTime lifeTime)
        {
            var asset = await highlight.LoadAssetInstanceTaskAsync<GameObject>(lifeTime,true);
            ApplyComponent(world, entity, asset);
        }

        private void ApplyComponent(EcsWorld world, int entity,GameObject highlightAsset)
        {
            ref var highlightComponent = ref world.GetOrAddComponent<HighlightComponent>(entity);
            highlightComponent.Highlight = highlightAsset;
        }
    }
}