namespace Game.Ecs.AI.Converters
{
    using System;
    using Unity.IL2CPP.CompilerServices;
    using Abstract;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Shared.Extensions;
    using UnityEngine;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public class AgroRangeConverter : AiCommonConverter<AiAgroRangeComponent>
    {
        [SerializeField]
        private float _range;

        public override void Apply(EcsWorld world, int entity)
        {
            ref var agroRangeComponent = ref world.AddComponent<AiAgroRangeComponent>(entity);
            agroRangeComponent.RangeSqr = _range * _range;
        }
    }
}