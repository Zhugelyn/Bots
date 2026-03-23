using UnityEngine;

public class FlagPlacement : MonoBehaviour
{
    private const string GroundMask = "Ground";
    private const float RayDistance = 500f;

    [SerializeField] private FlagPreviewView _flagPreviewView;
    [SerializeField] private PlaceChecker _placeChecker;
    [SerializeField] private BaseClickView _baseClickView;

    private Base _currentBase;
    private Flag _flag;
    private bool _isPreview;
    private bool _canPlace;

    private void OnEnable()
    {
        _baseClickView.OnFlagPlacementRequested += EnterPreviewMode;
    }

    private void OnDisable()
    {
        _baseClickView.OnFlagPlacementRequested -= EnterPreviewMode;
    }

    private void Update()
    {
        if (_isPreview == false)
            return;

        UpdatePreview();

        if (_canPlace && Input.GetMouseButtonDown(0))
            PlaceFlag();

        if (Input.GetKey(KeyCode.Escape))
            CancelPreview();
    }

    private void UpdatePreview()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, RayDistance, LayerMask.GetMask(GroundMask)))
            _flagPreviewView.SetPosition(hit.point);

        _canPlace = _placeChecker.CanPlaced(_flag.transform.position);
        _flagPreviewView.SetValid(_canPlace);
    }

    private void EnterPreviewMode(Base selectedBase)
    {
        if (_isPreview)
            return;

        _currentBase = selectedBase;
        _flag = _flagPreviewView.CreateFlag(Vector3.zero);
        _isPreview = true;
    }

    private void PlaceFlag()
    {
        if (_currentBase.HasFlag)
            _currentBase.RemoveFlag();

        _flagPreviewView.SetPlacementColor(_currentBase.MainColor);
        _currentBase.SetFlag(_flag);
        _flag = null;
        _canPlace = false;
        _isPreview = false;
    }

    private void CancelPreview()
    {
        if (_flag != null)
        {
            Destroy(_flag.gameObject);
            _flag = null;
        }

        _isPreview = false;
    }
}
