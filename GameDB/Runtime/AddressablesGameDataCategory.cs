namespace Game.Code.DataBase.Runtime
{
    using System.Collections.Generic;
    using Abstract;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game/Game Data/Addressable Category",fileName = "Addressable Category Asset")]
    public class AddressablesGameDataCategory : GameDataCategory, IGameDataCategory
    {
        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        public List<AddressablesObjectRecord> records = new List<AddressablesObjectRecord>();

        public override IReadOnlyList<IGameDatabaseRecord> Records => records;
    }
}