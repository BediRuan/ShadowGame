using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // 玩家目标
    private NavMeshAgent navMeshAgent;
    public float followDistance = 10.0f; // 敌人跟随玩家的最大距离
    private bool isStopped = false; // 移动状态控制
    private bool isExternallyStopped = false; // 外部暂停标志

    void Start()
    {
        // 获取 NavMeshAgent 组件
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // 如果外部暂停生效，则停止检查
        if (isExternallyStopped)
        {
            navMeshAgent.isStopped = true;
            return;
        }

        if (player != null)
        {
            // 计算敌人和玩家之间的距离
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= followDistance)
            {
                // 如果在跟随范围内，敌人移动到玩家位置
                isStopped = false;
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                // 如果超出范围，停止敌人移动
                isStopped = true;
                navMeshAgent.isStopped = true;
            }
        }
    }

    // 暂停或恢复移动
    public void StopMovement(bool stop)
    {
        isExternallyStopped = stop; // 设置外部暂停标志
        navMeshAgent.isStopped = stop; // 暂停或恢复 NavMeshAgent
    }
}
