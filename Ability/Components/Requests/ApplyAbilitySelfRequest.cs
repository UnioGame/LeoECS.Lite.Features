namespace Game.Ecs.Ability.Common.Components
{
    using System;
    using Leopotam.EcsLite;

    /// <summary>
    /// Запрос применить целевое умение, умение должно принадлежать сущности.
    /// </summary>
    [Serializable]
    public struct ApplyAbilitySelfRequest
    {
        public EcsPackedEntity Value;
    }
}