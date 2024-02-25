namespace Game.Editor.Runtime.CharacteristicsViewer
{
    using System;
    using Sirenix.OdinInspector;

    [InlineProperty]
    [HideLabel]
    [Serializable]
    public class EcsCharacteristicDebugView
    {
        [FoldoutGroup("$Name")]
        [InlineProperty]
        [HideLabel]
        [ShowIf(nameof(IsActive))]
        public CharacteristicValue Value;

        public virtual bool IsActive { get; set; }
        
        public string Name { get; set; }
        
        public void UpdateView()
        {
            Value = CreateView();
        }

        [FoldoutGroup("$Name")]
        [ButtonGroup("$Name/Commands", Stretch = true)]
        [Button]
        public virtual void Recalculate()
        {
            
        }
        
        public virtual CharacteristicValue CreateView()
        {
            return new CharacteristicValue();
        }
    }
}