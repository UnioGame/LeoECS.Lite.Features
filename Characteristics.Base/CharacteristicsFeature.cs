namespace Game.Ecs.Characteristics.Base
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Cysharp.Threading.Tasks;
    using Feature;
    using Components;
    using Components.Events;
    using Components.Requests;
    using Systems;
    using Leopotam.EcsLite;
    using Leopotam.EcsLite.ExtendedSystems;
    using Sirenix.OdinInspector;
    using UniCore.Runtime.ProfilerTools;
    using UniGame.LeoEcs.Bootstrap.Runtime;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Serialization;
    using System;
    
#if UNITY_EDITOR
    using UniModules.Editor;
    using UnityEditor;
#endif
    
    [CreateAssetMenu(menuName = "Game/Feature/Characteristics/Characteristics Feature",
        fileName = "Characteristics Feature")]
    public class CharacteristicsFeature : BaseLeoEcsFeature
    {
        [FormerlySerializedAs("effectFeatures")]
        [SerializeReference]
        [Searchable(FilterOptions = SearchFilterOptions.ISearchFilterableInterface)]
        public List<CharacteristicEcsFeature> characteristicFeatures = new List<CharacteristicEcsFeature>();
        
        public override async UniTask InitializeFeatureAsync(IEcsSystems ecsSystems)
        {
#if DEBUG
            var timer = Stopwatch.StartNew();   
            timer.Restart();
#endif
            foreach (var feature in characteristicFeatures)
            {
#if DEBUG
                timer.Restart();
#endif
                await feature.InitializeFeatureAsync(ecsSystems);
#if DEBUG
                var elapsed = timer.ElapsedMilliseconds;
                timer.Stop();
                GameLog.Log($"\tSOURCE: LOAD TIME Characteristic {feature.FeatureName}|{feature.GetType().Name} = {elapsed} ms");
#endif
            }
            
            //remove value changed event
            ecsSystems.DelHere<CharacteristicChangedComponent>();
            ecsSystems.DelHere<CharacteristicValueChangedEvent>();

            //reset all modification from characteristic and fire event 
            //base value reset to default
            ecsSystems.DelHere<ResetCharacteristicsEvent>();
            ecsSystems.Add(new ResetModificationsSystem());
            ecsSystems.DelHere<ResetModificationsRequest>();

            //change max limit and recalculate characteristic value
            ecsSystems.Add(new ChangeCharacteristicMaxLimitationSystem());
            ecsSystems.Add(new ChangeCharacteristicMinLimitationSystem());
            //change base value and recalculate characteristic value
            ecsSystems.Add(new ChangeCharacteristicBaseValueSystem());  
            
            //change characteristic value directly
            ecsSystems.Add(new ChangeCharacteristicValueSystem());
            ecsSystems.DelHere<ChangeCharacteristicRequest>();
            ecsSystems.DelHere<ChangeCharacteristicBaseRequest>();

            //mark characteristic as changed by RecalculateCharacteristicSelfRequest
            ecsSystems.Add(new MarkCharacteristicAsChangedSystem());

            //check modification source life time and remove if expired
            ecsSystems.Add(new CheckModificationSourceLifeTimeComponent());

            //base characteristic changes: min,max, base value,modifications
            //after changes characteristic marks with RecalculateCharacteristicSelfRequest
            ecsSystems.Add(new ResetCharacteristicMaxLimitSystem());
            ecsSystems.Add(new ResetCharacteristicsSystem());
            ecsSystems.Add(new RemoveModificationSystem());
            ecsSystems.Add(new AddModificationSystem());
            ecsSystems.Add(new CreateModificationSystem());
            
            //recalculate PercentModificationsValueComponent by modifications
            ecsSystems.Add(new RecalculatePercentValueSystem());
            //recalculate MaxLimitModificationsValueComponent by modifications
            ecsSystems.Add(new RecalculateMaxLimitValueSystem());
            //recalculate value of modifications by base value
            ecsSystems.Add(new RecalculateModificationsValueSystem());
            //listen RecalculateCharacteristicSelfRequest and detect characteristic changes,
            //if changed mark with CharacteristicChangedComponent
            ecsSystems.Add(new RecalculateCharacteristicValueSystem());
            //if characteristic changed fire event CharacteristicValueChangedEvent
            ecsSystems.Add(new DetectCharacteristicChangesSystem());
            
            ecsSystems.DelHere<ResetCharacteristicRequest>();
            ecsSystems.DelHere<ResetCharacteristicMaxLimitSelfRequest>();
            ecsSystems.DelHere<ChangeMaxLimitRequest>();
            ecsSystems.DelHere<ChangeMinLimitRequest>();
            ecsSystems.DelHere<AddModificationRequest>();
            ecsSystems.DelHere<CreateModificationRequest>();
            ecsSystems.DelHere<RemoveModificationRequest>();
            ecsSystems.DelHere<RecalculateModificationSelfRequest>();
            ecsSystems.DelHere<RecalculateCharacteristicSelfRequest>();
        }

        [Button("Fill Characteristics")]
        public void FillCharacteristics()
        {
#if UNITY_EDITOR
            characteristicFeatures.RemoveAll(x => x == null);
            
            var instances = TypeCache.GetTypesDerivedFrom<CharacteristicEcsFeature>();
            foreach (var characteristic in instances)
            {
                var firstOrDefault = characteristicFeatures
                    .FirstOrDefault(x => x.GetType() == characteristic);
                var contains = firstOrDefault != null;
                if(contains) continue;
                var instance = Activator.CreateInstance(characteristic) as CharacteristicEcsFeature;
                if(instance == null) continue;
                characteristicFeatures.Add(instance);
            }
            
            this.MarkDirty();
#endif
        }
    }
}
