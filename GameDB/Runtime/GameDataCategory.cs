namespace Game.Code.DataBase.Runtime
{
    using System;
    using System.Collections.Generic;
    using Abstract;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public abstract class GameDataCategory : ScriptableObject, IGameDataCategory
    {
        public string category;
        
        [InlineEditor()]
        public GameResourceLocation resourceLocation;

        public virtual string Category => category;

        public IGameResourceLocation ResourceLocation => resourceLocation;

        public abstract IReadOnlyList<IGameDatabaseRecord> Records { get; }

        public virtual IGameDatabaseRecord Find(string id)
        {
            foreach (var record in Records)
            {
                if (record.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                    return record;
                if(record.Name.Equals(id, StringComparison.OrdinalIgnoreCase))
                    return record;
            }

            return EmptyRecord.Value;
        }

        [Button(ButtonSizes.Large, Icon = SdfIconType.ArchiveFill)]
        public virtual void FillCategory() { }

    }
}