namespace Game.Code.GameLayers.Relationship
{
    using System.Runtime.CompilerServices;
    using Layer;
    using Unity.Collections;
    
#if ENABLE_IL2CPP
    using System.Runtime.CompilerServices;
    using Layer;
    using Unity.Collections;
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif
    public static class RelationshipTool
    {
        public static NativeHashMap<RelationLayerData, LayerId> layerMaskCache;
        public static LayerId[] layerIds;
        public static RelationshipId[,] relationshipMatrix;
        public static RelationshipId selfRelationship;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Initialize(LayerId[] layers, RelationshipId[,] matrix,RelationshipId selfId)
        {
            if (layerMaskCache.IsCreated)
                layerMaskCache.Dispose();
            
            layerMaskCache = new NativeHashMap<RelationLayerData, LayerId>(
                matrix.Length, 
                Allocator.Persistent);
            
            layerIds = layers;
            relationshipMatrix = matrix;
            selfRelationship = selfId;
        }

        public static void Destroy()
        {
            layerMaskCache.Dispose();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSelf(this RelationshipId relationshipId)
        {
            return relationshipId.HasFlag(selfRelationship);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LayerId GetFilterMask(this RelationshipId relationship, LayerId self)
        {
            if (relationship.HasFlag(selfRelationship))
                return self;
            
            var data = new RelationLayerData(self, relationship);
            if(layerMaskCache.ContainsKey(data)) 
                return layerMaskCache[data];
            
            var layer = GetFilterMaskCached(self, relationship);
            layerMaskCache[data] = layer;
            return layer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static LayerId GetFilterMaskCached(LayerId self, RelationshipId relationship)
        {
            var result = 0;
            var selfLayer = (int)self;
            
            for (var i = 0; i < layerIds.Length; i++)
            {
                var layer = (int)layerIds[i];
                if((selfLayer & layer) == 0) continue;
                
                for (var j = 0; j < layerIds.Length; j++)
                {
                    var layerRelation = relationshipMatrix[i, j];
                    if((layerRelation & relationship) == 0) 
                        continue;
                    
                    var targetLayer = (int)layerIds[j];
                    result |= targetLayer;
                }
            }

            return (LayerId)result;
        }
    }
}
