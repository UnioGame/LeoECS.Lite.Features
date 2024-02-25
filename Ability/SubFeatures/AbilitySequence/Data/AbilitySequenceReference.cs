namespace Game.Ecs.Ability.SubFeatures.AbilitySequence.Data
{
    using System;
    using System.Collections.Generic;
    using Assets;
    using Game.Code.Services.Ability;
    using UniGame.AddressableTools.Runtime.AssetReferencies;

    [Serializable]
    public class AbilitySequenceReference
    {
        public string name;
        
        public List<AbilityConfigurationValue> sequence = new List<AbilityConfigurationValue>();
    }
    
}