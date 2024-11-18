namespace Game.Code.Services.AbilityLoadout.Data
{
	using System;
	using Cysharp.Text;
	using DataBase.Runtime.Abstract;
	using Sirenix.OdinInspector;
	using UniModules.UniCore.Runtime.Utils;

	[Serializable]
	public class AbilityRecord : IGameResourceRecord
	{
		public static AbilityRecord Empty = new AbilityRecord()
		{
			id = -1,
			name = "EMPTY",
			ability = new AssetReferenceAbility()
		};
		
		public string name;
		public int id;
		public int slotType;
		
		[TitleGroup("ability data")]
		[InlineProperty]
		[HideLabel]
		public AbilityData data = new AbilityData();
		
		[TitleGroup("ability behaviour")]
		[InlineProperty]
		[HideLabel]
		public AssetReferenceAbility ability;
		
		public string Name => name;

		public string Id => name;
		
		public bool CheckRecord(string filter)
		{
			return name.Equals(filter, StringComparison.OrdinalIgnoreCase);
		}

		public string Label => ZString.Format("{0} | ({1})",name,id);

		public bool IsMatch(string searchString)
		{
			if (string.IsNullOrEmpty(searchString)) return true;
			if (id.ToStringFromCache().IndexOf(searchString,StringComparison.OrdinalIgnoreCase) >= 0) return true;
			if (Name != null && Name.IndexOf(searchString,StringComparison.OrdinalIgnoreCase) >= 0) return true;

#if UNITY_EDITOR
			if (ability.EditorValue != null &&
			    ability.EditorValue.name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
#endif
			return false;
		}
	}
}