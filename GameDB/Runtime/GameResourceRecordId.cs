namespace Game.Code.DataBase.Runtime
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [InlineProperty]
    [Serializable]
    [ValueDropdown("@Game.Code.DataBase.Runtime.GameDataBaseAsset.GetGameRecordIds()")]
    public struct GameResourceRecordId
    {
        public readonly static GameResourceRecordId Empty = new GameResourceRecordId() {value = string.Empty};
        
        [SerializeField, HideInInspector] 
        public string value;

        public static implicit operator string(GameResourceRecordId v) => v.value;

        public static explicit operator GameResourceRecordId(string v)
        {
            return new GameResourceRecordId { value = v };
        }

        public override string ToString() => value;

        public override int GetHashCode() => string.IsNullOrEmpty(value) ? 0 : value.GetHashCode();

        public GameResourceRecordId FromString(string value)
        {
            this.value = value;
            return this;
        }

        public override bool Equals(object obj)
        {
            if (!string.IsNullOrEmpty(value) && obj is GameResourceRecordId gameResourceId)
                return value.Equals(gameResourceId.value);

            return false;
        }
    }
}