using Patterns.ServiceLocator;
using UnityEngine;

namespace Runtime.World
{
    public class WorldStateGroup : MonoBehaviour
    {
        [Header("Dimension")]
        [SerializeField] private DimensionTarget activeDimension = DimensionTarget.Physical;

        [Header("Objects")]
        [SerializeField] private GameObject[] objectsToEnable;

        public void RefreshWorldState()
        {
            IWorldState worldState = ServiceLocator.Get<IWorldState>();

            if (worldState == null)
                return;

            bool shouldBeActive =
                activeDimension == DimensionTarget.Both ||
                MatchesCurrentDimension(worldState.CurrentDimension);

            foreach (GameObject obj in objectsToEnable)
            {
                if (obj != null)
                    obj.SetActive(shouldBeActive);
            }
        }

        private bool MatchesCurrentDimension(DimensionType currentDimension)
        {
            return
                activeDimension == DimensionTarget.Physical &&
                currentDimension == DimensionType.Physical
                ||
                activeDimension == DimensionTarget.Mirror &&
                currentDimension == DimensionType.Mirror;
        }

        private void Start()
        {
            RefreshWorldState();
        }
    }
}