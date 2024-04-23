namespace Game.Ecs.AI.Abstract
{
    using System;
    using System.Threading;
    using Configurations;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Shared.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public abstract class PlannerConverter : GameObjectConverter,IPlannerConverter, IEntityConverter
    {

        [FormerlySerializedAs("_id")] 
        [SerializeField]
        public AiAgentActionId id;
        
        public AiAgentActionId Id => id;

        public void Apply(GameObject target, EcsWorld world, int entity)
        {
            if (enabled == false)
                return;
            
            Apply(world, entity);
            OnApply(target,world,entity);
        }
        
        protected virtual void OnApply(GameObject target, EcsWorld world, int entity, CancellationToken cancellationToken = default)
        {
            
        }

        public virtual void Apply(EcsWorld world, int entity)
        {
            
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