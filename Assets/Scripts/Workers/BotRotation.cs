using System.Collections;
using UnityEngine;

namespace Workers
{
    public class BotRotation : MonoBehaviour
    {
        private const float AngleThreshold = 1f;

        [SerializeField] private float _rotationSpeed = 360f;

        public IEnumerator SmoothLookAt(Transform target)
        {
            while (true)
            {
                Vector3 direction = target.position - transform.position;
                direction.y = 0f;

                if (direction == Vector3.zero)
                    yield break;

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    _rotationSpeed * Time.deltaTime
                );

                if (Quaternion.Angle(transform.rotation, targetRotation) < AngleThreshold)
                    yield break;

                yield return null;
            }
        }

        public IEnumerator SmoothLookAt(Vector3 targetPosition)
        {
            while (true)
            {
                Vector3 direction = targetPosition - transform.position;
                direction.y = 0f;

                if (direction == Vector3.zero)
                    yield break;

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    _rotationSpeed * Time.deltaTime
                );

                if (Quaternion.Angle(transform.rotation, targetRotation) < AngleThreshold)
                    yield break;

                yield return null;
            }
        }
    }
}
