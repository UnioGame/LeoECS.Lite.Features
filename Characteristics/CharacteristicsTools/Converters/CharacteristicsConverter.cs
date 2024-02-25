namespace Game.Ecs.Characteristics.CharacteristicsTools.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using AbilityPower.Converters;
    using ArmorResist.Converters;
    using Attack.Converters;
    using AttackSpeed.Converters;
    using Block.Converters;
    using CriticalChance.Converters;
    using CriticalMultiplier.Converters;
    using Dodge.Converters;
    using Health.Converters;
    using JetBrains.Annotations;
    using Leopotam.EcsLite;
    using Mana.Converters;
    using ManaRegeneration.Converters;
    using Sirenix.OdinInspector;
    using Speed.Converters;
    using SplashDamage.Converters;
    using UniGame.LeoEcs.Converter.Runtime;
    using UniGame.LeoEcs.Converter.Runtime.Abstract;
    using UniModules.UniCore.Runtime.ReflectionUtils;
    using UnityEngine;
    using UnityEngine.Serialization;

#if UNITY_EDITOR
    using Sirenix.Utilities.Editor;
#endif
    
    [Serializable]
    public sealed class CharacteristicsConverter : LeoEcsConverter, ILeoEcsGizmosDrawer, IEcsConverterProvider
    {
        [FormerlySerializedAs("Characteristics")]
        [ListDrawerSettings(ShowFoldout = true)]//OnBeginListElementGUI = nameof(BeginDrawListElement)
        [SerializeReference]
        [Required]
        [ItemNotNull]
        public List<LeoEcsConverter> characteristics = new List<LeoEcsConverter>();
        
        public IEnumerable<IEcsComponentConverter> Converters => characteristics;

        
        [Button]
        public void Reset()
        {
            var items = new List<LeoEcsConverter>()
            {
                new HealthComponentConverter(),
                new AttackConverter(),
                new AttackSpeedConverter(),
                new AttackRangeComponentConverter(),
                new CriticalChanceConverter(),
                new CriticalMultiplierConverter(),
                new DodgeComponentConverter(),
                new BlockComponentConverter(),
                new AbilityPowerConverter(),
                new ManaComponentConverter(),
                new ManaRegenerationConverter(),
                new SpeedConverter(),
                new SplashDamageConverter(),
                new ArmorResistConverter(),
            };

            characteristics.RemoveAll(x => x == null);
            
            foreach (var item in items)
            {
                var found = false;
                foreach (var characteristic in characteristics)
                {
                    found = characteristic.GetType() == item.GetType();
                    if(found) break;
                }
                if(found) continue;
                characteristics.Add(item);
            }
        }
        
        public T GetConverter<T>() where T : class
        {
            foreach (var converter in characteristics)
                if(converter is T result) return result;

            return null;
        }

        public void RemoveConverter<T>() where T : IEcsComponentConverter
        {
            characteristics.RemoveAll(x => x is T);
        }

        public IEcsComponentConverter GetConverter(Type target)
        {
            foreach (var converter in characteristics)
                if(converter.GetType() == target) return converter;
            return null;
        }

        public override void Apply(GameObject target, EcsWorld world, int entity)
        {
            foreach (var characteristic in characteristics)
            {
                characteristic.Apply(target, world, entity);
            }
        }

        public override bool IsMatch(string searchString)
        {
            var result = base.IsMatch(searchString);
            if (result) return true;
            foreach (var converter in characteristics)
                if (converter.IsMatch(searchString)) return true;

            return false;
        }

        public void DrawGizmos(GameObject target)
        {
            foreach (var converter in characteristics)
            {
                if(converter is not ILeoEcsGizmosDrawer drawer) continue;
                drawer.DrawGizmos(target);
            }
        }
        
        private void BeginDrawListElement(int index)
        {
#if UNITY_EDITOR
            var item =  characteristics[index];
            var label = item.GetType().PrettyName();
            SirenixEditorGUI.BeginBox(label);
#endif
        }

    }
}