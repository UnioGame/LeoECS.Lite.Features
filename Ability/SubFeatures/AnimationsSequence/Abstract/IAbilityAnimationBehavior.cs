namespace Game.Ecs.Ability.SubFeatures.CriticalAnimations.Abstract
{
    using Leopotam.EcsLite;

    public interface IAbilityAnimationBehavior
    {
        public void Compose(EcsWorld world, int abilityEntity, bool isDefault);
    }
}