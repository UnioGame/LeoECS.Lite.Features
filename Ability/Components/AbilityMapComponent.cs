namespace Game.Ecs.Ability.Common.Components
{
    using System;
    using System.Collections.Generic;
    using Leopotam.EcsLite;
    using Sirenix.OdinInspector;

    /// <summary>
    /// Компонент со ссылками на доступные умения у сущности.
    /// </summary>
    [Serializable]
    public struct AbilityMapComponent : IEcsAutoReset<AbilityMapComponent>
    {
        // TODO: https://dreamfrost.atlassian.net/browse/IDLE-907
        public static int AbilitiesSlotsCount = 5;
        
        [InlineProperty]
        [ListDrawerSettings]
        public List<EcsPackedEntity> AbilityEntities;

        public void AutoReset(ref AbilityMapComponent c)
        {
            c.AbilityEntities ??= new List<EcsPackedEntity>(AbilitiesSlotsCount);
            c.AbilityEntities.Clear();
            for (int i = 0; i < AbilitiesSlotsCount; i++)
            {
                if (i < c.AbilityEntities.Count)
                    c.AbilityEntities[i] = new EcsPackedEntity();
                else
                    c.AbilityEntities.Add(new EcsPackedEntity());
            }
        }
    }
}