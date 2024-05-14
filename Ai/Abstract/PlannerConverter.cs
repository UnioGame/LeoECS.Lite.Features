namespace Game.Ecs.AI.Abstract
{
    using System;
    using Data;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public abstract class PlannerConverter : GameObjectConverter, IPlannerConverter, IEntityConverter
    {

        [FormerlySerializedAs("id")]
        [FormerlySerializedAs("_id")] 
        [SerializeField]
        public ActionType actionId;
        
        public ActionType ActionId => actionId;

        protected int PlannerIndex;

        public new void Apply(GameObject target, EcsWorld world, int entity)
        {
            if (enabled == false)
                return;
            
            Apply(world, entity);
        }
    }


    [Serializable]
    public class PlannerConverter<TComponent> : PlannerConverter
        where TComponent : struct, IApplyableComponent<TComponent>
    {
        [InlineProperty]
        [SerializeField]
        [HideLabel]
        public TComponent data;
    
        protected sealed override void OnApply(GameObject target, EcsWorld world, int entity)
        {
            ref var component = ref world.GetOrAddComponent<TComponent>(entity);
            data.Apply(ref component);
            OnApplyComponents(target, world, entity);
        }
        
        protected virtual void OnApplyComponents(GameObject target, EcsWorld world, int entity)
        {
        }

    }
}