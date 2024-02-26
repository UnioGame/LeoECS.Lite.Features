namespace Game.Ecs.Gameplay.Tutorial.Actions.RestrictUITapAreaAction
{
	using System.Collections.Generic;
	using Abstracts;
	using ActionTools;
	using Components;
	using Data;
	using Leopotam.EcsLite;
	using Tools;
	using UniGame.LeoEcs.Converter.Runtime.Abstract;
	using UniGame.LeoEcs.Shared.Extensions;
	using UniGame.LeoEcs.ViewSystem.Extensions;
	using UniGame.UiSystem.Runtime.Settings;
	using UniModules.UniGame.UiSystem.Runtime;
	using UnityEngine;
	using UnityEngine.Serialization;

	public class RestrictUITapAreaActionConfiguration : TutorialAction, ILeoEcsGizmosDrawer
	{
		#region Ispector

		public ViewId View;
		public ViewType LayoutType = ViewType.Overlay;
		
		[SerializeReference]
		public List<RestrictTapArea> Areas = new List<RestrictTapArea>();
		
		public ActionId ActionId;

		#endregion
		
#if UNITY_EDITOR
		private Rect _debugRect;
#endif
		protected override void Composer(EcsWorld world, int entity)
		{
			ref var restrictUITapAreaComponent = ref world.AddComponent<RestrictUITapAreaActionComponent>(entity);
			var camera = Camera.main;
			foreach (var restrictTapArea in Areas)
			{
				var newTapArea = new RestrictTapArea()
				{
					Rect = CalculateNewRect(restrictTapArea, camera),
					FingerRect = CalculateOffset(restrictTapArea, camera),
					Offset = restrictTapArea.Offset,
					Passages = restrictTapArea.Passages,
					IsFlip = restrictTapArea.IsFlip,
					Delay = restrictTapArea.Delay,
					AnchorPosition = restrictTapArea.AnchorPosition,
					Actions = restrictTapArea.Actions
				};
				restrictUITapAreaComponent.Areas.Add(newTapArea);
			}
			restrictUITapAreaComponent.ActionId = ActionId;
			world.MakeViewRequest(View, LayoutType);
		}
		
		public void DrawGizmos(GameObject target)
		{
#if UNITY_EDITOR
			if (Areas == null || Areas.Count == 0)
				return;
			foreach (var restrictTapArea in Areas)
			{
				var camera = Camera.main;
				_debugRect = CalculateNewRect(restrictTapArea, camera);
				TutorialTool.DrawGizmos(_debugRect);
			}
#endif
		}

		public Rect CalculateNewRect(RestrictTapArea restrictTapArea, Camera camera)
		{
			var rectTapArea = restrictTapArea.Rect;
			var currentAspectRatio = (float)camera.pixelWidth / camera.pixelHeight;

			var referenceWidth = 720f;
			var referenceHeight = 1440f;
			var referenceAspectRatio = referenceWidth / referenceHeight;
			
			if (!Mathf.Approximately(currentAspectRatio, referenceAspectRatio))
			{
				referenceHeight = referenceWidth * camera.pixelHeight / camera.pixelWidth;
			}

			var scaleX = camera.pixelWidth / referenceWidth;
			var scaleY = camera.pixelHeight / referenceHeight;

			var width = rectTapArea.width * scaleX;
			var heightScale = camera.pixelHeight / referenceHeight;
			var height = rectTapArea.height * heightScale;

			var anchorPosition = restrictTapArea.AnchorPosition;
			var xPos = camera.pixelWidth * anchorPosition.x - rectTapArea.x * scaleX;
			var yPos = camera.pixelHeight * anchorPosition.y - rectTapArea.y * scaleY;
			return new Rect(xPos, yPos, width, height);
		}
		
		public Rect CalculateOffset(RestrictTapArea restrictTapArea, Camera camera)
		{
			var rectTapArea = restrictTapArea.Rect;
			var offset = restrictTapArea.Offset;
			var xPos = rectTapArea.x + offset.x;
			var yPos = rectTapArea.y + offset.y;
			var width = rectTapArea.width;
			var height = rectTapArea.height;
			return new Rect(xPos, yPos, width, height);
		}
	}
}