using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;       // 移动速度
    public float mouseSensitivity = 100f; // 鼠标灵敏度
    public Transform playerBody;      // 角色身体
    public Camera playerCamera;       // 第一人称摄像机
    private float xRotation = 0f;     // 摄像机上下旋转角度

    void Start()
    {
        // 隐藏并锁定鼠标指针在屏幕中央
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook(); // 控制视角旋转
        HandleMovement();  // 控制角色移动
    }

    // 处理鼠标旋转
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 上下旋转（限制上下旋转角度在 -90° 到 90° 之间）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 应用摄像机的上下旋转
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 应用角色的水平旋转
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // 处理角色移动
    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D 或 左/右箭头
        float moveZ = Input.GetAxis("Vertical");   // W/S 或 上/下箭头

        // 根据当前摄像机方向计算移动方向
        Vector3 moveDirection = playerBody.forward * moveZ + playerBody.right * moveX;
        moveDirection = moveDirection.normalized;

        // 移动角色
        playerBody.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}