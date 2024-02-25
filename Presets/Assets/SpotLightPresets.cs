namespace Game.Ecs.Presets.Assets
{
    using System;
    using UnityEngine;
    using Sirenix.OdinInspector;

    [Serializable]
    public class SpotLightPresets
    {
        [ShowIf(nameof(showTargetValue))]
        public bool autoUpdate = false;
        [HideInInspector]
        public bool showTargetValue = true;

        [OnValueChanged(nameof(AutoUpdate))]
        public bool active;

        [OnValueChanged(nameof(AutoUpdate))]
        public Vector3 position;
        [OnValueChanged(nameof(AutoUpdate))]
        public Vector3 rotation;
        
        [OnValueChanged(nameof(AutoUpdateSpotAngle))]
        [Range(0, 179)]
        public float spotAngle;
        [OnValueChanged(nameof(AutoUpdateSpotAngle))]
        [Range(0, 179)]
        public float innerSpotAngle;

        [OnValueChanged(nameof(AutoUpdate))]
        public Color color;
        [OnValueChanged(nameof(AutoUpdate))]
        public float intensity;
        [OnValueChanged(nameof(AutoUpdate))]
        public float range;
        
        [ShowIf(nameof(showTargetValue))]
        [Required]
        public Light spotLight;
        
        private static SpotLightPresets _bufferPreset = new SpotLightPresets();

        public static void Lerp(SpotLightPresets from, SpotLightPresets to, float progress)
        {
            switch (progress)
            {
                case >= 1:
                    to.ApplyToSpotLight();
                    return;
                case <= 0:
                    from.ApplyToSpotLight();
                    return;
            }

            _bufferPreset.BakeSpotLight(from);
            _bufferPreset.ApplyLerp(from, to, progress);
            _bufferPreset.ApplyToSpotLight();
        }

        public void ApplyLerp(SpotLightPresets from, SpotLightPresets target, float progress)
        {
            //Position and rotation
            position = Vector3.Lerp(from.position, target.position, progress);
            rotation = Vector3.Lerp(from.rotation, target.rotation, progress);

            //Float
            spotAngle = Mathf.Lerp(from.spotAngle, target.spotAngle, progress);
            innerSpotAngle = Mathf.Lerp(from.innerSpotAngle, target.innerSpotAngle, progress);
            intensity = Mathf.Lerp(from.intensity, target.intensity, progress);
            range = Mathf.Lerp(from.range, target.range, progress);

            //Color
            color = Color.Lerp(from.color, target.color, progress);
        }
        
        private void SearchFirstLight()
        {
            var spotLights = UnityEngine.Object.FindObjectsOfType<Light>(includeInactive: true);

            foreach (var light in spotLights)
            {
                if (light.type != LightType.Spot) continue;
                spotLight = light;
                break;
            }
        }


        private void AutoUpdate()
        {
#if UNITY_EDITOR
            if (autoUpdate == false) return;
        
            ApplyToSpotLight();
#endif
        }

        public void SetSourceConverter(bool apply, SpotLightPresets sourcePreset)
        {
            if (apply)
            {
                ApplySpotLight(sourcePreset);
                sourcePreset.ApplyToSpotLight();
            }
            else
            {
                BakeSpotLight(sourcePreset);
            }
        }

        private void ApplySpotLight(SpotLightPresets spotLightPresets)
        {
            spotLightPresets.position = position;
            spotLightPresets.rotation = rotation;
            spotLightPresets.active = active;
            spotLightPresets.spotAngle = spotAngle;
            spotLightPresets.innerSpotAngle = innerSpotAngle;
            spotLightPresets.color = color;
            spotLightPresets.intensity = intensity;
            spotLightPresets.range = range;
        }
        
        public void ApplyToSpotLight()
        {
            if (spotLight == null) SearchFirstLight();
            
            var spotLightObject = spotLight.gameObject;
            var transform = spotLight.transform;

            transform.localPosition = position;
            transform.localRotation = Quaternion.Euler(rotation);
            spotLightObject.SetActive(active);
            spotLight.spotAngle = spotAngle;
            spotLight.innerSpotAngle = innerSpotAngle;
            spotLight.color = color;
            spotLight.intensity = intensity;
            spotLight.range = range;
        }
        
        public void BakeSpotLight(SpotLightPresets spotLightPresets)
        {
            position = spotLightPresets.position;
            rotation = spotLightPresets.rotation;
            active = spotLightPresets.active;
            spotAngle = spotLightPresets.spotAngle;
            innerSpotAngle = spotLightPresets.innerSpotAngle;
            color = spotLightPresets.color;
            intensity = spotLightPresets.intensity;
            range = spotLightPresets.range;
        }
        
        public void BakeSpotLight()
        {
            if (spotLight == null) SearchFirstLight();
            
            var spotLightObject = spotLight.gameObject;
            var transform = spotLight.transform;
            
            position = transform.localPosition;
            rotation = transform.localRotation.eulerAngles;
            active = spotLightObject.activeSelf;
            spotAngle = spotLight.spotAngle;
            innerSpotAngle = spotLight.innerSpotAngle;
            color = spotLight.color;
            intensity = spotLight.intensity;
            range = spotLight.range;
        }

        public void AutoUpdateSpotAngle()
        {
            innerSpotAngle = Mathf.Clamp(innerSpotAngle, 0f, spotAngle);

            AutoUpdate();
        }
    }
}