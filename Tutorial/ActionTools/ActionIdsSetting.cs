namespace Game.Ecs.Gameplay.Tutorial.ActionTools
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Serialization;

	[CreateAssetMenu(menuName = "Game/Tutorial/Action Ids Setting", fileName = nameof(ActionIdsSetting))]
	public class ActionIdsSetting : ScriptableObject
	{
		public List<ActionId> ids = new List<ActionId>();
	}
}