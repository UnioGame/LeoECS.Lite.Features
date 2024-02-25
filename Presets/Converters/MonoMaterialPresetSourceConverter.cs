namespace Game.Ecs.Presets.Converters
{
    using System.Threading;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

    public sealed class MonoMaterialPresetSourceConverter : MonoLeoEcsConverter
    {
        [HideLabel]
        [InlineProperty]
        public MaterialPresetSourceConverter converter = new MaterialPresetSourceConverter();
        
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            converter.Apply(world,entity);
        }
    }
}