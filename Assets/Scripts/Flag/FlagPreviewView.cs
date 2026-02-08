using UnityEngine;

public class FlagPreviewView : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;
    
    private Color _unvalid = Color.red;
    private Color _valid = Color.green;
    private float _transperent = 0.3f;
    private Material _material;
    private Flag _flag;

    public void SetValid(bool isValid)
    {
        if (_material == null)
            return;
        
        _material.color = isValid ? _valid : _unvalid;
        var material = _material.color;
        material.a = _transperent;
        _material.color = material;
    }

    public void SetPlacementColor(Color color)
    {
        if (_material == null)
            return;
        
        _material.color = color;
    }

    public void SetPosition(Vector3 position)
    {
        _flag.transform.position = position;
    }

    public Flag CreateFlag(Vector3 position)
    {
        _flag = Instantiate(_flagPrefab, position, Quaternion.identity);
        _material = _flag.GetComponent<MeshRenderer>().material;

        return _flag;
    }
}
