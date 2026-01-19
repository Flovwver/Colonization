using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void GoToSpawner(Vector3 spawnerPosition)
    {
        _rigidbody.MovePosition(Vector3.MoveTowards(transform.position, spawnerPosition, _speed * Time.fixedDeltaTime));
    }
}
