using UnityEngine;

public class TriggerActivator : MonoBehaviour
{
    // �ɱ�����������б�
    public ActivateAfterDelay[] objectsToActivate;

    // ��ҽ��봥����
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
