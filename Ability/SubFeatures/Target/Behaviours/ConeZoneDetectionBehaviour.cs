namespace Game.Ecs.Ability.SubFeatures.Target.Behaviours
{
	using System;
	using Code.Configuration.Runtime.Ability.Description;
	using Code.GameTools.Runtime;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Converter.Runtime.Abstract;
	using UnityEngine;
	using UnityEngine.Serialization;

	[Serializable]
	public class ConeZoneDetectionBehaviour : IAbilityBehaviour, ILeoEcsGizmosDrawer
	{
		public float angle;
		public float distance;
		
		public void Compose(EcsWorld world, int abilityEntity, bool isDefault)
		{
			var zoneDetectionPool = world.GetPool<ConeZoneDetectionComponent>();
			ref var zoneDetectionComponent = ref zoneDetectionPool.Add(abilityEntity);
			zoneDetectionComponent.Angle = angle;
			zoneDetectionComponent.Distance = distance;
		}

		public void DrawGizmos(GameObject target)
		{
#if UNITY_EDITOR
			ZoneDetectionMathTool.DrawGizmos(target, angle, distance);
#endif
		}
	}
}