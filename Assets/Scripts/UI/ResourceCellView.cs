using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceCellView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    private void OnValidate()
    {
        Debug.Assert(_text != null);
    }

    public void SetText(int count)
    {
        _text.text = count.ToString();
    }
}
