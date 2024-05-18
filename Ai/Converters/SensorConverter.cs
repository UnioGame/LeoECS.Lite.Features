namespace Game.Ecs.AI.Converters
{
    using System;
    using Unity.IL2CPP.CompilerServices;
    using Abstract;
    using Components;

    /// <summary>
    /// ADD DESCRIPTION HERE
    /// </summary>
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public class SensorConverter : AiCommonConverter<AiSensorRangeComponent>
    {
    }
}