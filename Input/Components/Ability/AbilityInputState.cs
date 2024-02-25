namespace Game.Ecs.Input.Components.Ability
{
    using Leopotam.EcsLite;

    internal struct AbilityInputState : IEcsAutoReset<AbilityInputState>
    {
        public int InputId;
        public float ActiveTime;
        
        public void AutoReset(ref AbilityInputState c)
        {
            c.InputId = -1;
            c.ActiveTime = 0.0f;
        }
    }
}