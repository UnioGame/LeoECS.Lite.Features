namespace Game.Ecs.Presets.SpotLightSettings.Converters
{
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;
    
    public sealed class MonoSpotLightSettingsPresetSourceConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        [InlineProperty]
        [HideLabel]
        public SpotLightSettingsSourceConverter spotLightConverter = new SpotLightSettingsSourceConverter();

        public sealed override void Apply(GameObject target, EcsWorld world, int entity)
        {
            spotLightConverter.Apply(world, entity);
        }
    }
}