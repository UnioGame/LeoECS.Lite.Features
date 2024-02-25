namespace Game.Code.Animations
{
    using System;

    public static class AnimationEditorData
    {
        public static event Action<AnimationLink> OnEditorOpen;

        public static void OpenEditor(AnimationLink link)
        {
            OnEditorOpen?.Invoke(link);
        }
    }
}