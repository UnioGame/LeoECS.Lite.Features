namespace Game.Ecs.AI.Abstract
{
    using Data;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;

    public interface IPlannerConverter : IEcsComponentConverter
    {
        ActionType ActionId { get; }
    }
}