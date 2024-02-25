namespace Game.Code.Services.AbilityLoadout.Data
{
	using System.Linq;
	using Sirenix.OdinInspector;
	using UniGame.Core.Runtime;
	using UnityEngine;
	
#if UNITY_EDITOR
	using UniModules.Editor;
	using UnityEditor;
#endif
	
	[CreateAssetMenu(menuName = "Game/Configurations/Ability Item",fileName = nameof(AbilityItemAsset))]
	public class AbilityItemAsset : ScriptableObject, IUnique
	{
		public const string AbilityItemGroupName = "Ability Info";

		#region inspector
		
		[BoxGroup(AbilityItemGroupName, order:-1)]
		[InlineProperty]
		[HideLabel]
		public AbilityItemData data = new AbilityItemData();

		#endregion

		public int Id
		{
			get => data.Id;
			set => data.id = value;
		}

#if UNITY_EDITOR
		
		[Button]
		public void GetNewId()
		{
			var assets = AssetEditorTools.GetAssets<AbilityItemAsset>();
			var maxId = assets.Max(x => x.Id);
			data.id = maxId + 1;
			this.MarkDirty();
		}

		[OnInspectorInit]
		public void EditorInitialize()
		{
			if (data.id == 0)
			{
				GetNewId();
			}
		}

#endif
	}
}