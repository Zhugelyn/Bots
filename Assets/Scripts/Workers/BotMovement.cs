using System.Collections;
using UnityEngine;

namespace Workers
{
    public class BotMovement : MonoBehaviour
    {
        private const float ArrivalThreshold = 0.1f;

        [SerializeField] private float _speed = 5f;

        public IEnumerator MoveTo(Transform target)
        {
            while (Vector3.Distance(transform.position, target.position) > ArrivalThreshold)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target.position,
                    _speed * Time.deltaTime
                );
                yield return null;
            }
        }

        public IEnumerator MoveTo(Vector3 destination)
        {
            while (Vector3.Distance(transform.position, destination) > ArrivalThreshold)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    destination,
                    _speed * Time.deltaTime
                );
                yield return null;
            }
        }
    }
}
