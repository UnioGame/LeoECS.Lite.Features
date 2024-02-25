namespace Game.Ecs.Effects
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Code.Configuration.Runtime.Effects;
    using Code.Configuration.Runtime.Effects.Abstract;
    using Components;
    using Leopotam.EcsLite;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public static class EffectsExtensions
    {
#if ENABLE_IL2CPP
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void CreateRequests(this List<IEffectConfiguration> effects, 
            EcsWorld world, 
            EcsPackedEntity source,
            EcsPackedEntity destination)
        {
            if(effects == null) return;
            
            foreach (var effect in effects)
                effect.CreateRequest(world,ref source,ref destination);
        }

#if ENABLE_IL2CPP
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int CreateRequest(this IEffectConfiguration effect,
            EcsWorld world,
            ref EcsPackedEntity source,
            ref EcsPackedEntity destination)
        {
            var requestPool = world.GetPool<CreateEffectSelfRequest>();
            var effectsEntity = world.NewEntity();
                
            ref var request = ref requestPool.Add(effectsEntity);
            request.Source = source;
            request.Destination = effect.TargetType == TargetType.Self ? source : destination;
            request.Effect = effect;
            return effectsEntity;
        }
    }
}