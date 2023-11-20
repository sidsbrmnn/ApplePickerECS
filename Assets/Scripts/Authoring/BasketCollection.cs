using Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class BasketCollection : MonoBehaviour
    {
        public GameObject basketPrefab;
        public uint count = 3;
        public float bottomY = -14f;
        public float spacing = 2f;

        private class BasketCollectionBaker : Baker<BasketCollection>
        {
            public override void Bake(BasketCollection authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new BasketSpawner
                {
                    Prefab = GetEntity(authoring.basketPrefab, TransformUsageFlags.Dynamic),
                    Count = authoring.count,
                    BottomY = authoring.bottomY,
                    Spacing = authoring.spacing,
                    ShouldSpawn = true,
                });
            }
        }
    }
}
