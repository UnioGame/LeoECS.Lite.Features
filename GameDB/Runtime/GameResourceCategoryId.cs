namespace Game.Code.DataBase.Runtime
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [InlineProperty]
    [Serializable]
    [ValueDropdown("@Game.Code.DataBase.Runtime.GameDataBaseAsset.GetGameRecordCategories()")]
    public struct GameResourceCategoryId
    {

        public readonly static GameResourceCategoryId Empty = new GameResourceCategoryId() {value = string.Empty};
        
        [SerializeField, HideInInspector] 
        public string value;

        public static implicit operator string(GameResourceCategoryId v) => v.value;

        public static explicit operator GameResourceCategoryId(string v)
        {
            return new GameResourceCategoryId { value = v };
        }

        public override string ToString() => value;

        public override int GetHashCode() => string.IsNullOrEmpty(value) ? 0 : value.GetHashCode();

        public GameResourceCategoryId FromString(string value)
        {
            this.value = value;
            return this;
        }

        public override bool Equals(object obj)
        {
            if (!string.IsNullOrEmpty(value) && obj is GameResourceCategoryId gameResourceId)
                return value.Equals(gameResourceId.value);

            return false;
        }
    }
}