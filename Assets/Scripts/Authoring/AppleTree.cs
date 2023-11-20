using Components;
using Unity.Entities;
using UnityEngine;
using Random = Components.Random;

namespace Authoring
{
    public class AppleTree : MonoBehaviour
    {
        public float speed = 10f;
        public float leftAndRightEdge = 24f;
        public float changeDirectionChance = 0.2f;

        public GameObject applePrefab;
        public float appleDropDelay = 0.5f;

        private class AppleTreeBaker : Baker<AppleTree>
        {
            public override void Bake(AppleTree authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new AppleTreeProperties
                {
                    Speed = authoring.speed,
                    LeftAndRightEdge = authoring.leftAndRightEdge,
                    ChangeDirectionChance = authoring.changeDirectionChance
                });
                AddComponent(entity, new Random
                {
                    Value = Unity.Mathematics.Random.CreateFromIndex((uint)entity.Index)
                });
                AddComponent(entity, new AppleSpawner
                {
                    Prefab = GetEntity(authoring.applePrefab, TransformUsageFlags.Dynamic),
                    Delay = authoring.appleDropDelay
                });
                AddComponent(entity, new Timer
                {
                    Value = 2f
                });
            }
        }
    }
}
