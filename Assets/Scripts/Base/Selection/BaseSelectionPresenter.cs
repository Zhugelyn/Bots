using System;
using UnityEngine;

public class BaseSelectionPresenter : MonoBehaviour
{
    [SerializeField] private Base _initializedBase;
    [SerializeField] private ResourceCounterView _resourceCounterView;
    [SerializeField] private BaseClickView _baseClickView;
    [SerializeField] private BaseView _baseView;

    private Base _selectedBase;

    private void OnValidate()
    {
        Debug.Assert(_initializedBase != null);
    }

    private void Awake()
    {
        ChangeBase(_initializedBase);
    }

    private void OnEnable()
    {
        _baseClickView.OnBaseChanged += ChangeBase;
    }

    private void OnDisable()
    {
        _baseClickView.OnBaseChanged -= ChangeBase;
    }

    public void ChangeBase(Base newBase)
    {
        if (newBase == null)
            return;
        
        if (_selectedBase == newBase)
            return;
        
        if (_selectedBase != null)
            _baseView.StopSmoke(_selectedBase.Particle);
        
        _selectedBase = newBase;
        _resourceCounterView.Bind(_selectedBase.ResourceCounter);
        _baseView.SetColorBase(_selectedBase.MainColor);
        _baseView.StartSmoke(_selectedBase.Particle, _selectedBase.MainColor);
    }
}
