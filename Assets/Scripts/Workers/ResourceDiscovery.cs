using UnityEngine;

namespace Workers
{
    public class ResourceDiscovery : MonoBehaviour
    {
        private Worker _worker;

        public void Initialize(Worker worker)
        {
            _worker = worker;
        }

        private void OnTriggerEnter(Collider other)
        {
            var offsetY = new Vector3(0, other.transform.position.y, 0);
            
            if (other.TryGetComponent(out Resource resource)
                && other.transform.position - offsetY == _worker.DestinationPoint)
            {
                _worker.PickUpResource(resource);
            }
        }
    }
}
