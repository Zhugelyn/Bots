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

    public void StartSmoke(ParticleSystem particleSystem, Color color)
    {
        var main = particleSystem.main;
        main.startColor = color;
        particleSystem.Play();
    }

    public void StopSmoke(ParticleSystem particleSystem)
    {
        if (particleSystem.isPlaying)
            particleSystem.Stop();
    }
}
