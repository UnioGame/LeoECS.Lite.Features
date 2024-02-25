namespace Game.Code.Configuration.Runtime.Ability
{
    using Leopotam.EcsLite;
    using UnityEngine;

    public abstract class AbilityFormConfiguration : ScriptableObject
    {
        public abstract void Run(EcsSystems systems);
    }
}