namespace Game.Ecs.GameAi.MoveToTarget.Components
{
    using System;
    using UniGame.LeoEcs.Shared.Abstract;

    [Serializable]
    public struct MoveByPoiComponent : IApplyableComponent<MoveByPoiComponent>
    {
        /// <summary>
        /// Расстояние после которого будет считаться, что достигли цели
        /// </summary>
        public float ReachRange;
        
        public void Apply(ref MoveByPoiComponent component)
        {
            component.ReachRange = ReachRange;
        }
    }
}