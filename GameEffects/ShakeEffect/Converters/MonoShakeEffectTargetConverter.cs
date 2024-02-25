namespace Game.Ecs.GameEffects.ShakeEffect.Converters
{
    using System;
    using System.Threading;
    using Components;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using UniGame.LeoEcs.Converter.Runtime;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;

    using UniGame.LeoEcs.Converter.Runtime.Converters;
    using UniGame.LeoEcs.Shared.Extensions;

    public sealed class MonoShakeEffectTargetConverter : MonoLeoEcsConverter<ShakeEffectTargetConverter>
    {
        [TitleGroup("runtime: debug")]
        [InlineProperty]
        [HideLabel]
        public ShakeEffectDataComponent shakeData = new ShakeEffectDataComponent()
        {
            Duration = 2f,
            Strength = new Vector3(2f,2f,2f),
            Vibrato = 10,
            Random = 90f,
            Snapping = true,
            FadeOut = true
        };

        [TitleGroup("runtime: debug")]
        [ShowIf(nameof(IsRuntime))]
        public bool shakeTargetOnly = true;
        
        [Button]
        [EnableIf(nameof(IsRuntime))]
        public void Shake()
        {
            if (!World.IsAlive())
                return;

            var targetEntity = World.NewEntity();
            ref var dataComponent = ref World.AddComponent<ShakeEffectDataComponent>(targetEntity);
            
            dataComponent.Duration = shakeData.Duration;
            dataComponent.Strength = shakeData.Strength;
            dataComponent.Vibrato = shakeData.Vibrato;
            dataComponent.Random = shakeData.Random;
            dataComponent.Snapping = shakeData.Snapping;
            dataComponent.FadeOut = shakeData.FadeOut;

            if (!shakeTargetOnly) return;
            
            ref var targetComponent = ref World.AddComponent<ShakeEffectTargetComponent>(targetEntity);
            targetComponent.Value = transform;
        }
    }
    
    /// <summary>
    /// create shake default target
    /// </summary>
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public class ShakeEffectTargetConverter : LeoEcsConverter
    {
        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            ref var targetComponent = ref world.AddComponent<ShakeEffectDefaultTargetComponent>(entity);
        }

    }
}