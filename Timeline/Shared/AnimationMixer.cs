namespace Game.Code.Timeline.Shared
{
    using System;

    [Serializable]
    public abstract class AnimationMixer<TBinding, TAnimationBehaviour>
    {
        public TBinding Binding;

        public virtual void SetupFrame(TBinding binding)
        {
            Binding = binding;
            OnSetupFrame(binding);
        }

        public abstract void Blend(TAnimationBehaviour behaviour, float inputWeight, float progress);

        public abstract void ApplyFrame();

        protected virtual void OnSetupFrame(TBinding binding) {}
    }
}