namespace Game.Ecs.GameEffects.ManaEffect.Behaviours
{
	using System;
	using Code.Configuration.Runtime.Ability.Description;
	using Effects;
	using Leopotam.EcsLite;
	using ShakeEffect.Components;
	using Sirenix.OdinInspector;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;

	/// <summary>
	/// <summary>Shakes a Transform's localPosition with the given values.</summary>
	/// <param name="duration">The duration of the tween</param>
	/// <param name="strength">The shake strength on each axis</param>
	/// <param name="vibrato">Indicates how much will the shake vibrate</param>
	/// <param name="randomness">Indicates how much the shake will be random (0 to 180 - values higher than 90 kind of suck, so beware).
	/// Setting it to 0 will shake along a single direction.</param>
	/// <param name="snapping">If TRUE the tween will smoothly snap all values to integers</param>
	/// <param name="fadeOut">If TRUE the shake will automatically fadeOut smoothly within the tween's duration, otherwise it will not</param>
	/// </summary>
	[Serializable]
	public class CameraShakeConfiguration : EffectConfiguration
	{
		[TitleGroup("shake data")]
		[Tooltip("The shake strength on each axis")]
		public Vector3 strength = new Vector3(2f,2f,2f);
		
		[TitleGroup("shake data")]
		[Tooltip("Indicates how much will the shake vibrate")]
		public int vibrato = 10;
		
		[TitleGroup("shake data")]
		[Range(0,180)]
		[Tooltip("Indicates how much the shake will be random (0 to 180 - values higher than 90 kind of suck, so beware. Setting it to 0 will shake along a single direction")]
		public float random = 90f;
		
		[TitleGroup("shake data")]
		[Tooltip("If TRUE the tween will smoothly snap all values to integers")]
		public bool snapping = true;
		
		[TitleGroup("shake data")]
		[Tooltip("If TRUE the shake will automatically fadeOut smoothly within the tween's duration, otherwise it will not")]
		public bool fadeOut = true;

		protected override void Compose(EcsWorld world, int effectEntity)
		{
			ref var effectComponent = ref world.AddComponent<ShakeEffectDataComponent>(effectEntity);

			effectComponent.Duration = duration;
			effectComponent.Vibrato = vibrato;
			effectComponent.Random = random;
			effectComponent.Snapping = snapping;
			effectComponent.FadeOut = fadeOut;
			effectComponent.Strength = strength;
		}
	}
}