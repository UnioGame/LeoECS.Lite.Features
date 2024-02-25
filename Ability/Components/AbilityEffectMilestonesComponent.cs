namespace Game.Ecs.Ability.Common.Components
{
    using System;
    using Code.Animations.EffectMilestones;
    using Leopotam.EcsLite;

    public struct AbilityEffectMilestonesComponent : IEcsAutoReset<AbilityEffectMilestonesComponent>
    {
        public EffectMilestone[] Milestones;
        
        public void AutoReset(ref AbilityEffectMilestonesComponent c)
        {
            c.Milestones = Array.Empty<EffectMilestone>();
        }
    }
}