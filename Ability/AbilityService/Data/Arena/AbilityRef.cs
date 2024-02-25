namespace Game.Code.Services.Ability.Data.Arena
{
    using System;
    using Game.Code.Services.AbilityLoadout.Data;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    public class AbilityRef
    {
        [TableColumnWidth(50, resizable: false)]
        public int level;
        [TableColumnWidth(150, resizable: true)]
        public AssetReferenceAbility ability;
        [HideInInspector]
        public int id;
    }
}