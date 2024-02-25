namespace Game.Ecs.Gameplay.LevelProgress.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UniGame.LeoEcsLite.LeoEcs.Shared.Components;
    using UniModules.UniCore.Runtime.DataFlow;
    using UniModules.UniGame.Core.Runtime.DataFlow.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    [Serializable]
    public class GameViewContainerConverter : LeoEcsConverter
    {
        #region inspector
        
        [TitleGroup("settings")]
        public Transform parent;
        [TitleGroup("settings")]
        public Vector3 position;
        [TitleGroup("settings")]
        public Vector3 rotation = Quaternion.identity.eulerAngles;
        [TitleGroup("settings")]
        public Vector3 scale = Vector3.one;
        [TitleGroup("settings")]
        public List<AssetReferenceGameObject> views = new List<AssetReferenceGameObject>();

        [TitleGroup("default view")]
        public bool activateOnConvert = false;
        
        [TitleGroup("default view")]
        [ShowIf(nameof(activateOnConvert))]
        public bool enableDefaultView = false;
        
        [TitleGroup("default view")]
        [ShowIf(nameof(enableDefaultView))]
        [ShowIf(nameof(activateOnConvert))]
        public GameObject view;
        
        [Space]
        [ValueDropdown(nameof(GetViews))]
        public AssetReferenceGameObject selection;

        #endregion
        
        private LifeTimeDefinition _lifeTime;
        private EcsWorld _world;
        private int _entity;

        public bool IsActive => Application.isPlaying && _world != null && _world.IsAlive();

        public sealed override void Apply(GameObject target, EcsWorld world, int entity)
        {
            _world = world;
            _entity = entity;
            
            _lifeTime ??= new LifeTimeDefinition()
                .AddTo(target.GetAssetLifeTime());
            _lifeTime.Release();

            ref var viewParentComponent = ref world.GetOrAddComponent<GameViewParentComponent>(entity);
            viewParentComponent.Parent = parent == null ? target.transform : parent;
            viewParentComponent.Position = position;
            viewParentComponent.Rotation = Quaternion.Euler(rotation);
            viewParentComponent.Scale = scale;

            if (!activateOnConvert) return;
            
            if (enableDefaultView && view != null)
            {
                //create default view by reference
                var viewEntity = world.NewEntity();
                ref var parentComponent = ref world.GetOrAddComponent<ParentEntityComponent>(viewEntity);
                ref var requestComponent = ref world.GetOrAddComponent<ActiveGameViewComponent>(entity);

                var viewPackedEntity = world.PackEntity(viewEntity);
                requestComponent.Value = viewPackedEntity;
                parentComponent.Value = world.PackEntity(entity);
                
                view.ConvertGameObjectToEntity(world, viewEntity);
                view.SetActive(true);
                return;
            }
            
            Activate();
        }

        [EnableIf(nameof(IsActive))]
        [Button]
        public void Activate()
        {
            if (selection == null || selection.RuntimeKeyIsValid() == false) return;

            var requestEntity = _world.NewEntity();
            ref var requestComponent = ref _world.GetOrAddComponent<ActivateGameViewRequest>(requestEntity);
            requestComponent.Source = _world.PackEntity(_entity);
            requestComponent.View = selection.AssetGUID;
        }
    
        private IEnumerable<ValueDropdownItem<AssetReferenceGameObject>> GetViews()
        {
#if UNITY_EDITOR
            if (view == null) selection = views.FirstOrDefault();
            
            foreach (var item in views)
            {
                yield return new ValueDropdownItem<AssetReferenceGameObject>()
                {
                    Text = item.editorAsset.name,
                    Value = item,
                };
            }
#endif
            yield break;
        }
        
    }
}