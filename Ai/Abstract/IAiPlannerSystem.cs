namespace Game.Ecs.AI.Abstract
{
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Data;

    public interface IAiPlannerSystem : IAiPlannerSwitched
    {
        public UniTask Initialize(int id, IEcsSystems ecsSystems);
       
        void ApplyPlanningResult(IEcsSystems systems, int entity, AiPlannerData data);
    }

    public interface IAiPlannerSwitched
    {
        void RemoveComponent(IEcsSystems systems, int entity);
    }
    
}