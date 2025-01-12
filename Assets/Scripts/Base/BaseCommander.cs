using System.Linq;
using UnityEngine;

public class BaseCommander : MonoBehaviour
{
    private Base _base;

    public void Initialize(Base base1)
    {
        _base = base1;
    }

    private void Update()
    {
        if (CheckTheSendingOfWorker())
            SendWorkerToGetResource(_base._backlog.First());
    }

    private void SendWorkerToGetResource(Vector3 position)
    {
        var worker = GetFreeWorker();
        worker.SetMovementState(position);
        _base._backlog.Remove(position);
    }

    private Worker GetFreeWorker() =>
        _base.Workers.Where(w => w.IsBusy == false).First();

    private bool CheckTheSendingOfWorker() =>
        _base._backlog.Any() && _base.Workers.Any(w => w.IsBusy == false);
}
