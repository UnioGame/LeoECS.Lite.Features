namespace Game.Ecs.Gameplay.Tutorial.Actions.RestrictUITapAreaAction.Systems
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Aspects;
	using Components;
	using Leopotam.EcsLite;
	using Tutorial.Components;
	using UniGame.Core.Runtime.Extension;
	using UniGame.Runtime.ObjectPool.Extensions;
	using UnityEngine;
	using UnityEngine.Pool;
	using UniGame.LeoEcs.Bootstrap.Runtime.Attributes;
	using UnityEngine.EventSystems;
	using UnityEngine.UI;

	/// <summary>
	/// ADD DESCRIPTION HERE
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	[ECSDI]
	public class ProcessRestrictUITapAreaSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private RestrictUITapAreaActionAspect _aspect;
		private EcsFilter _filter;
		
		private List<RaycastResult> _raycastResults;
		private EventSystem _eventSystem;

		public void Init(IEcsSystems systems)
		{
			_world = systems.GetWorld();
			_filter = _world
				.Filter<ActivateRestrictUITapAreaComponent>()
				.End();
			_raycastResults = new List<RaycastResult>();
			_eventSystem = EventSystem.current;
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _filter)
			{
				if (!Input.GetMouseButtonDown(0)) 
					continue;
				ref var restrictUITapArea = ref _aspect.RestrictUITapArea.Get(entity);
				var rect = restrictUITapArea.Value.Rect;
				var pass = restrictUITapArea.Value.Passages;
				var mouseScreenPosition = Input.mousePosition;
				var isPointInside = rect.Contains(mouseScreenPosition);
				if (!isPointInside)
					continue;
				
				_raycastResults.Clear();
				var pointerEventData = new PointerEventData(_eventSystem)
				{
					position = Input.mousePosition
				};
				
				_eventSystem.RaycastAll(pointerEventData, _raycastResults);
				foreach (var result in _raycastResults)
				{
					if (pass == 0) continue;
					if (!result.gameObject) continue;
					PassTapToUI(pointerEventData, result.gameObject);
					pass--;
				}

				_aspect.CompletedRestrictUITapArea.Add(entity);
			}
		}
		
		private void PassTapToUI(BaseEventData eventData, GameObject targetGameObject)
		{
			ExecuteEvents.Execute(targetGameObject, eventData, ExecuteEvents.pointerClickHandler);
		}
	}
}