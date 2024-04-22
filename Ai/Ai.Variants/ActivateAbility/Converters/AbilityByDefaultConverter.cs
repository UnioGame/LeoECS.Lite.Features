namespace Game.Ecs.GameAi.ActivateAbility.Converters
{
    using System;
    using AI.Abstract;
    using Components;

    [Serializable]
    public class AbilityByDefaultConverter 
        : ComponentPlannerConverter<AbilityByDefaultComponent>, IAbilityByConverter
    {
    }
}