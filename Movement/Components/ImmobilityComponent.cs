namespace Game.Ecs.Movement.Components
{
    using Leopotam.EcsLite;

    public struct ImmobilityComponent : IEcsAutoReset<ImmobilityComponent>
    {
        public int BlockSourceCounter;
        
        public void AutoReset(ref ImmobilityComponent c)
        {
            c.BlockSourceCounter = 0;
        }
    }
}