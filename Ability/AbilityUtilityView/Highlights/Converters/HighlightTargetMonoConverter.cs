namespace Game.Ecs.Ability.AbilityUtilityView.Highlights.Converters
{
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime;
    using UnityEngine;

    public sealed class HighlightTargetMonoConverter : MonoLeoEcsConverter
    {
        [SerializeField]
        public GameObject highlight;

        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            var highlightPool = world.GetPool<HighlightComponent>();
            ref var highlightComponent = ref highlightPool.Add(entity);
            
            highlightComponent.Highlight = highlight;
        }
    }
}