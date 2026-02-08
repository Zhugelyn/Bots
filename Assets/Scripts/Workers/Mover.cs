using UnityEngine;

namespace Workers
{
    public class Mover
    {
        private Transform _transform;

        public Mover(Transform transform)
        {
            _transform = transform;
        }

        public void MoveTo(Vector3 destination, float speed)
        {
            Debug.Log("Moving to " + _transform.position);
            _transform.position = Vector3.MoveTowards(
                _transform.position,
                destination,
                speed * Time.deltaTime
            );
            _transform.LookAt(destination);
        }
    }
}