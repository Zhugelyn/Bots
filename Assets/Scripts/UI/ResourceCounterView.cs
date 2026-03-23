using System.Collections.Generic;
using UnityEngine;

public class ResourceCounterView : MonoBehaviour
{
    [SerializeField] private ResourceCellView _woodCellView;
    [SerializeField] private ResourceCellView _stoneCellView;
    [SerializeField] private ResourceCellView _goldCellView;

    private ResourcesCounter _resourcesCounter;

    private void OnDisable()
    {
        Unbind();
    }

    public void Bind(ResourcesCounter resourcesCounter)
    {
        if (resourcesCounter == null)
            return;

        Unbind();

        _resourcesCounter = resourcesCounter;
        _resourcesCounter.Changed += Refresh;
        Refresh(null);
    }

    private void Unbind()
    {
        if (_resourcesCounter != null)
            _resourcesCounter.Changed -= Refresh;

        _resourcesCounter = null;
    }

    private void Refresh(Dictionary<ResourceType, int> _)
    {
        if (_resourcesCounter == null)
            return;

        _woodCellView.SetText(_resourcesCounter.GetResourceCount(ResourceType.Wood));
        _stoneCellView.SetText(_resourcesCounter.GetResourceCount(ResourceType.Stone));
        _goldCellView.SetText(_resourcesCounter.GetResourceCount(ResourceType.Gold));
    }
}
