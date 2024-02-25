namespace Game.Ecs.Ability.SubFeatures.Area.Behaviours
{
    using System;
    using Code.Configuration.Runtime.Ability.Description;
    using Components;
    using Leopotam.EcsLite;
    using Target.Components;
    using UnityEngine;
    using UserInput.Components;

    [Serializable]
    public sealed class AreableBehaviour : IAbilityBehaviour
    {
        [SerializeField]
        private float _areaRadius;
        [SerializeField]
        private GameObject _areaView;
        
        public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
        {
            var targetsPool = world.GetPool<AbilityTargetsComponent>();
            targetsPool.Add(abilityEntity);

            var areablePool = world.GetPool<AreableAbilityComponent>();
            areablePool.Add(abilityEntity);

            var areaRadiusPool = world.GetPool<AreaRadiusComponent>();
            ref var areaRadius = ref areaRadiusPool.Add(abilityEntity);
            areaRadius.Value = _areaRadius;

            var areaRadiusViewPool = world.GetPool<AreaRadiusViewComponent>();
            ref var areaRadiusView = ref areaRadiusViewPool.Add(abilityEntity);
            areaRadiusView.View = _areaView;
            
            var upPool = world.GetPool<CanApplyWhenUpInputComponent>();
            
            if(!isDefault && !upPool.Has(abilityEntity))
            {
                upPool.Add(abilityEntity);
            }
        }

        public void DrawGizmos(GameObject target)
        {
        }
    }
}