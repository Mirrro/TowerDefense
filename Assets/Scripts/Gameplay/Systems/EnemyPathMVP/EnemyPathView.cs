using UnityEngine;

namespace Gameplay.Systems.EnemyPathMVP
{
    public class EnemyPathView : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        public void DisplayPath(Vector3[] pathPoints)
        {
            lineRenderer.positionCount = pathPoints.Length;
            lineRenderer.SetPositions(pathPoints);
        }

        public void ClearPath()
        {
            lineRenderer.positionCount = 0;
        }
    }
}