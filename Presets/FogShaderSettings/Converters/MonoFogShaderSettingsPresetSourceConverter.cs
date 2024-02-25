namespace Game.Ecs.Presets.FogShaderSettings.Converters
{
    using System.Threading;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UnityEngine;
    public sealed class MonoFogShaderSettingsPresetSourceConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        [InlineProperty]
        [HideLabel]
        public FogShaderSettingsSourceConverter fogShaderConverter = new FogShaderSettingsSourceConverter();

        public sealed override void Apply(GameObject target, EcsWorld world, int entity)
        {
            fogShaderConverter.Apply(world, entity);
        }
    }
}