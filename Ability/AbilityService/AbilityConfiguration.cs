namespace Game.Code.Configuration.Runtime.Ability
{
    using System.Collections.Generic;
    using Animations;
    using Description;
    // using Ecs.Animation.Data;
    using Sirenix.OdinInspector;
    using UniGame.AddressableTools.Runtime.AssetReferencies;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Serialization;

#if UNITY_EDITOR
    using UnityEditor;
    using UniModules.Editor;
#endif

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
        // [SerializeField] public AnimationClipId animationClipId;

        [PropertySpace(8)]
        [TitleGroup("Animation")]
        [InlineProperty]
        [HideLabel]
        [ShowIf(nameof(useAnimation))]
        public AddressableValue<AnimationLink> animationLink;
        
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