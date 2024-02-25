namespace Game.Ecs.Ability.SubFeatures.Target.Components
{
    using UnityEngine;

    /// <summary>
    /// The ability has a detection zone
    /// </summary>
    public struct RectangleZoneDetectionComponent
    {
        public Vector2 Offset;
        public Vector2 Size;
    }
}