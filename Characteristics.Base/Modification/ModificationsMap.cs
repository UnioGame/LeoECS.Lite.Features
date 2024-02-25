namespace Game.Ecs.Characteristics.Base.Modification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Sirenix.OdinInspector;

    [Serializable]
    public class ModificationsMap : IModificationsMap
    {
        #region inspector
        
        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        [ListDrawerSettings(ListElementLabelName = "id")]
        public List<ModificationData> modifications = new List<ModificationData>();

        #endregion
        
        #region private
        
        private Dictionary<Type,ModificationData> _modificationDataCache = new Dictionary<Type, ModificationData>();

        #endregion
        
        public IEnumerable<string> Modifications => modifications.Select(x => x.id);

        
        public ModificationHandler Create(Type type, float value)
        {
            var modificationData = GetFromCache(type);
            var result = modificationData?.factory.Create(value);
            return result;
        }

        public ModificationInfo GetModificationInfo(Type type)
        {
            var modificationData = GetFromCache(type);
            return modificationData == null 
                ? ModificationInfo.Empty 
                : modificationData.info;
        }

        public ModificationHandler Create(string type, float value)
        {
            var modificationData = modifications
                .FirstOrDefault(x => x.id.Equals(type, StringComparison.OrdinalIgnoreCase));
            if (modificationData == null) return null;
            var result = modificationData.factory.Create(value);
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ModificationData GetFromCache(Type modificationType)
        {
            if (_modificationDataCache.TryGetValue(modificationType, out var modificationData))
                return modificationData;
            
            modificationData = modifications
                .FirstOrDefault(x => x.ModificationType == modificationType);
            _modificationDataCache[modificationType] = modificationData;
            
            return modificationData;
        }

    }
}