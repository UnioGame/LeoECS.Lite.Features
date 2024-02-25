namespace Game.Code.Configuration.Runtime.Effects
{
    using System;

    [Serializable]
    public enum ViewInstanceType : byte
    {
        Head = 0,
        Body = 1,
        Feet = 2,
        Hand = 3,
        Weapon = 4
    }
}