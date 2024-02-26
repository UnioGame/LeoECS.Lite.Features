namespace Game.Ecs.Gameplay.Tutorial.Tools
{
	using UnityEngine;

	public class TutorialTool
	{
		public static void DrawGizmos(Rect rect)
		{
#if UNITY_EDITOR
			UnityEditor.Handles.color = Color.yellow;
			UnityEditor.Handles.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), 
				new Vector3(rect.size.x, rect.size.y, 0.01f));
#endif
		}
	}
}