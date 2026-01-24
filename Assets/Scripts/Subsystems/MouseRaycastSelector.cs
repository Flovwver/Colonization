using System;
using UnityEngine;

public class MouseRaycastSelector : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    public event Action<Throne> ThroneClicked;
    public event Action<Vector3> GroundClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            FindClickedObject();
        }
    }

    private void FindClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Throne throne))
            {
                ThroneClicked?.Invoke(throne);
            }
            else if (hit.collider.TryGetComponent(out Ground ground))
            {
                GroundClicked?.Invoke(hit.point);
            }
        }
    }
}