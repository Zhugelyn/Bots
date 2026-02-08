using System;
using Unity.VisualScripting;
using UnityEngine;

public class FlagPlacementTickView : MonoBehaviour
{
    [SerializeField] private PlaceChecker _placeChecker;
    [SerializeField] private FlagPreviewView _flagPreviewView;
    [SerializeField] private BaseClickView _baseClickView;
    
    private FlagPlacementPresenter _presenter;

    private void Awake()
    {
        _presenter = new FlagPlacementPresenter(_flagPreviewView, _placeChecker, _baseClickView);
    }

    private void OnDisable()
    {
        _presenter.Dispose();
    }

    private void OnDestroy()
    {
        _presenter.Dispose();
    }

    private void Update()
    {
        _presenter.OnUpdate();
    }
}
