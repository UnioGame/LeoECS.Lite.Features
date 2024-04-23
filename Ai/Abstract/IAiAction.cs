namespace Game.Ecs.AI.Abstract
{
    using Leopotam.EcsLite;
    using Service;

    public interface IAiAction
    {
        AiActionResult Execute( EcsSystems systems,int entity);
    }
}