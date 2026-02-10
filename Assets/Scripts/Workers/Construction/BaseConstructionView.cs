using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseConstructionView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _buildParticlePrefab;
    [SerializeField] private Vector3 _finalScaleBase = new Vector3(1.5f, 1.5f, 1.5f);
    [SerializeField] private Vector3[] _scalesByStage = new Vector3[]
    {
        new Vector3(0.2f, 0.2f, 0.2f),
        new Vector3(0.5f, 0.5f, 0.5f),
        new Vector3(0.8f, 0.8f, 0.8f)
    };
    
    public void ShowStage(int stage, Transform baseTransform)
    {
        StopParticles();

        if (_buildParticlePrefab != null)
        {
            _buildParticlePrefab.transform.position = baseTransform.position;
            _buildParticlePrefab.Play();
        }
        
        baseTransform.localScale = _scalesByStage[stage];
    }

    public void ShowFinalBase(Transform baseTransform)
    {
        baseTransform.localScale = _finalScaleBase;
    }

    public void Clear()
    {
        StopParticles();
    }
    
    private void StopParticles()
    {
        if (_buildParticlePrefab != null)
        {
            if (_buildParticlePrefab.isPlaying)
            {
                _buildParticlePrefab.Stop();
            }
        }
    }
}
