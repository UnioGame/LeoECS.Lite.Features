namespace Game.Ecs.Characteristics.Base.Modification
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    [CreateAssetMenu(menuName = "Game/Modifications/Modifications Map",fileName = "Modifications Map")]
    public class ModificationsMapAsset : ScriptableObject,IModificationsMap
    {
        [HideLabel]
        [InlineProperty]
        public ModificationsMap map = new ModificationsMap();

        public IEnumerable<string> Modifications => map.Modifications;

        public ModificationInfo GetModificationInfo(Type type) => map.GetModificationInfo(type);

        public ModificationHandler Create(string type,float value) => map.Create(type,value);
        public ModificationHandler Create(Type type, float value) => map.Create(type, value);
    }
}