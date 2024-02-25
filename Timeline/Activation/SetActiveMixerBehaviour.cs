namespace Game.Code.Timeline.Activation
{
    using System;
    using UnityEngine;
    using UnityEngine.Playables;

    [Serializable]
    public sealed class SetActiveMixerBehaviour : PlayableBehaviour
    {
        private readonly bool _endValue;
        private bool? _defaultState;

        private GameObject _target;

        public SetActiveMixerBehaviour()
        {
        }

        public SetActiveMixerBehaviour(bool endValue)
        {
            _endValue = endValue;
        }
        
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            _target = playerData as GameObject;
            if(_target == null)
                return;
            
            _defaultState ??= _target.activeSelf;
            
            var inputCount = playable.GetInputCount();
            
            var largestWeight = 0f;
            var currentIndex  = -1;
            
            for (var i = 0; i < inputCount; i++)
            {
                var weight = playable.GetInputWeight(i);
                if (weight > largestWeight)
                {
                    largestWeight = weight;
                    currentIndex  = i;
                }
            }

            if (currentIndex == -1)
            {
                _target.SetActive(_defaultState.Value);
                return;
            }
            
            var currentPlayable  = playable.GetInput(currentIndex);
            var currentBehaviour = ((ScriptPlayable<SetActiveBehaviour>) currentPlayable).GetBehaviour();

            var currentState = currentBehaviour.IsActive;

            if (_target.activeSelf != currentState)
                _target.SetActive(currentState);
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if(_target == null)
                return;
            
            if(Application.isPlaying)
                _target.SetActive(_endValue);
            else if(_defaultState != null)
                _target.SetActive(_defaultState.Value);
        }
    }
}