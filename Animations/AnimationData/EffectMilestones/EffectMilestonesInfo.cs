namespace Game.Code.Animations.EffectMilestones
{
    using Sirenix.OdinInspector;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Effect Milestones Info", menuName = "Game/Configurations/Ability/Effect Milestones Info")]
    public sealed class EffectMilestonesInfo : ScriptableObject
    {
        [SerializeField]
        [InlineProperty]
        [HideLabel]
        public EffectMilestonesData data = new EffectMilestonesData();

        public void AddMilestone(float time) => data.AddMilestone(time);

        public void Clear() => data.Clear();
    }
}