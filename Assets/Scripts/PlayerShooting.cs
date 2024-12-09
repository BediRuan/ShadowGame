using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;    // �ӵ���Ԥ����
    public Camera playerCamera;        // ���ʹ�õ������
    public float bulletSpeed = 20f;    // �ӵ��ٶ�
    public float fireRate = 0.5f;      // ������ʱ��
    private float nextFireTime = 0f;   // �´������ʱ��

    void Update()
    {
        HandleShooting();
    }

    void HandleShooting()
    {
        // ���������������������
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) // ���������
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // �����´����ʱ��
        }
    }

    void Shoot()
    {
        // ����һ������Ļ���ĵ�����
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // ��������λ��Ϊ�������ǰ��
        Vector3 spawnPosition = playerCamera.transform.position + ray.direction.normalized * 1f; // �����������Զ�ĵ�
        Quaternion spawnRotation = Quaternion.LookRotation(ray.direction); // �������߷��������ӵ���ת

        // ʵ�����ӵ�
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);

        // �����ӵ����ٶ�
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = ray.direction * bulletSpeed;
        }

        // �Զ������ӵ��������������
        Destroy(bullet, 5f);
    }
}
