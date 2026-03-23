using UnityEngine;

public class BaseSelector : MonoBehaviour
{
    [SerializeField] private BaseView _baseView;
    [SerializeField] private ResourceCounterView _resourceCounterView;
    [SerializeField] private BaseClickView _baseClickView;
    [SerializeField] private Base _initialBase;

    private Base _selectedBase;

    private void Start()
    {
        SelectBase(_initialBase);
    }

    private void OnEnable()
    {
        _baseClickView.OnBaseChanged += SelectBase;
    }

    private void OnDisable()
    {
        _baseClickView.OnBaseChanged -= SelectBase;
    }

    private void SelectBase(Base newBase)
    {
        if (newBase == null || newBase == _selectedBase)
            return;

        if (_selectedBase != null)
            _baseView.StopSmoke(_selectedBase.Particle);

        _selectedBase = newBase;
        _resourceCounterView.Bind(_selectedBase.ResourceCounter);
        _baseView.SetColorBase(_selectedBase.MainColor);
        _baseView.StartSmoke(_selectedBase.Particle, _selectedBase.MainColor);
    }
}
