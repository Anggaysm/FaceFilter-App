using UnityEngine;

public class ModelController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float zoomSpeed = 2f;
    public float minScale = 0.5f;
    public float maxScale = 2f;

    void Update()
    {
        // Rotasi menggunakan sentuhan/mouse drag
        if (Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, -rotX, Space.World);
            transform.Rotate(Vector3.right, rotY, Space.World);
        }

        // Zoom menggunakan scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            float newScale = Mathf.Clamp(transform.localScale.x + scroll * zoomSpeed, minScale, maxScale);
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
