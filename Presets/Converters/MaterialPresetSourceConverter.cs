namespace Game.Ecs.Presets.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Abstract;
    using Components;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.AddressableTools.Runtime;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using Object = UnityEngine.Object;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif

    [Serializable]
    public sealed class MaterialPresetSourceConverter : EcsComponentConverter,IPresetAction
    {
        [ShowIf(nameof(isEnabled))]
        public string targetId;
        [ShowIf(nameof(isEnabled))]
        public AssetReferenceT<Material> materialReference;
        [ShowIf(nameof(isEnabled))]
        public float duration;
        
        [ShowIf(nameof(isEnabled))]
        [Space]
        [BoxGroup("editor")]
        public bool showButtons = true;

        public bool ButtonsEnabled => showButtons && isEnabled;
        
        public override void Apply(EcsWorld world, int entity)
        {
            ref var sourceComponent = ref world.GetOrAddComponent<MaterialPresetSourceComponent>(entity);
            ref var presetComponent = ref world.GetOrAddComponent<PresetComponent>(entity);
            ref var presetSourceComponent = ref world.GetOrAddComponent<PresetSourceComponent>(entity);
            ref var dataComponent = ref world.GetOrAddComponent<MaterialPresetComponent>(entity);
            ref var idComponent = ref world.GetOrAddComponent<PresetIdComponent>(entity);
            ref var durationComponent = ref world.GetOrAddComponent<PresetDurationComponent>(entity);
            ref var activePresetSource = ref world.GetOrAddComponent<ActivePresetSourceComponent>(entity);

            var worldLifeTime = world.GetWorldLifeTime();
            var material = materialReference.LoadAssetInstanceForCompletion(worldLifeTime);
            dataComponent.Value = material;
            idComponent.Value = targetId.GetHashCode();
            durationComponent.Value = duration;
        }

        [ButtonGroup()]
        [ShowIf(nameof(ButtonsEnabled))]
        public void Bake()
        {
#if UNITY_EDITOR
            var sourceMaterial = materialReference.editorAsset;
            if (sourceMaterial == null) return;
            var materials = GetSceneTargets();
            foreach (var target in materials)
            {
                sourceMaterial.Lerp(sourceMaterial,target,1);
                sourceMaterial.MarkDirty();
            }
#endif
        }
        
        [ButtonGroup()]
        [ShowIf(nameof(ButtonsEnabled))]
        public void ApplyToTarget()
        {
#if UNITY_EDITOR
            var sourceMaterial = materialReference.editorAsset;
            if (sourceMaterial == null) return;
            var materials = GetSceneTargets();
            foreach (var target in materials)
            {
                target.Lerp(target,sourceMaterial,1);
                target.MarkDirty();
            }
#endif
        }
        
        private List<Material> GetSceneTargets()
        {
            var result = new List<Material>();
            
#if UNITY_EDITOR
            
            var assets = Object
                .FindObjectsOfType<MonoMaterialPresetTargetConverter>()
                .Where(x => x.Converter.targetId == targetId)
                .ToList();

            foreach (var asset in assets)
            {
                var targetConverter = asset.Converter;
                var targetRenderer = targetConverter.renderer;
                if (targetRenderer == null)
                {
                    Debug.LogError($"EMPTY RENDERER IN {nameof(MonoMaterialPresetTargetConverter)} {asset.name}",asset);
                    return null;
                }
            
                var material = targetRenderer.sharedMaterials.FirstOrDefault();
                result.Add(material);
            }
#endif
            return result;
        }

    }
}