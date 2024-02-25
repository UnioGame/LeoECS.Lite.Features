namespace Game.Code.Configuration.Runtime.Ability.Description
{
    using Leopotam.EcsLite;

    public interface IAbilityBehaviour
    {
        void Compose(EcsWorld world, int abilityEntity, bool isDefault);
    }
}