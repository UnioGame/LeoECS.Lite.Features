namespace Game.Ecs.AI
{
    using Abstract;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using UnityEngine;

    public class AiPlannerFeature : ScriptableObject
    {
        protected int _id;

        public int Id => _id;
        
        public async UniTask Initialize(int id,EcsSystems ecsSystems)
        {
            _id = id;

            await OnInitialize(id, ecsSystems);
        }
        
        protected virtual UniTask OnInitialize(int id, EcsSystems systems) => UniTask.CompletedTask;
    }
    
    public class AiPlannerFeature<TPlanner> : ScriptableObject
        where TPlanner : IAiPlannerSystem
    {
        #region inspector

        public TPlanner plannerSystem;

        #endregion
        
        protected int _id;

        public int Id => _id;
        
        public async UniTask Initialize(int id,EcsSystems ecsSystems)
        {
            _id = id;

            await OnInitialize(id, ecsSystems);
        }
        
        protected virtual UniTask OnInitialize(int id, EcsSystems systems) => UniTask.CompletedTask;
    }
}