using Unity.Entities;

namespace Components
{
    public struct Random : IComponentData
    {
        public Unity.Mathematics.Random Value;
    }
}
