using UnityEngine;
using TMPro;

public class CoalStorageUI : MonoBehaviour
{
    [SerializeField] private CoalStorage _storage;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _storage.StoredCoalCountChanged += SetCount;
    }

    private void OnDisable()
    {
        _storage.StoredCoalCountChanged -= SetCount;
    }

    public void SetCount(int value)
    {
        _text.text = value.ToString();
    }
}
