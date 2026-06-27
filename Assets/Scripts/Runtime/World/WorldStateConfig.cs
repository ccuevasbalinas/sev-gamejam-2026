using UnityEngine;
using Patterns.ScriptableEvent;

namespace Runtime.World
{
    [CreateAssetMenu(fileName = "WorldStateConfig", menuName = "Game/World State Config")]
    public class WorldStateConfig : ScriptableObject
    {
        [SerializeField] private DimensionType initialDimension = DimensionType.Physical;
        [SerializeField] private ScriptableEvent worldChangedEvent;

        public DimensionType InitialDimension => initialDimension;
        public ScriptableEvent WorldChangedEvent => worldChangedEvent;
    }
}