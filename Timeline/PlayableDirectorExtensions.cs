namespace Game.Code.Timeline
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UniGame.Core.Runtime;
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    public static class PlayableDirectorExtensions
    {
        public static bool SetRootSpeed(this PlayableDirector playableDirector, float value)
        {
            if (!playableDirector.playableGraph.IsValid()) return false;
            playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(value);
            return true;
        }
        
        public static bool SetSpeed(this PlayableDirector playableDirector, int index, float value)
        {
            if (!playableDirector.playableGraph.IsValid()) return false;
            playableDirector.playableGraph.GetRootPlayable(index).SetSpeed(value);
            return true;
        }

        public static async UniTask<PlayableDirector> PlayAsync(this PlayableDirector director,
            TimelineAsset animation,
            ILifeTime lifeTime,
            DirectorWrapMode wrapMode = DirectorWrapMode.Hold)
        {
            director = await director
                .PlayDirector(animation,wrapMode)
                .AddTo(lifeTime)
                .AwaitForTimerFinished(lifeTime, true);
            
            return director;
        }

        public static PlayableDirector AddTo(this PlayableDirector director,ILifeTime lifeTime)
        {
            lifeTime.AddCleanUpAction(() =>
            {
                if (director == null) return;
                director.Stop();
            });
            
            return director;
        }

        public static PlayableDirector PlayDirector(this PlayableDirector director,
            TimelineAsset animation,
            DirectorWrapMode wrapMode = DirectorWrapMode.Hold)
        {
            director.Stop();
                
            if(!director.playableGraph.IsValid())
                director.RebuildGraph();
                
            director.Play(animation, wrapMode);

            return director;
        }
        
        public static async UniTask<PlayableDirector> AwaitForFinished(this PlayableDirector director)
        {
            while (director.state == PlayState.Playing)
                await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);

            return director;
        }

        public static async UniTask<PlayableDirector> AwaitForTimerFinished(this PlayableDirector director, ILifeTime lifeTime, bool ignoreTimeScale)
        {
            var duration = director.duration;
            await UniTask.Delay(TimeSpan.FromSeconds(duration), ignoreTimeScale, cancellationToken: lifeTime.Token);
            return director;
        }
    }
}