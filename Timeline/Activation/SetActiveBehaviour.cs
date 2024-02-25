namespace Game.Code.Timeline.Activation
{
    using System;
    using UnityEngine;
    using UnityEngine.Playables;

    [Serializable]
    public sealed class SetActiveBehaviour : PlayableBehaviour
    {
        [SerializeField]
        private bool _isActive;

        public bool IsActive => _isActive;
    }
}