namespace Game.Ecs.Presets.Assets
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;
    
    [Serializable]
    public class DirectionalLightPresets
    {
        [HideInInspector]
        public bool showTargetValue = true;

        public bool createNewLight = false;
        public bool active;
        
        public Vector3 position;
        public Vector3 rotation;
        
        public Color color;
        public float intensity;

        [ShowIf(nameof(showTargetValue))]
        [Required]
        public Light directionLight;
        
        
        private static DirectionalLightPresets _bufferPreset = new DirectionalLightPresets();
        
        public static void Lerp(DirectionalLightPresets from, DirectionalLightPresets to, float progress)
        {
            switch (progress)
            {
                case >= 1:
                    to.ApplyToDirectionalLight();
                    return;
                case <= 0:
                    from.ApplyToDirectionalLight();
                    return;
            }

            _bufferPreset.BakeDirectionalLight(from);
            _bufferPreset.ApplyLerp(from, to, progress);
            _bufferPreset.ApplyToDirectionalLight();
        }

        public void ApplyLerp(DirectionalLightPresets from, DirectionalLightPresets target, float progress)
        {
            //Bool
            createNewLight = target.createNewLight;
            
            //Position and rotation
            position = Vector3.Lerp(from.position, target.position, progress);
            rotation = Vector3.Lerp(from.rotation, target.rotation, progress);

            //Float
            intensity = Mathf.Lerp(from.intensity, target.intensity, progress);

            //Color
            color = Color.Lerp(from.color, target.color, progress);
        }
        
        private Light SearchFirstLight()
        {
            var directionalLights = UnityEngine.Object.FindObjectsOfType<Light>(includeInactive: true);
            
            foreach (var light in directionalLights)
            {
                if (light.type != LightType.Directional) continue;
                return light;
            }

            return null;
        }
        
        private void CreateDirectionalLight()
        {
            var directionalLight = new GameObject("Directional Light");
            directionLight = directionalLight.AddComponent<Light>();
            directionLight.type = LightType.Directional;
        }
        
        public void SetSourceConverter(bool apply, DirectionalLightPresets sourcePreset)
        {
            if (apply)
            {
                ApplyDirectionalLight(sourcePreset);
                sourcePreset.ApplyToDirectionalLight();
            }
            else
            {
                BakeDirectionalLight(sourcePreset);
            }
        }

        private void ApplyDirectionalLight(DirectionalLightPresets directionalLightPresets)
        {
            directionalLightPresets.position = position;
            directionalLightPresets.rotation = rotation;
            directionalLightPresets.active = active;
            directionalLightPresets.color = color;
            directionalLightPresets.intensity = intensity;
            directionalLightPresets.createNewLight = createNewLight;
        }
        
        public void ApplyToDirectionalLight()
        {
            if (createNewLight && directionLight == null) CreateDirectionalLight();
            if (directionLight == null) 
                directionLight = SearchFirstLight();
            
            if (directionLight == null)
            {
                Debug.LogError($"Directional light not found in {nameof(DirectionalLightPresets)} : {nameof(ApplyToDirectionalLight)}");
                return;
            }
            
            var directionalLightObject = directionLight.gameObject;
            var transform = directionalLightObject.transform;

            transform.localPosition = position;
            transform.localRotation = Quaternion.Euler(rotation);
            directionalLightObject.SetActive(active);
            directionLight.color = color;
            directionLight.intensity = intensity;
        }
        
        public void BakeDirectionalLight(DirectionalLightPresets directionalLightPresets)
        {
            createNewLight = directionalLightPresets.createNewLight;
            position = directionalLightPresets.position;
            rotation = directionalLightPresets.rotation;
            active = directionalLightPresets.active;
            color = directionalLightPresets.color;
            intensity = directionalLightPresets.intensity;
        }
        
        public void BakeDirectionalLight()
        {
            directionLight ??= SearchFirstLight();

#if UNITY_EDITOR
            if (directionLight == null)
            {
                Debug.LogError($"Directional light not found in {nameof(DirectionalLightPresets)} : {nameof(BakeDirectionalLight)}");
                return;
            }
#endif
            var directionalLightObject = directionLight.gameObject;
            var transform = directionalLightObject.transform;
            
            position = transform.localPosition;
            rotation = transform.localRotation.eulerAngles;
            active = directionalLightObject.activeSelf;
            color = directionLight.color;
            intensity = directionLight.intensity;
        }
    }
}