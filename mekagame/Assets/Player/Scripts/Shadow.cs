using UnityEngine;

public class Shadow : MonoBehaviour
{
    private float y_currentTransform;
    void Start()
    {
        y_currentTransform = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = y_currentTransform;
        transform.position = pos;
    }
}
