using UnityEngine;

namespace Gameplay.Blocks.Ground
{
    public class GroundBlockView : MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}