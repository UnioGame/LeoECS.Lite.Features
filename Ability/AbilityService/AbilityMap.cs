namespace Game.Code.Configuration.Runtime.Ability
{
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [CreateAssetMenu(fileName = "Ability Map", menuName = "Game/Configurations/Ability/Ability Map")]
    public class AbilityMap : ScriptableObject
    {
        [InlineEditor()]
        [SerializeField]
        private List<AbilityConfiguration> _abilityConfigurations;

        public IReadOnlyList<AbilityConfiguration> Abilities => _abilityConfigurations;
    }
}