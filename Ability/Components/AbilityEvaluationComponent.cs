namespace Game.Ecs.Ability.Common.Components
{
    using Leopotam.EcsLite;

    /// <summary>
    /// Компонент состояния оценки умения.
    /// </summary>
    public struct AbilityEvaluationComponent : IEcsAutoReset<AbilityEvaluationComponent>
    {
        public float EvaluateTime;
        
        public void AutoReset(ref AbilityEvaluationComponent c)
        {
            c.EvaluateTime = 0.0f;
        }
    }
}