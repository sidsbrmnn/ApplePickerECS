using Unity.Entities;

namespace Components
{
    public struct BasketSpawner : IComponentData
    {
        public Entity Prefab;
        public uint Count;
        public float BottomY;
        public float Spacing;
        public bool ShouldSpawn;
    }
}
