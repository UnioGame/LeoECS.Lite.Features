namespace Game.Ecs.Presets
{
    using Game.Ecs.Presets.Directional_Light.Systems;
    using Game.Ecs.Presets.FogShaderSettings.Systems;
    using Game.Ecs.Presets.SpotLightSettings.Systems;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Systems;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Feature/Game Presets Feature", fileName = "Game Presets Feature")]
    public class PresetsFeature : BaseLeoEcsFeature
    {
        public override UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            //find active preset and target by id
            ecsSystems.Add(new FindPresetTargetsSystem());
            ecsSystems.Add(new CalculatePresetProgressSystem());

            //apply material preset to target
            ecsSystems.Add(new ApplyMaterialPresetToTargetSystem());

            //apply rendering settings preset to target
            ecsSystems.Add(new ApplyRenderingSettingsPresetSystem());

            //apply fog shader preset in game.
            ecsSystems.Add(new ApplyFogShaderSettingsPresetSystem());

            //apply spot light preset in game.
            ecsSystems.Add(new ApplySpotLightSettingsPresetSystem());
            
            //apply directional light preset in game.
            ecsSystems.Add(new ApplyDirectionalLightSettingsPresetSystem());

            //apply light preset to target
            ecsSystems.Add(new ApplyLightPresetSystem());

            //disable already activated presets
            //for new activation ActivePresetSourceComponent should be added on preset
            ecsSystems.Add(new DisableActivatedPresetsSystem());

            //if progress is completed when remove active status
            ecsSystems.Add(new CompletePresetProgressSystem());

            return UniTask.CompletedTask;
        }
    }
}