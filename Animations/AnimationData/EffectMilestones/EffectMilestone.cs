namespace Game.Code.Animations.EffectMilestones
{
    using System;

    [Serializable]
    public struct EffectMilestone
    {
        public float Time;
        public bool IsApplied;

        public EffectMilestone Clone()
        {
            return new EffectMilestone
            {
                Time = Time,
                IsApplied = IsApplied
            };
        }
    }
}