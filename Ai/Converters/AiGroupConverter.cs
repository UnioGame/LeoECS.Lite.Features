namespace Game.Ecs.AI.Converters
{
    using System;
    using Components;
    using Leopotam.EcsLite;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UniGame.LeoEcs.Shared.Extensions;
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public class AiGroupConverter : IEcsComponentConverter
    {
        public string Name => nameof(AiGroupConverter);
        public bool IsEnabled => true;

        public void Apply(EcsWorld world, int entity)
        {
            world.AddComponent<AiGroupComponent>(entity);
        }

        public bool IsMatch(string searchString)
        {
            return searchString == Name;
        }
    }
}