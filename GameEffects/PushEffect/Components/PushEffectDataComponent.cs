namespace Game.Ecs.GameEffects.PushEffect.Components
{
	using System;
	using DG.Tweening;
	using UnityEngine.Serialization;

	/// <summary>
	/// Push effect data component
	/// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
	[Serializable]
	public struct PushEffectDataComponent
	{
		[FormerlySerializedAs("ForwardFromSource")]
		[FormerlySerializedAs("ForwardDirection")]
		public bool FromSource;
		public float Distance;
		public float DurationOffset;
		public Ease Ease;
	}
}