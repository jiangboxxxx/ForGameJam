using UnityEngine;

public class SelfRotation : MonoBehaviour
{
    public float rotationSpeed = 50f; // ��ת�ٶ�

    void Update()
    {
        // ��ÿһ֡�����趨���ٶ����������Y����ת
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
