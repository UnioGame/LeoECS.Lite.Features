using System;

namespace Game.Ecs.GameEffects.ModificationEffect.Components
{
    using System.Collections.Generic;
    using Characteristics.Base.Modification;
    using Leopotam.EcsLite;

    [Serializable]
    public struct ModificationEffectComponent : IEcsAutoReset<ModificationEffectComponent>
    {
        public List<ModificationHandler> ModificationHandlers;
        
        public void AutoReset(ref ModificationEffectComponent c)
        {
            c.ModificationHandlers ??= new List<ModificationHandler>();
            c.ModificationHandlers.Clear();
        }
    }
    
    [Serializable]
    public struct SingleModificationEffectComponent : IEcsAutoReset<SingleModificationEffectComponent>
    {
        public ModificationHandler Value;
        
        public void AutoReset(ref SingleModificationEffectComponent c)
        {
            c.Value = null;
        }
    }
}