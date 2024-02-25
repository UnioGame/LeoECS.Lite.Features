namespace Game.Ecs.Effects
{
    using System.Collections.Generic;
    using Code.Configuration.Runtime.Effects;
    using Code.Configuration.Runtime.Effects.Abstract;
    using Components;
    using Leopotam.EcsLite;

    public static class EffectsExtensions
    {
        public static void CreateRequests(this IEnumerable<IEffectConfiguration> effects, EcsWorld world, EcsPackedEntity source,EcsPackedEntity destination)
        {
            if(effects == null)
                return;
            foreach (var effect in effects)
                effect.CreateRequest(world,ref source,ref destination);
        }

        public static int CreateRequest(this IEffectConfiguration effect, EcsWorld world,ref EcsPackedEntity source,ref  EcsPackedEntity destination)
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