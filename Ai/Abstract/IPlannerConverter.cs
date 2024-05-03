using Game.Ecs.AI.Configurations;
using UniGame.LeoEcs.Converter.Runtime.Abstract;

namespace Game.Ecs.AI.Abstract
{
    public interface IPlannerConverter : IEcsComponentConverter
    {
        AiAgentActionId Id { get; }
    }
}