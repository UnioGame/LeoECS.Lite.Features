namespace Game.Ecs.Ability.AbilityUtilityView
{
    using System;
    using System.Collections.Generic;
    using Area.Systems;
    using Cysharp.Threading.Tasks;
    using Highlights.Components;
    using Highlights.Systems;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Radius.AggressiveRadius.Systems;
    using Radius.Component;
    using Radius.Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Serializable]
    [CreateAssetMenu(menuName = "Game/Feature/Ability/Ability Utility View Feature", 
        fileName = "Ability Utility View Feature")]
    public class AbilityUtilityViewFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            
            ecsSystems.Add(GetHighlightSystems(world));
            ecsSystems.Add(GetRadiusSystems(world));
            ecsSystems.Add(GetAreaSystems(world));
            
            return UniTask.CompletedTask;
        }

        private IEnumerable<IEcsSystem> GetHighlightSystems(EcsWorld world)
        {
            yield return new ProcessInHandAbilityHighlightSystem();
            yield return new ProcessNotInHandAbilityHighlightSystem();

            yield return new ProcessHighlightWhenDeadSystem();
            
            yield return new ProcessShowHighlightRequestSystem();
            yield return new DelHereSystem<ShowHighlightRequest>(world);

            yield return new ProcessHideHighlightRequestSystem();
            yield return new DelHereSystem<HideHighlightRequest>(world);
        }

        private IEnumerable<IEcsSystem> GetRadiusSystems(EcsWorld world)
        {
            yield return new ProcessInHandAbilityRadiusSystem();
            yield return new ProcessRadiusAreaAbilitySystem();
            yield return new ProcessRadiusForTargetAbilitySystem();
            yield return new ProcessNotInHandAbilityRadiusSystem();

            yield return new ProcessAggressiveAbilityRadiusSystem();
            yield return new ProcessAggressiveAbilityRadiusWhenDeadSystem();

            yield return new ProcessAbilityRadiusWhenOwnerDeadSystem();
            
            yield return new ProcessHideRadiusRequestSystem();
            yield return new DelHereSystem<HideRadiusRequest>(world);
            
            yield return new ProcessShowRadiusRequestSystem();
            yield return new DelHereSystem<ShowRadiusRequest>(world);
        }

        private IEnumerable<IEcsSystem> GetAreaSystems(EcsWorld world)
        {
            yield return new ShowAreaSystem();
            yield return new UpdateAreaPositionSystem();
            yield return new DestroyAreaByOwnerSystem();
            yield return new DestroyAreaSystem();
        }
    }
}