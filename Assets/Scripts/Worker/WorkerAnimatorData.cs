using UnityEngine;

public class WorkerAnimatorData : MonoBehaviour
{
    public static class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int IsPickUp = Animator.StringToHash(nameof(IsPickUp));
    }
}
