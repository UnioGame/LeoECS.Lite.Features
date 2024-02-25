namespace Game.Code.Configuration.Runtime.Ability.Abstract
{
    using Leopotam.EcsLite;

    public interface IAbilityAdjustmentType
    {
        void Execute(EcsSystems systems, int entity);
    }
}