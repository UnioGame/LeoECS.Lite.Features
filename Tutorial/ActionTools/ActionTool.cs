namespace Game.Ecs.Gameplay.Tutorial.ActionTools
{
	using System.Collections.Generic;
#if UNITY_EDITOR
	using UniModules.Editor;
#endif

	/// <summary>
	/// Show all action ids in editor
	/// </summary>
	public static class ActionTool
	{
		public static IEnumerable<ActionId> GetActionIds()
		{
			foreach (var id in GetActionNames())
				yield return (ActionId)id;
		}

		public static IEnumerable<string> GetActionNames()
		{
#if UNITY_EDITOR
			var settings = AssetEditorTools.GetAssets<ActionIdsSetting>();

			foreach (var data in settings)
			{
				if(data == null) continue;
                
				foreach (var actionId in data.ids)
				{
					yield return actionId;
				}
			}
#endif
			yield break;
		}
	}
}