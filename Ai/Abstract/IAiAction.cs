namespace Game.Ecs.AI.Abstract
{
    using Leopotam.EcsLite;
    using Data;

    public interface IAiAction
    {
        AiActionResult Execute( EcsSystems systems,int entity);
    }
}