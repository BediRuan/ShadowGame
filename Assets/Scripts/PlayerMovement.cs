using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;       // �ƶ��ٶ�
    public float mouseSensitivity = 100f; // ���������
    public Transform playerBody;      // ��ɫ����
    public Camera playerCamera;       // ��һ�˳������
    private float xRotation = 0f;     // �����������ת�Ƕ�

    void Start()
    {
        // ���ز��������ָ������Ļ����
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook(); // �����ӽ���ת
        HandleMovement();  // ���ƽ�ɫ�ƶ�
    }

    // ���������ת
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ������ת������������ת�Ƕ��� -90�� �� 90�� ֮�䣩
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Ӧ���������������ת
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Ӧ�ý�ɫ��ˮƽ��ת
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // �����ɫ�ƶ�
    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D �� ��/�Ҽ�ͷ
        float moveZ = Input.GetAxis("Vertical");   // W/S �� ��/�¼�ͷ

        // ���ݵ�ǰ�������������ƶ�����
        Vector3 moveDirection = playerBody.forward * moveZ + playerBody.right * moveX;
        moveDirection = moveDirection.normalized;

        // �ƶ���ɫ
        playerBody.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}