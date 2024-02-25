namespace Game.Ecs.Presets.Demo
{
    using System.Collections.Generic;
    using Game.Ecs.Presets.Assets;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class DemoSceneSettingsChanger : MonoBehaviour
    {
        [FoldoutGroup("Runtime")] 
        [HideLabel]
        public RenderingSettingsPreset runtime;

        [FoldoutGroup("Source")] 
        [HideLabel]
        public RenderingSettingsPreset source;
        
        public List<RenderingSettingsPreset> settings = new List<RenderingSettingsPreset>();

        [Button]
        public void Apply()
        {
            runtime.ApplyToRendering();
        }

        [Button]
        public void Reset()
        {
            runtime.ApplySettings(source);
        }
        
#if UNITY_EDITOR
        [Button]
        private void RemoveHighlight()
        {

        }
#endif
    }
}