namespace Game.Ecs.AI.Configurations
{
    using System;
    using System.Collections;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    
#if ODIN_INSPECTOR
    [ValueDropdown("@Game.Ecs.AI.Configurations.AiAgentActionId.GetActionsId()",IsUniqueList = true)]
#endif
    [Serializable]
    public class AiAgentActionId
    {
        [SerializeField]
        private int _value;

        public int Value => _value;

        public static implicit operator int(AiAgentActionId v)
        {
            return v._value;
        }

        public static explicit operator AiAgentActionId(int v)
        {
            return new AiAgentActionId { _value = v };
        }

        public override string ToString() => _value.ToString();

        public override int GetHashCode() => _value;

        public AiAgentActionId FromInt(int data)
        {
            _value = data;
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj is AiAgentActionId mask)
                return mask._value == _value;
            
            return false;
        }
        
        #region editor
    
        public static IEnumerable GetActionsId()
        {
#if UNITY_EDITOR
            var configurationAsset = AssetEditorTools.GetAsset<AiConfigurationAsset>();
            if(configurationAsset == null)
                yield break;

            var configuration = configurationAsset.configuration;
            var actions = configuration.aiActions;
            
            for (var i = 0; i < actions.Length; i++)
            {
                var action = actions[i];
                yield return new ValueDropdownItem<AiAgentActionId>(action.name,(AiAgentActionId)i);
            }
#endif
            yield break;
        }

        #endregion
    }
}