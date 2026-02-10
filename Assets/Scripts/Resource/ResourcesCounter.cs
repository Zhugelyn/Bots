using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesCounter : MonoBehaviour
{
    private const int QuantityPerUnit = 1;

    [SerializeField] private ResourceReceiver _resourceReceiver;

    private Dictionary<ResourceType, int> _resourcesCount;

    public event Action<Dictionary<ResourceType, int>> Changed;

    private void Awake()
    {
        _resourcesCount = new Dictionary<ResourceType, int>();

        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            _resourcesCount[type] = 0;
    }

    private void OnEnable()
    {
        _resourceReceiver.ResourceAccepted += ChangeResourceCount;
    }

    private void OnDisable()
    {
        _resourceReceiver.ResourceAccepted -= ChangeResourceCount;
    }

    public int GetResourceCount(ResourceType resourceType)
    {
        if (_resourcesCount.TryGetValue(resourceType, out int count))
            return count;

        return 0;
    }

    public bool HasEnoughTotal(int totalAmount) =>
        totalAmount > 0 && _resourcesCount.Values.Sum() >= totalAmount;

    public bool TryCost(int totalAmount)
    {
        if (HasEnoughTotal(totalAmount) == false)
            return false;

        SpendEvenly(totalAmount);
        Changed?.Invoke(new Dictionary<ResourceType, int>(_resourcesCount));
        return true;
    }

    public void Refund(int totalAmount)
    {
        if (totalAmount <= 0)
            return;

        RefundEvenly(totalAmount);
        Changed?.Invoke(new Dictionary<ResourceType, int>(_resourcesCount));
    }

    private void SpendEvenly(int totalAmount)
    {
        int remaining = totalAmount;

        while (remaining > 0)
        {
            ResourceType richestType = _resourcesCount
                .OrderByDescending(r => r.Value)
                .First().Key;

            _resourcesCount[richestType]--;
            remaining--;
        }
    }

    private void RefundEvenly(int totalAmount)
    {
        int remaining = totalAmount;

        while (remaining > 0)
        {
            ResourceType poorestType = _resourcesCount
                .OrderBy(r => r.Value)
                .First().Key;

            _resourcesCount[poorestType]++;
            remaining--;
        }
    }

    private void ChangeResourceCount(Resource resource)
    {
        ResourceType type = resource.Type;

        if (_resourcesCount.TryGetValue(type, out int current))
        {
            _resourcesCount[type] = current + QuantityPerUnit;

            Changed?.Invoke(new Dictionary<ResourceType, int>(_resourcesCount));
        }
    }
}
