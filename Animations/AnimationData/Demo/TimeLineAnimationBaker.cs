namespace Game.Ecs.Tools.Converters
{
    using Code.Animations;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using UnityEngine.Playables;

#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    
    public class TimeLineAnimationBaker : MonoBehaviour
    {
        [PropertySpace(8)]
        [InlineEditor]
        public AnimationLink animationLink;

        [PropertySpace(8)]
        public PlayableDirector director;

        public bool IsDataAvailable => director !=null && animationLink != null && animationLink.animation != null;
        
        [ButtonGroup]
        [EnableIf(nameof(IsDataAvailable))]
        public void Bake()
        {
            AnimationTool.BakeAnimationLink(director, animationLink);
        }

        [ButtonGroup]
        [EnableIf(nameof(IsDataAvailable))]
        public void Apply()
        {
            AnimationTool.ApplyBindings(director, animationLink);
        }

        [ButtonGroup]
        [EnableIf(nameof(IsDataAvailable))]
        public void ClearTimeline()
        {
            AnimationTool.ClearReferences(director, animationLink.animation);
        }
        
        [Button]
        public void ClearBacking()
        {
            animationLink?.Clear();
        }
        
        
    }
}