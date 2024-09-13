using UnityEngine;

namespace Gameplay.Blocks.Obstacle
{
    public class ObstacleBlockView : MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}