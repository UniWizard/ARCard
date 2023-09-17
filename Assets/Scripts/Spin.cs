using UnityEngine;


/// <summary>
/// Rotates the attached <c>GameObject</c> in y-direction.
/// </summary>
public class Spin : MonoBehaviour
{

    private Transform _transform;
    private GameObject _gameObject;

    void Awake()
    {
        _transform = this.transform;
        _gameObject = this.gameObject;
    }


    /// <summary>
    /// if active, update rotatation by a bit.
    /// </summary>
    void Update()
    {
        if (_gameObject.activeSelf)
        {
            _transform.Rotate(new Vector3(0, 0.5f, 0));
        }
    }
}
