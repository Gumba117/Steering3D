using UnityEngine;

public class Limits : MonoBehaviour
{
    public float topLimit;
    public float bottomLimit;
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLimit, topLimit), 0, Mathf.Clamp(transform.position.z, bottomLimit, topLimit));
    }
}
