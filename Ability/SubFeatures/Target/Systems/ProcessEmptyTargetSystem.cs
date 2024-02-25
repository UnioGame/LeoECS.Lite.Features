namespace Game.Ecs.Ability.SubFeatures.Target.Systems
{
    using Aspects;
    using Components;
    using Leopotam.EcsLite;
    using TargetSelection;
    using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
    
    /// <summary>
    /// Add an empty target to an ability
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [ECSDI]
    public class ProcessEmptyTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filterAbilityTarget;
        private EcsFilter _filterEmptyTarget;

        private TargetAbilityAspect _targetAspect;
        
        private EcsPool<AbilityTargetsComponent> _abilityTargetPool;
        private EcsPool<SoloTargetComponent> _soleTargetPool;
        private EcsPool<EmptyTargetComponent> _emptyTargetPool;
        
        private int _emptyEntity;
        
        private EcsPackedEntity[] _targets = new EcsPackedEntity[TargetSelectionData.MaxTargets];

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            
            _filterEmptyTarget = _world
                .Filter<EmptyTargetComponent>()
                .End();

            _filterAbilityTarget = _world
                .Filter<AbilityTargetsComponent>()
                .Inc<NonTargetAbilityComponent>()
                .End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var abilityTargetsEntity in _filterAbilityTarget)
            {
                ref var abilityTargetsComponent = ref _targetAspect.AbilityTargets.Get(abilityTargetsEntity);
                if(abilityTargetsComponent.Count > 0) continue;
                
                ref var soloTargetComponent = ref _targetAspect.SoloTarget.Get(abilityTargetsEntity);
                var counter = 0;

                foreach (var emptyTargetEntity in _filterEmptyTarget)
                {
                    if (counter >= TargetSelectionData.MaxTargets) break;
                    
                    var emptyTargetPack = _world.PackEntity(emptyTargetEntity);
                    _targets[counter] = emptyTargetPack;
                    
                    soloTargetComponent.Value = emptyTargetPack;
                    counter++;
                }

                abilityTargetsComponent.SetEntities(_targets,counter);
            }
        }
    }

}