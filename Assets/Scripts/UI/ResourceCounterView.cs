using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCounterView : MonoBehaviour
{
    [SerializeField] private ResourceCellView _woodCellView;
    [SerializeField] private ResourceCellView _stoneCellView;
    [SerializeField] private ResourceCellView _goldCellView;

    private ResourcesCounter _resourcesCounter;
    private Dictionary<ResourceType, int> _;

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
        Refresh(_);
    }

    private void Unbind()
    {
        if (_resourcesCounter != null)
            _resourcesCounter.Changed -= Refresh;
        
        _resourcesCounter = null;
    }

    public void Refresh(Dictionary<ResourceType, int> _)
    {
        if (_resourcesCounter == null)
            return;

        var woodCount = _resourcesCounter.GetResourceCount(ResourceType.Wood);
        _woodCellView.SetText(woodCount);
        var stoneCount = _resourcesCounter.GetResourceCount(ResourceType.Stone);
        _stoneCellView.SetText(stoneCount);
        var goldCount = _resourcesCounter.GetResourceCount(ResourceType.Gold);
        _goldCellView.SetText(goldCount);
    }
}