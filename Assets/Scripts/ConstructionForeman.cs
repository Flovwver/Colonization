using UnityEngine;

public class ConstructionForeman : MonoBehaviour
{
    [SerializeField] private MouseRaycastSelector _mouseRaycastSelector;
    [SerializeField] private Flag _flagPrefab;

    private Throne _selectedThrone;

    private void OnEnable()
    {
        _mouseRaycastSelector.ThroneClicked += OnThroneClicked;
        _mouseRaycastSelector.GroundClicked += OnGroundClicked;
    }

    private void OnDisable()
    {
        _mouseRaycastSelector.ThroneClicked -= OnThroneClicked;
        _mouseRaycastSelector.GroundClicked -= OnGroundClicked;
    }

    private void OnThroneClicked(Throne throne)
    {
        if (_selectedThrone != null)
        {
            //_selectedThrone.SetSelected(false);
        }

        _selectedThrone = throne;
        //_selectedThrone.SetSelected(true);
    }

    private void OnGroundClicked(Vector3 position)
    {
        if (_selectedThrone == null)
        {
            return;
        }

        var flag = Instantiate(_flagPrefab, position, Quaternion.identity);

        _selectedThrone.SendUnitToBuild(flag);
    }
}
