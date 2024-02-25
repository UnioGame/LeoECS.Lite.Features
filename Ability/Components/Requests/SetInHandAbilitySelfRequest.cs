namespace Game.Ecs.Ability.Common.Components
{
    using Leopotam.EcsLite;
    using UnityEngine.Serialization;

    /// <summary>
    /// Запрос "положить" в руку умение по энтити. Умение должно принадлежать этому же чемпиону.
    /// </summary>
    public struct SetInHandAbilitySelfRequest
    {
        public EcsPackedEntity Value;
    }
}