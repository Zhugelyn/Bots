using UnityEngine;
using System;

public class BaseClickView : MonoBehaviour
{
    public event Action<Base> OnBaseChanged;
    public event Action<Base> OnFlagPlacementRequested;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Base baseComponent;
            
            if (TryGetBase(out baseComponent))
                OnBaseChanged?.Invoke(baseComponent);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            Base baseComponent;
            
            if (TryGetBase(out baseComponent))
                OnFlagPlacementRequested?.Invoke(baseComponent);
        }
    }

    private bool TryGetBase(out Base baseComponent)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out baseComponent))
            {
                return true;
            }
        }
        
        baseComponent = null;
        return false;
    }
}
