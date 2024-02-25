namespace Game.Ecs.Input.Components.Ability
{
    using Leopotam.EcsLite;

    public struct AbilityUpInputRequest : IEcsAutoReset<AbilityUpInputRequest>
    {
        public int InputId;
        public float ActiveTime;
        public void AutoReset(ref AbilityUpInputRequest c)
        {
            c.ActiveTime = 0.0f;
        }
    }
}