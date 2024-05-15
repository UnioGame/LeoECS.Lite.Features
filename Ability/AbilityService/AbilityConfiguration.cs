namespace Game.Code.Configuration.Runtime.Ability
{
    using System.Collections.Generic;
    using Animations;
    using Description;
    //using Ecs.Animation.Data;
    using Sirenix.OdinInspector;
    using UniGame.AddressableTools.Runtime.AssetReferencies;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Serialization;

#if UNITY_EDITOR
    using UnityEditor;
    using UniModules.Editor;
#endif
    
    public enum AnimationType
    {
        Animator = 0,
        PlayableDirector = 1
    }
    
    public enum FittingType
    {
        BasedOnClip = 0,
        BasedOnAbility = 1
    }
    [CreateAssetMenu(fileName = "Ability Configuration", menuName = "Game/Ability/Ability Configuration")]
    public sealed class AbilityConfiguration : ScriptableObject
    {
        [TitleGroup("Specification")]
        [InlineProperty]
        [HideLabel]
        [FormerlySerializedAs("_specification")] 
        [SerializeField]
        public AbilitySpecification specification;
        
        public bool useAnimation = true;
        
        [TitleGroup("Animation")]
        [InlineProperty]
        [PropertySpace(8)]
        [ShowIf(nameof(useAnimation))]
        [EnumToggleButtons]
        [InfoBox("Animation clip id witch triggered concrete animation", InfoMessageType.None,VisibleIf = "@animationType == AnimationType.Animator")]
        public AnimationType animationType;

        #region animator
        
        //[TitleGroup("Animation")]
        //[InlineProperty]
        //[HideLabel]
        //[ShowIf("@useAnimation && animationType == AnimationType.Animator")]
        //[SerializeField] public AnimationClipId animationClipId;

        [TitleGroup("Animation")]
        [InlineProperty]
        [HideLabel]
        [ShowIf("@useAnimation && animationType == AnimationType.Animator")]
        [EnumToggleButtons]
        public FittingType fittingType;

        #endregion
        
        #region playables director
        
        [PropertySpace(8)]
        [TitleGroup("Animation")]
        [InlineProperty]
        [HideLabel]
        [ShowIf(nameof(useAnimation))]
        [ShowIf("@useAnimation && animationType == AnimationType.PlayableDirector")]
        public AddressableValue<AnimationLink> animationLink;
        
        #endregion
        
        [TitleGroup("Animation")]
        [HideIf(nameof(useAnimation))]
        public float duration = 0.2f;
        
        [PropertySpace(8)]
        [FormerlySerializedAs("_abilityBehaviours")] 
        [SerializeReference]
        public List<IAbilityBehaviour> abilityBehaviours = new List<IAbilityBehaviour>();
        
        [SerializeField]
        public bool isBlocked;
        
    }
}