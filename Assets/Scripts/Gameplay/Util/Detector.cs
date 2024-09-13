using UnityEngine;

namespace Gameplay.Util
{
    public class Detector<T> where T : Component
    {
        public int OverlapSphere(Vector3 position, float radius, T[] result)
        {
            Collider[] colliders = new Collider[result.Length];
            var size = Physics.OverlapSphereNonAlloc(position, radius, colliders, LayerMask.GetMask($"Enemy"));
        
            for (int i = 0; i < size; i++)
            {
                T component = colliders[i].GetComponent<T>();
                if (component)
                {
                    result[i] = component;
                }
            }

            return size;
        }
    }
}
