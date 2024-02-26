namespace Game.Ecs.Gameplay.Tutorial.ActionTools
{
	using System;
	using Sirenix.OdinInspector;
	using UnityEngine;

	[Serializable]
#if ODIN_INSPECTOR
	[ValueDropdown("@Game.Ecs.Gameplay.Tutorial.ActionTools.TutorialKeysTool.GetKey()",IsUniqueList = true,DropdownTitle = "Tutorial Key")]
#endif
	public class TutorialKey
	{
		[SerializeField]
		public string value = string.Empty;

		public static implicit operator string(TutorialKey v)
		{
			return v.value;
		}

		public static explicit operator TutorialKey(string v)
		{
			return new TutorialKey { value = v };
		}
		
		public override string ToString() => value;
	}
}