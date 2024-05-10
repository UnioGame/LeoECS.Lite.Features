namespace Game.Ecs.AI.Abstract
{
    using Shared.Generated;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;

    public interface IPlannerConverter : IEcsComponentConverter
    {
        ActionType ActionId { get; }
    }
}