namespace Game.Code.Animations
{
    using EffectMilestones;
    using EffectMilestones.Timeline;
    using UniGame.Shared.Runtime.Timeline;
    using UnityEditor;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    public static class AnimationTool
    {
        public static readonly PlayableBinder Binder = new PlayableBinder();
        
        public static void ApplyBindings(PlayableDirector playableDirector, AnimationLink animationLink)
        {
            Binder.ResolveBindings(playableDirector, animationLink);
        }
        
        public static void ApplyBindings(PlayableDirector playableDirector, PlayableBindingData bindings)
        {
            Binder.ResolveBindings(playableDirector, bindings);
        }


        public static void ClearReferences(PlayableDirector director,PlayableAsset animation)
        {
            Binder.ClearReferences(director, animation);
        }

        public static void BakeAnimationLink(PlayableDirector playableDirector,AnimationLink animationLink)
        {
#if UNITY_EDITOR
            if(animationLink == null || playableDirector == null || animationLink.animation == null)
                return;
            
            Binder.BakeAnimationBinding(playableDirector, animationLink.animation, animationLink.bindingData);
            
            BakeMilestones(animationLink.milestones, animationLink.animation);
            
            EditorUtility.SetDirty(animationLink);
            AssetDatabase.SaveAssets();
#endif
        }

        public static void BakeMilestones(EffectMilestonesData milestonesInfo, PlayableAsset animation)
        {
            if (milestonesInfo == null || animation == null) return;
            
#if UNITY_EDITOR
            milestonesInfo.Clear();
            if(animation is not TimelineAsset playableAsset) return;
            
            var milestoneTrack = playableAsset.GetTrack<EffectMilestonesTrack>();
            var milestoneClips = milestoneTrack.GetClips<EffectMilestoneClip>();
            foreach (var milestoneClip in milestoneClips)
            {
                var milestoneTime = (float)milestoneClip.start;
                milestonesInfo.AddMilestone(milestoneTime);
            }
#endif
        }
        


    }
}