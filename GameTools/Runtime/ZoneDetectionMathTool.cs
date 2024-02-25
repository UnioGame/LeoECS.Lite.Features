namespace Game.Code.GameTools.Runtime
{
    using System.Runtime.CompilerServices;
    using Unity.Mathematics;
    using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public class ZoneDetectionMathTool
    {
        /// <summary>
        /// Is point within rectangle
        /// </summary>
        /// <param name="targetPoint"></param>
        /// <param name="right"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="sourcePoint"></param>
        /// <param name="forward"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPointWithin(
            float3 targetPoint, 
            float3 sourcePoint, 
            float3 forward,
            float3 right,
            float2 offset, 
            float2 size)
        {
            var localPosition = sourcePoint + forward * offset.y + right * offset.x;
            var x = localPosition.x - (size.x / 2);
            var z = localPosition.z - (size.y / 2);
            var isDetectX = x <= targetPoint.x && targetPoint.x <= x + size.x;
            var isDetectZ = z <= targetPoint.z && targetPoint.z <= z + size.y;
            return isDetectX && isDetectZ;
        }

        /// <summary>
        /// Is point within cone
        /// </summary>
        /// <param name="targetPoint"></param>
        /// <param name="transformSource"></param>
        /// <param name="sourcePosition"></param>
        /// <param name="forward"></param>
        /// <param name="angle"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPointWithin(
            float3 targetPoint,
            float3 sourcePosition, 
            float3 forward, 
            float angle, 
            float distance)
        {
            var position = sourcePosition;
            var positionSourceIn2d = new float2(position.x, position.z);
            var positionTargetIn2d = new float2(targetPoint.x, targetPoint.z);
            var direction = positionTargetIn2d - positionSourceIn2d;
            var angleToTarget = Vector2.Angle((Vector3)forward, direction);

            if (!(angleToTarget * 2 <= angle)) return false;
            
            var sqDistance = distance * distance;
            var distanceToTarget = math.distancesq(positionSourceIn2d, positionTargetIn2d);
            return sqDistance >= distanceToTarget;
        }
        
        /// <summary>
        /// Is point within cone
        /// </summary>
        /// <param name="positionTarget"></param>
        /// <param name="transformSource"></param>
        /// <param name="angle"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPointWithin(
            float3 positionTarget,
            Transform transformSource,
            float angle, float distance)
        {
            return IsPointWithin(positionTarget, 
                transformSource.position,
                transformSource.forward,
                angle, distance);
        }

        /// <summary>
        /// Is point within circle
        /// </summary>
        /// <param name="positionTarget"></param>
        /// <param name="transformSource"></param>
        /// <param name="right"></param>
        /// <param name="offset"></param>
        /// <param name="radius"></param>
        /// <param name="sourcePosition"></param>
        /// <param name="forward"></param>
        /// <returns></returns>
        public static bool IsPointWithin(
            float3 positionTarget,
            float3 sourcePosition, 
            float3 forward,
            float3 right,
            float2 offset, 
            float radius)
        {
            var sqRadius = radius * radius;
            var localPosition = sourcePosition + forward * offset.y + right * offset.x;
            var distance = math.distancesq(localPosition, positionTarget);
            return distance <= sqRadius;
        }

        public static void DrawGizmos(GameObject target, Vector2 offset, float radius)
        {
#if UNITY_EDITOR
            var transform = target.transform;
            var transformMatrix = Matrix4x4.TRS(transform.position, transform.rotation,
                UnityEditor.Handles.matrix.lossyScale);
            using (new UnityEditor.Handles.DrawingScope(transformMatrix))
            {
                var position = Vector3.zero + Vector3.forward * offset.y + Vector3.right * offset.x;
                UnityEditor.Handles.color = Color.yellow;
                UnityEditor.Handles.DrawWireDisc(position, Vector3.up, radius);
            }
#endif
        }

        public static void DrawGizmos(GameObject target, Vector2 offset, Vector2 size)
        {
#if UNITY_EDITOR
            var transform = target.transform;
            var transformMatrix = Matrix4x4.TRS(transform.position, transform.rotation, UnityEditor.Handles.matrix.lossyScale);
            using (new UnityEditor.Handles.DrawingScope(transformMatrix))
            {
                var position = Vector3.zero + Vector3.forward * offset.y + Vector3.right * offset.x;
                UnityEditor.Handles.color = Color.yellow;
                UnityEditor.Handles.DrawWireCube(position, new Vector3(size.x, 0f, size.y));
            }
#endif
        }
        
        public static void DrawGizmos(GameObject target, float angle, float distance)
        {
#if UNITY_EDITOR
            var transform = target.transform;
            var position = transform.position;
            var transformMatrix = Matrix4x4.TRS(position, transform.rotation, UnityEditor.Handles.matrix.lossyScale);
            using (new UnityEditor.Handles.DrawingScope(transformMatrix))
            {
                var forward = transform.forward;
                var leftBoundary = Quaternion.Euler(0f, -angle / 2f, 0f) * forward;
                var rightBoundary = Quaternion.Euler(0f, angle / 2f, 0f) * forward;

                var leftPosition = position + leftBoundary * distance;
                var rightPosition = position + rightBoundary * distance;
                var centerPosition = position + forward * distance;
            
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(leftPosition, centerPosition);
                Gizmos.DrawLine(centerPosition, rightPosition);


                Gizmos.color = Color.red;
                Gizmos.DrawLine(position, position + forward * distance);
                Gizmos.DrawLine(position, position + leftBoundary * distance);
                Gizmos.DrawLine(position, position + rightBoundary * distance);
            }
#endif
        }

    }
}