namespace Game.Ecs.Gameplay.Tutorial.ActionTools
{
	using System.Collections.Generic;
#if UNITY_EDITOR
	using UniModules.Editor;
#endif

	public static class TutorialKeysTool
	{
		public static IEnumerable<TutorialKey> GetKey()
		{
			foreach (var id in GetKeys())
				yield return (TutorialKey)id;
		}

		public static IEnumerable<string> GetKeys()
		{
#if UNITY_EDITOR
			var settings = AssetEditorTools.GetAssets<TutorialKeysSetting>();

			foreach (var data in settings)
			{
				if(data == null) continue;
                
				foreach (var key in data.keys)
				{
					yield return key;
				}
			}
#endif
			yield break;
		}
	}
}