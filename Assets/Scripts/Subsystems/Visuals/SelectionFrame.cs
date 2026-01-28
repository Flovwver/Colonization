using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SelectionFrame : MonoBehaviour
{
    [SerializeField] private Throne _throne;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _throne.OnSelected += ShowFrame;
    }

    private void OnDisable()
    {
        _throne.OnSelected -= ShowFrame;
    }

    private void ShowFrame(bool isSelected)
    {
        _renderer.enabled = isSelected;
    }
}
