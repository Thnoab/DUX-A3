using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] 
    private float rotationSpeed = 10f; // 控制旋转速度（度/秒）

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}