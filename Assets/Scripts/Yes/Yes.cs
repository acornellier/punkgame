using UnityEngine;

public class Yes : MonoBehaviour
{
    [SerializeField] GameObject yes;
    
    Vector2 _screenHalfSizeWorldUnits;

    void Start()
    {
        _screenHalfSizeWorldUnits = new Vector2(
            Camera.main.aspect * Camera.main.orthographicSize,
            Camera.main.orthographicSize
        );
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Yess();
    }

    void Yess()
    {
        var x = Random.Range(-_screenHalfSizeWorldUnits.x, _screenHalfSizeWorldUnits.x);
        var y = Random.Range(-_screenHalfSizeWorldUnits.y, _screenHalfSizeWorldUnits.y);
        var yess = Instantiate(yes, new Vector3(x, y, 0), Quaternion.identity);
        yess.transform.localScale *= Random.Range(1, 3);
    }
}
