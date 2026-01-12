using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceCounterView : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _resourceCountTexts;
    [SerializeField] private ResourcesCounter _resourcesCounter;

    private void OnEnable()
    {
        _resourcesCounter.Changed += UpdateResourceCount;
    }

    private void OnDisable()
    {
        _resourcesCounter.Changed -= UpdateResourceCount;
    }

    private void UpdateResourceCount(Dictionary<ResourceType, int> resourcesCount)
    {
        foreach (var resourceCountText in _resourceCountTexts)
        {
            if (Enum.TryParse(resourceCountText.name, true, out ResourceType resourceType))
            {
                if (resourcesCount.TryGetValue(resourceType, out int value))
                    resourceCountText.text = SetDisplay(value);
            }
        }
    }

    private string SetDisplay(int count)
    {
       return $"{count}";
    }
}