namespace Game.Code.Configuration.Runtime.Ability.Abstract
{
    using Leopotam.EcsLite;

    public interface IAbilitySourceType
    {
        void Execute(EcsSystems systems, int entity);
    }
}