using Unity.Entities;

namespace Components
{
    public struct AppleTreeProperties : IComponentData
    {
        public float Speed;
        public float LeftAndRightEdge;
        public float ChangeDirectionChance;
    }
}
