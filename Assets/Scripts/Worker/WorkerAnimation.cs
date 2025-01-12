using System;
using UnityEngine;

public class WorkerAnimation : MonoBehaviour
{
    private readonly int LayerIndex = 0;

    [SerializeField] private Animator _animator;

    public event Action PickUpFinished;

    private AnimatorStateInfo _stateInfo;

    private void Setup(float speed, bool isPickUp)
    {
        _animator.SetFloat(WorkerAnimatorData.Params.Speed, speed);
        _animator.SetBool(WorkerAnimatorData.Params.IsPickUp, isPickUp);
    }

    private void Update()
    {
        _stateInfo = _animator.GetCurrentAnimatorStateInfo(LayerIndex);

        if (_stateInfo.shortNameHash == WorkerAnimatorData.Params.IsPickUp && _stateInfo.normalizedTime >= 1f)
            PickUpFinished?.Invoke();
    }

    public void Move()
    {
        Setup(1f, false);
    }

    public void PickUp()
    {
        Setup(0f, true);
    }

    public void Idle()
    {
        Setup(0f, false);
    }
}
