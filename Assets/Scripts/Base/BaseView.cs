using UnityEngine;
using UnityEngine.UI;

public class BaseView : MonoBehaviour
{
    [SerializeField] private Image _icon;

    public void SetColorBase(Color baseColor)
    {
        baseColor.a = 1f;
        _icon.color = baseColor;;
    }
}
