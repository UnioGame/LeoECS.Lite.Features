namespace Animations.Animatror
{
    using UnityEngine;

    public static class AnimatorExtension
    {
        public static bool IsPlaying(this Animator animator, string stateName)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }
        
        public static bool IsPlaying(this Animator animator, int layerIndex, string stateName)
        {
            return animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        }
        
        public static bool IsPlaying(this Animator animator, int layerIndex, int stateNameHash)
        {
            return animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash == stateNameHash;
        }
        
        public static bool IsPlaying(this Animator animator, int layerIndex = 0)
        {
            return animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime < 1;
        }
        //todo добавить методы для проверки конкретного значения компонента AnimaitonLiveComponent
    }
}