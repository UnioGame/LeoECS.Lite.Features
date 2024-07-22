namespace Game.Code.Animations
{
    using System;
    using System.Linq;
    using Cysharp.Threading.Tasks;
    using PlayableBindings;
    using Resolvers;
    using UniGame.AddressableTools.Runtime;
    using UniGame.Core.Runtime;
    using UniModules.UniGame.Core.Runtime.DataFlow.Extensions;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;
    
    [Serializable]
    public class ControlTrackResolver : OutputTrackResolver<ControlTrack,ControlTrackReference>
    {
        protected override void OnResolve(PlayableDirector director,ControlTrack track,ControlTrackReference reference)
        {
            foreach (var clip in track.GetClips())
            {
                if(clip.asset is not ControlPlayableAsset clipAsset) continue;
                
                var clipReference = reference.Clips.FirstOrDefault(x => x.Clip == clipAsset);
                if(clipReference == null) continue;
                
                var prefabReference = clipReference.Prefab;
                        
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    clipAsset.prefabGameObject = prefabReference.editorAsset;
                    continue;
                }
#endif
                ResolvePrefabAsync(clipAsset, prefabReference, director.gameObject.GetLifeTime())
                    .Forget();
            }
            
        }

        private async UniTask ResolvePrefabAsync(
            ControlPlayableAsset clipAsset,
            AssetReferenceGameObject assetReferenceGameObject,
            ILifeTime directorLifeTime)
        {
            clipAsset.prefabGameObject = await assetReferenceGameObject
                .LoadAssetTaskAsync(directorLifeTime);
        }
    }
}