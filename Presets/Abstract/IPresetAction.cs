namespace Game.Ecs.Presets.Abstract
{
    public interface IPresetAction
    {
        void Bake();
        void ApplyToTarget();
    }
}