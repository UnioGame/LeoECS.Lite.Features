namespace Game.Ecs.Gameplay.Tutorial.Converters
{
	using System;
	using System.Threading;
	using Components;
	using Configurations;
	using Leopotam.EcsLite;
	using Sirenix.OdinInspector;
	using Tools;
	using UniGame.LeoEcs.Converter.Runtime;
	using UniGame.LeoEcs.Converter.Runtime.Abstract;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;
	using UnityEngine.Serialization;

	[Serializable]
	public class TutorialTriggerPointConverter : LeoEcsConverter, ILeoEcsGizmosDrawer
	{
		#region Inspector
		
		[Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
		[InlineEditor]
		[SerializeReference]
		public TutorialTriggerPointConfiguration configuration;

		#endregion
		
		public override void Apply(GameObject target, EcsWorld world, int entity)
		{
			world.AddComponent<TutorialTriggerPointComponent>(entity);
			configuration.Apply(world, entity);
		}


		public void DrawGizmos(GameObject target)
		{
#if UNITY_EDITOR
			foreach (var converterValue in configuration.StartTriggerActions)
			{
				if (converterValue is ILeoEcsGizmosDrawer gizmosDrawer)
					gizmosDrawer.DrawGizmos(target);
			}
			
			foreach (var converterValue in configuration.FinalTriggerActions)
			{
				if (converterValue is ILeoEcsGizmosDrawer gizmosDrawer)
					gizmosDrawer.DrawGizmos(target);
			}
#endif
		}
	}
}