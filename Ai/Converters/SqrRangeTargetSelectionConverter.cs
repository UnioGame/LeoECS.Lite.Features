using Game.Ecs.TargetSelection;
using Leopotam.EcsLite;
using UniGame.LeoEcs.Shared.Extensions;
using Unity.VisualScripting;

namespace Game.Ecs.AI.Converters
{
    using System;
    using Abstract;
    using Game.Ecs.TargetSelection.Components;
    using Unity.IL2CPP.CompilerServices;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public class SqrRangeTargetSelectionConverter : AiCommonConverter<SqrRangeTargetsSelectionComponent>
    {
        public override void Apply(EcsWorld world, int entity)
        {
            ref var component = ref world.AddComponent<SqrRangeTargetsSelectionComponent>(entity);
            Value.Apply(ref component);
            for (int i = 0; i < component.Results.Length; i++)
            {
                ref var result = ref component.Results[i];
                result.Values = new EcsPackedEntity[TargetSelectionData.MaxTargets];
            }
        }
    }
}