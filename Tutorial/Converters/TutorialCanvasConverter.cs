namespace Game.Ecs.Gameplay.Tutorial.Converters
{
	using System;
	using System.Threading;
	using Components;
	using Leopotam.EcsLite;
	using UniGame.LeoEcs.Converter.Runtime;
	using UniGame.LeoEcs.Shared.Extensions;
	using UnityEngine;
	using UnityEngine.EventSystems;
	using UnityEngine.UI;

	[Serializable]
	public class TutorialCanvasConverter : LeoEcsConverter
	{
		public override void Apply(GameObject target, EcsWorld world, int entity)
		{
			var graphicRaycaster = target.GetComponentInParent<GraphicRaycaster>();
			var eventSystem = EventSystem.current;
			
			ref var graphicRaycasterComponent = ref world.AddComponent<TutorialGraphicRaycasterComponent>(entity);
			graphicRaycasterComponent.Value = graphicRaycaster;
			
			ref var eventSystemComponent = ref world.AddComponent<TutorialEventSystemComponent>(entity);
			eventSystemComponent.Value = eventSystem;
		}
	}
}