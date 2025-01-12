using System.Collections.Generic;
using System.Linq;
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
            var needResource = resourcesCount.Where(r => r.Key.ToString() == resourceCountText.name);

            if (!needResource.Any())
                continue;

            var count = needResource.Select(r => r.Value).First();
            resourceCountText.text = SetDisplay(count);
        }
    }

    private string SetDisplay(int count)
    {
       return $"{count}";
    }
}