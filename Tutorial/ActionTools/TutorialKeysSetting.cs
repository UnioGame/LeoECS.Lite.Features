namespace Game.Ecs.Gameplay.Tutorial.ActionTools
{
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu(menuName = "Game/Tutorial/Tutorial Keys Setting", fileName = "Tutorial Keys Setting")]
	public class TutorialKeysSetting : ScriptableObject
	{
		public List<TutorialKey> keys = new List<TutorialKey>();
	}
}