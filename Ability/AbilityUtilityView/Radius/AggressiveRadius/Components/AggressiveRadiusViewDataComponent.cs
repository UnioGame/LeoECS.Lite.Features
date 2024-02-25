namespace Game.Ecs.Ability.AbilityUtilityView.Radius.AggressiveRadius.Components
{
    using Code.GameLayers.Category;
    using Code.GameLayers.Layer;
    using UnityEngine;

    public struct AggressiveRadiusViewDataComponent
    {
        public GameObject NoTargetRadiusView;
        public GameObject TargetCloseRadiusView;
        public GameObject HasTargetRadiusView;
        
        public CategoryId CategoryId;
        public LayerId LayerMask;
    }
}