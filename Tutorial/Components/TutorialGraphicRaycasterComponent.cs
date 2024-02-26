namespace Game.Ecs.Gameplay.Tutorial.Components
{
	using System;
	using UnityEngine.Serialization;
	using UnityEngine.UI;

	/// <summary>
	/// ADD DESCRIPTION HERE
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct TutorialGraphicRaycasterComponent
	{
		public GraphicRaycaster Value;
	}
}