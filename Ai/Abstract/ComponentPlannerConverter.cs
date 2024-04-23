namespace Game.Ecs.AI.Abstract
{
    using System;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    public abstract class ComponentPlannerConverter : EcsComponentConverter, IEntityConverter
    {
        public void Apply(GameObject target, EcsWorld world, int entity)
        {
            Apply(world, entity);
            OnApply(target, world, entity);
        }

        protected virtual void OnApply(GameObject target, EcsWorld world, int entity)
        {

        }
    }
    
    [Serializable]
    public class ComponentPlannerConverter<TComponent> : ComponentPlannerConverter
        where TComponent : struct, IApplyableComponent<TComponent>
    {
        [FoldoutGroup(nameof(data))]
        [InlineProperty]
        [SerializeField]
        [HideLabel]
        public TComponent data;
    
        protected override void OnApply(GameObject target, EcsWorld world, int entity)
        {
            ref var component = ref world.GetOrAddComponent<TComponent>(entity);
            data.Apply(ref component);
        }

        public override void Apply(EcsWorld world, int entity)
        {
            
        }
    }
}