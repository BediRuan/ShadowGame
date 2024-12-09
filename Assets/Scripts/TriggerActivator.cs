using UnityEngine;

public class TriggerActivator : MonoBehaviour
{
    // 可被激活的物体列表
    public ActivateAfterDelay[] objectsToActivate;

    // 玩家进入触发器
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (ActivateAfterDelay obj in objectsToActivate)
            {
                obj.ActivateWithDelay();
            }
        }
    }
}
