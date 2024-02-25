namespace Game.Ecs.Presets.SpotLightSettings.Components
{
    using Assets;
    using System;
    /// <summary>
    /// game spot light shader settings preset
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct SpotLightSettingsPresetComponent
    {
        public SpotLightPresets Value;

    }
}