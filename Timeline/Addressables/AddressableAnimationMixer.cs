namespace Game.Code.Timeline.Addressables
{
    using System;
    using Shared;
    using UnityEngine;

    [Serializable]
    public class AddressableAnimationMixer<TBehaviour> 
        : AnimationMixer<GameObject, TBehaviour>
        where TBehaviour : AddressableAnimationBehaviour
    {
        private GameObject _target;
        
        protected override void OnSetupFrame(GameObject binding)
        {
            _target = binding;
        }
        
        public override void Blend(TBehaviour behaviour, float inputWeight, float progress)
        {
            if (Application.isPlaying == false || _target == null) return;

            var state = behaviour.state;
            
            if(state.isLoaded && state.singleLoadPerLifeTime) return;

            var progressPassed = progress >= state.loadOnProgress;
            if (!progressPassed) return;
            
            state.counter++;
            state.isLoaded = true;

            behaviour.Load(_target,inputWeight,progress);
        }

        public override void ApplyFrame()
        {
            
        }
    }
}