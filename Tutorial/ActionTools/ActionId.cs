namespace Game.Ecs.Gameplay.Tutorial.ActionTools
{
	using System;
	using Sirenix.OdinInspector;
	using UnityEngine;

	[Serializable]
#if ODIN_INSPECTOR
	[ValueDropdown("@Game.Ecs.Gameplay.Tutorial.ActionTools.ActionTool.GetActionIds()",IsUniqueList = true,DropdownTitle = "Action Id")]
#endif
	public class ActionId
	{
		[SerializeField]
		public string value = string.Empty;

		public static implicit operator string(ActionId v)
		{
			return v.value;
		}

		public static explicit operator ActionId(string v)
		{
			return new ActionId { value = v };
		}
		
		public override string ToString() => value;
	}
}