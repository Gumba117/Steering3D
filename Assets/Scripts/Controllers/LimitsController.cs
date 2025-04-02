using UnityEngine;

public class LimitsController : MonoBehaviour
{
    public float topLimit;
    public float bottomLimit;
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLimit, topLimit), 0.5f, Mathf.Clamp(transform.position.z, bottomLimit, topLimit));
    }
}
