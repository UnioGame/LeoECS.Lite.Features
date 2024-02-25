namespace Game.Ecs.Ability.SubFeatures.Target.Behaviours
{
	using System;
	using Code.Configuration.Runtime.Ability.Description;
	using Code.GameTools.Runtime;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Converter.Runtime.Abstract;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;
	using UnityEngine.Serialization;

	[Serializable]
	public sealed class CircleZoneDetectionBehaviour : IAbilityBehaviour, ILeoEcsGizmosDrawer
	{
		public Vector2 offset;
		public float radius;
		
		public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
		{
			var zoneDetectionPool = world.GetPool<CircleZoneDetectionComponent>();
            ref var zoneDetectionComponent = ref zoneDetectionPool.Add(abilityEntity);
            zoneDetectionComponent.Offset = offset;
            zoneDetectionComponent.Radius = radius;
		}

		public void DrawGizmos(GameObject target)
		{
#if UNITY_EDITOR
			ZoneDetectionMathTool.DrawGizmos(target, offset, radius);
#endif
		}
	}
}