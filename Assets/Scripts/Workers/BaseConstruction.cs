using System.Collections;
using UnityEngine;
using Workers;

public class BaseConstruction : MonoBehaviour
{
    public static float s_TimeConstruction = 10f;
    
    [SerializeField] private Base _basePrefab;
    [SerializeField] private ParticleSystem _particlePrefab;
    [SerializeField] private Worker _worker;
    
    private Coroutine _courtine;
    private float _partsConstructionCount = s_TimeConstruction / 3;

    private void OnEnable()
    {
        _worker.OnStartBuild += StartConstruction;
    }

    private void OnDisable()
    {
        _worker.OnStartBuild -= StartConstruction;
    }

    private void StartConstruction(Vector3 position)
    {
        StartCoroutine(Start());
    }

    private IEnumerator Start()
    {
        int i = 0;
        while (enabled)
        {
            // Логика для партиклов и базы
            i++;
            Debug.Log($"Этап {i}");
            
            yield return new WaitForSeconds(_partsConstructionCount);
        }
    }

    private void StopConstruction()
    {
        StopCoroutine(_courtine);
    }
}
