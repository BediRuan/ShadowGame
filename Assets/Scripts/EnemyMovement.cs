using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // ���Ŀ��
    private NavMeshAgent navMeshAgent;
    public float followDistance = 10.0f; // ���˸�����ҵ�������
    private bool isStopped = false; // �ƶ�״̬����
    private bool isExternallyStopped = false; // �ⲿ��ͣ��־

    void Start()
    {
        // ��ȡ NavMeshAgent ���
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // ����ⲿ��ͣ��Ч����ֹͣ���
        if (isExternallyStopped)
        {
            navMeshAgent.isStopped = true;
            return;
        }

        if (player != null)
        {
            // ������˺����֮��ľ���
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= followDistance)
            {
                // ����ڸ��淶Χ�ڣ������ƶ������λ��
                isStopped = false;
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                // ���������Χ��ֹͣ�����ƶ�
                isStopped = true;
                navMeshAgent.isStopped = true;
            }
        }
    }

    // ��ͣ��ָ��ƶ�
    public void StopMovement(bool stop)
    {
        isExternallyStopped = stop; // �����ⲿ��ͣ��־
        navMeshAgent.isStopped = stop; // ��ͣ��ָ� NavMeshAgent
    }
}
