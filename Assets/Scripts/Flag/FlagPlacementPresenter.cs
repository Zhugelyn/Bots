using System;
using UnityEngine;

public class FlagPlacementPresenter : IDisposable
{
    private const string GroundMask = "Ground";

    private float _rayDistance = 500f;
    
    private FlagPreviewView _flagPreviewView;
    private PlaceChecker _placeChecker;
    private BaseClickView _baseClickView;
    private Flag _flag;
    private Base _currentBase;
    
    private bool _isPreview;
    private bool _canPlacement;


    public FlagPlacementPresenter(FlagPreviewView flagPreviewView, PlaceChecker placeChecker, BaseClickView baseClickView)
    {
        _flagPreviewView = flagPreviewView;
        _placeChecker = placeChecker;
        _baseClickView = baseClickView;

        _baseClickView.OnFlagPlacementRequested += EnterPreviewMode;
    }

    public void OnUpdate()
    {
        if (_isPreview)
            TickPreview();
    }

    public void Dispose()
    {
        _baseClickView.OnFlagPlacementRequested -= EnterPreviewMode;
    }

    private void TickPreview()
    {
        CalculateFlagPosition();
        _canPlacement = _placeChecker.CanPlaced(_flag.transform.position);
        _flagPreviewView.SetValid(_canPlacement);
            
        if (_canPlacement && Input.GetMouseButtonDown(0))
            ExitPreviewMode();

        if (Input.GetKey(KeyCode.Escape))
            CancelPreviewMode();
    }
    
    private void CalculateFlagPosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance, LayerMask.GetMask(GroundMask)))
            _flagPreviewView.SetPosition(hit.point);
    }

    private void EnterPreviewMode(Base selectedBase)
    {
        if (_isPreview)
            return;
        
        _currentBase = selectedBase;
        _flag = _flagPreviewView.CreateFlag(Vector3.zero);
        _isPreview = true;
    }
    
    private void ExitPreviewMode()
    {
        if (_currentBase.HasFlag)
            _currentBase.RemoveFlag();
            
        _flagPreviewView.SetPlacementColor(_currentBase.MainColor);
        _currentBase.SetFlag(_flag);
        _canPlacement = false;
        _isPreview = false;
    }

    private void CancelPreviewMode()
    {
        if (_flag != null)
        {
            UnityEngine.Object.Destroy(_flag.gameObject);
            _flag = null;
        }
        
        _isPreview = false;
    }
}
