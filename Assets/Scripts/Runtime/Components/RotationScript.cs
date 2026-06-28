using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float speed = 1f;

    private Quaternion _initialRotation;

    void Start()
    {
        _initialRotation = transform.localRotation;
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * speed) * 30f;
        transform.localRotation = _initialRotation * Quaternion.AngleAxis(angle, Vector3.forward);
    }
}