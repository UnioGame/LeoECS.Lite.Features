namespace Game.Ecs.Ability.SubFeatures.Target.Components
{
    using System.Collections.Generic;
    using Abstract;
    using Game.Code.Configuration.Runtime.Effects.Abstract;
    using Leopotam.EcsLite;

    public struct SplashEffectSourceComponent : IEcsAutoReset<SplashEffectSourceComponent>
    {
        public IZoneTargetsDetector ZoneTargetsDetector;
        public List<IEffectConfiguration> MainTargetEffects;
        public List<IEffectConfiguration> OtherTargetsEffects;
        
        public void AutoReset(ref SplashEffectSourceComponent c)
        {
            c.MainTargetEffects ??= new List<IEffectConfiguration>();
            c.MainTargetEffects.Clear();
            
            c.OtherTargetsEffects ??= new List<IEffectConfiguration>();
            c.OtherTargetsEffects.Clear();
        }
    }
}