namespace Game.Ecs.Ability.SubFeatures.Target.Behaviours
{
    using System;
    using Ability.UserInput.Components;
    using Code.Configuration.Runtime.Ability.Description;
    using Components;
    using Leopotam.EcsLite;
    using Selection.Components;
    using UniGame.LeoEcs.Shared.Extensions;

    [Serializable]
    public sealed class ToTargetBehaviour : IAbilityBehaviour
    {
        public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
        {
            var selectablePool = world.GetPool<SelectableAbilityComponent>();
            selectablePool.Add(abilityEntity);

            var selectedTargetsPool = world.GetPool<SelectedTargetsComponent>();
            selectedTargetsPool.Add(abilityEntity);

            var targetablePool = world.GetPool<TargetableAbilityComponent>();
            targetablePool.Add(abilityEntity);
            
            var targetsPool = world.GetPool<AbilityTargetsComponent>();
            targetsPool.Add(abilityEntity);
            
            world.AddComponent<MultipleTargetsComponent>(abilityEntity);
            world.AddComponent<SoloTargetComponent>(abilityEntity);
            
            var upPool = world.GetPool<CanApplyWhenUpInputComponent>();
            
            if(!isDefault && !upPool.Has(abilityEntity))
            {
                upPool.Add(abilityEntity);
            }
        }
    }
}