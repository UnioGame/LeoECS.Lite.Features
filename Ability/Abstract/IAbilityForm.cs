namespace Game.Code.Configuration.Runtime.Ability.Abstract
{
    using Leopotam.EcsLite;

    public interface IAbilityForm
    {
        void Execute(EcsSystems systems, int entity);
    }
}