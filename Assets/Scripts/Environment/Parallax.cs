using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parralaxEffect;

    Vector3 _cameraStartPos;
    Vector3 _startPos;

    void Start()
    {
        _cameraStartPos = Camera.main.transform.position;
        _startPos = transform.position;
    }

    void Update()
    {
        var distFromStart = Camera.main.transform.position - _cameraStartPos;
        transform.position = new Vector3(
            _startPos.x + distFromStart.x * parralaxEffect,
            _startPos.y + distFromStart.y,
            transform.position.z
        );
    }
}