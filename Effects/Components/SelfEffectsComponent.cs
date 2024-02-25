namespace Game.Ecs.Effects.Components
{
	using System.Collections.Generic;
	using Code.Configuration.Runtime.Effects.Abstract;
	using Leopotam.EcsLite;

	public struct SelfEffectsComponent : IEcsAutoReset<SelfEffectsComponent>
	{
		public List<IEffectConfiguration> Effects;
        
		public void AutoReset(ref SelfEffectsComponent c)
		{
			c.Effects ??= new List<IEffectConfiguration>();
			c.Effects.Clear();
		}
	}
}