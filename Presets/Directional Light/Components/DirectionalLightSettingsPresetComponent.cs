namespace Game.Ecs.Presets.Directional_Light.Components
{
    using Assets;
    using System;

    /// <summary>
    /// game directional light shader settings preset
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct DirectionalLightSettingsPresetComponent
    {
        public DirectionalLightPresets Value;
    }
}