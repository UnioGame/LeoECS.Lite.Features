namespace Game.Ecs.Effects.Data
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

#if UNITY_EDITOR
    using Converters;
    using UnityEditor;
    using UniModules.Editor;
#endif
    
    [CreateAssetMenu(menuName = "Game/Effects/Effects Root Configuration", fileName = "Effects Root Configuration")]
    public class EffectsRootConfiguration : ScriptableObject
    {
        [InlineProperty]
        [HideLabel]
        public EffectsRootData data;

        [FolderPath] 
        public string[] bakeLocations = Array.Empty<string>();

        [ButtonGroup(nameof(BakeAll))]
        public void BakeAll()
        {
#if UNITY_EDITOR
            var targets = AssetEditorTools.GetAssets<GameObject>(bakeLocations);
            foreach (var target in targets)
            {
                Bake(target);
            }
#endif
        }
        
#if UNITY_EDITOR
        public void Bake(GameObject target)
        {
            var converter = target.GetComponent<EffectRootMonoConverter>();
            converter = converter == null ? target.AddComponent<EffectRootMonoConverter>() : converter;
                
            converter.BakeActive();
            converter.MarkDirty();
            target.MarkDirty();
        }
#endif
    }
}