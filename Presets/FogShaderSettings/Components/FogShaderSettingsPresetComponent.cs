namespace Game.Ecs.Presets.FogShaderSettings.Components
{
    using Assets;
    using System;

    /// <summary>
    /// game runtime fog shader settings preset
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct FogShaderSettingsPresetComponent
    {
        public FogShaderPresets Value;
    }
}