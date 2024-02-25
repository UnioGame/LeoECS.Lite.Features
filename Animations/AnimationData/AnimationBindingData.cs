namespace Game.Code.Animations
{
    using Sirenix.OdinInspector;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Animation Binding Data", menuName = "Game/Animation/Animation Binding Data")]
    public sealed class AnimationBindingData : ScriptableObject
    {
        [SerializeField]
        [InlineProperty]
        [HideLabel]
        public PlayableBindingData data = new PlayableBindingData();
    }
    
}