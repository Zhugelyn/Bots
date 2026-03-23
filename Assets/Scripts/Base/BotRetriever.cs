using System;
using UnityEngine;
using Workers;

public class BotRetriever : MonoBehaviour
{
    public event Action<Worker> WorkerArrived;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Worker worker))
            WorkerArrived?.Invoke(worker);
    }
}
