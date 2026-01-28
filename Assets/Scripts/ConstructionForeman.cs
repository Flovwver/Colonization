using UnityEngine;

public class ConstructionForeman : MonoBehaviour
{
    [SerializeField] private MouseRaycastSelector _mouseRaycastSelector;
    [SerializeField] private Flag _flagPrefab;

    private Flag _flagInstance;
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
            _selectedThrone.SetSelected(false);
        }

        _selectedThrone = throne;
        _selectedThrone.SetSelected(true);

        if (_flagInstance != null)
            Destroy(_flagInstance.gameObject);
    }

    private void OnGroundClicked(Vector3 position)
    {
        if (_selectedThrone == null)
        {
            return;
        }

        if (_flagInstance == null)
        {
            _flagInstance = Instantiate(_flagPrefab, position, Quaternion.identity);
            _selectedThrone.SendUnitToBuild(_flagInstance);
        }
        else
        {
            _flagInstance.transform.position = position;
        }
    }
}
