namespace Animations.Animatror
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Animator.Data;
    using Cysharp.Threading.Tasks;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;
    using Systems;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;
    using Object = UnityEngine.Object;

#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditor.Animations;
#endif
    
    /// <summary>
    /// Animation feature for 2d game and animator controller
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Feature/Gameplay/AnimationFeature", fileName = "Animation Feature")]
    public class AnimationFeature : BaseLeoEcsFeature
    {
#if UNITY_EDITOR
        
        [TitleGroup("Animator Setup")]
        [NonSerialized]
        [OnValueChanged("@GetGUID($value)")]
        [OnInspectorGUI]
        private Object _baseUnitAnimator;
        private void GetGUID(Object asset)
        {
            var path = AssetDatabase.GetAssetPath(asset);
            var guid = AssetDatabase.AssetPathToGUID(path);
            _animatorGuid = guid;
            GameLog.Log($"GUID form _baseUnityAnimator: {guid}");
        }
        private string _animatorGuid;
#endif
        [TitleGroup("Animator Setup")]
        public AnimationsIdsMap animationsIdsMap;
        
        private AnimatorsMap GetAnimatorsMap()
        {
            return new AnimatorsMap
            {
                data = animationsIdsMap.animations.ToDictionary(x => x.clipId, x => new StateAndClipData
                {
                    stateNameHash = Animator.StringToHash(x.stateName),
                    clipName = x.clipName
                })
            };
        }
        
        [TitleGroup("Animator Setup")]
        [GUIColor(0,0.85f,0)]
        [PropertySpace(5f, 15f)]
        [Button("Update Animator States", ButtonSizes.Large, ButtonStyle.CompactBox, Icon = SdfIconType.ArrowCounterclockwise)]
        public void UpdateAnimatorStates()
        {
#if UNITY_EDITOR
            animationsIdsMap.animations.Clear();
            var baseUnitAnimator =
                AssetDatabase.LoadAssetAtPath<AnimatorController>(AssetDatabase.GUIDToAssetPath(_animatorGuid));
            var animatorRuntimeAnimatorController = baseUnitAnimator;
            for (int i = 0; i < baseUnitAnimator.layers.Length; i++)
            {
                var childAnimatorStates = animatorRuntimeAnimatorController.layers[i].stateMachine.states;
                foreach (var state in childAnimatorStates)
                {
                    var stateName = state.state.name;
                    var clip = state.state.motion as AnimationClip;
                    if (clip == null)
                    {
                        continue;
                    }
                    animationsIdsMap.animations.Add(new AnimationClipData
                    {
                        stateName = stateName,
                        clipName = clip.name
                    });
                }
            }
            EditorUtility.SetDirty(this);
#endif
        }
        [PropertyOrder(99)]
        public List<BaseLeoEcsFeature> subFeatures;
        public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var stateAndClipDatas = GetAnimatorsMap();
            ecsWorld.SetGlobal(stateAndClipDatas);
            ecsSystems.Add(new PlayAnimationTroughAnimatorSystem());
            ecsSystems.Add(new CalculateAnimationsLengthSystem());
            foreach (var subFeature in subFeatures)
            {
                await subFeature.InitializeFeatureAsync(ecsSystems);
            }
        }
    }
}