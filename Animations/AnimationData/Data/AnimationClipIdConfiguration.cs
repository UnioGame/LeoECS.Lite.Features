namespace Game.Code.Animations
{
    using System.Collections.Generic;
    using System.Linq;
    using Sirenix.OdinInspector;
    using UnityEngine;

    /// <summary>
    /// Configuration asset for AnimationClipId
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Configuration/Animation/Animation Clip Id Configuration",
        fileName = "Animations clips IDs configuration")]
    public class AnimationClipIdConfiguration : ScriptableObject
    {
        #region inspector

        [Header("default state")]
        [HideLabel]
        public AnimationClipId defaultState;

        #endregion
        public List<string> ids= new List<string>();
    }
}