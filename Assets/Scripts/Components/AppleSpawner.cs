using Unity.Entities;

namespace Components
{
    public struct AppleSpawner : IComponentData
    {
        public Entity Prefab;
        public float Delay;
    }
}
