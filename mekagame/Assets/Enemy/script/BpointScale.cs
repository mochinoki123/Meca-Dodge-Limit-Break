using UnityEngine;

public class BpointScale : MonoBehaviour
{
    public Vector3 bpointscale = new Vector3 (10f, 0.01f, 10f);
    public float speed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, bpointscale, speed * Time.deltaTime);
    }
}
