namespace Game.Ecs.Presets.Components
{
    using System;
    using Assets;
    
    /// <summary>
    /// game runtime rendering settings preset
    /// </summary>
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    [Serializable]
    public struct RenderingSettingsPresetComponent
    {
        public RenderingSettingsPreset Value;
    }
}