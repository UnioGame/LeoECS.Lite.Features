namespace Game.Ecs.Gameplay.Tutorial.Actions.RestrictUITapAreaAction.Data
{
	using System;
	using System.Collections.Generic;
	using Abstracts;
	using UnityEngine;
	using UnityEngine.Serialization;

	[Serializable]
	public class RestrictTapArea
	{
		public Rect Rect;
		public int Passages;
		public Vector2 AnchorPosition;
		public Vector2 Offset;
		[HideInInspector]
		public Rect FingerRect;
		public bool IsFlip;
		[FormerlySerializedAs("Duration")]
		public float Delay;
		[SerializeReference]
		public List<ITutorialAction> Actions;
	}
}