using UnityEngine;

public class MouseRayCast
{
    private Plane plane = new Plane(Vector3.up, Vector3.zero);
    
    public bool TryGetPosition(out Vector3 point)
    {
        //Create a ray from the Mouse click position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Initialise the enter variable
        float enter = 0.0f;
        point = Vector3.zero;
        
        if (plane.Raycast(ray, out enter))
        {
            point = ray.GetPoint(enter);
            return true;
        }
        return false;
    }
}