using UnityEngine;
using TMPro;

public class CoalStorageUI : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void SetCount(int value)
    {
        _text.text = value.ToString();
    }
}
