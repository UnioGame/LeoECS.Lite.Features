namespace Game.Ecs.Input
{
    using Systems;
    using Components.Ability;
    using Components.Direction;
    using Cysharp.Threading.Tasks;
    using JetBrains.Annotations;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Map.Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;

    [UsedImplicitly]
    public sealed class InputFeature : LeoEcsSystemAsyncFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            ecsSystems.Add(new MapSpaceInitializeSystem());
            ecsSystems.DelHere<DirectionInputEvent>();
            ecsSystems.Add(new DirectionRawMapConvertSystem());

            ecsSystems.DelHere<AbilityCellVelocityEvent>();
            ecsSystems.Add(new AbilityVelocityRawConvertSystem());
            
            ecsSystems.Add(new ProcessAbilityActiveTimeSystem());
            ecsSystems.Add(new ClearAbilityInputStateSystem());

            return UniTask.CompletedTask;
        }
    }
}