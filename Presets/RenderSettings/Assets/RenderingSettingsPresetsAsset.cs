namespace Game.Ecs.Presets.Assets
{
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Serialization;

    [CreateAssetMenu(menuName = "Game/Presets/LightingPreset", fileName = "LightingPreset")]
    public class RenderingSettingsPresetsAsset : ScriptableObject
    {
        [FormerlySerializedAs("settings")]
        [InlineProperty]
        [HideLabel]
        public RenderingSettingsPreset settingsPreset = new RenderingSettingsPreset();
    }
}