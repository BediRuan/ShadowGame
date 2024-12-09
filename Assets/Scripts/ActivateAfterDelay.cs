using System.Collections;
using UnityEngine;

public class ActivateAfterDelay : MonoBehaviour
{
    public float delay = 3f; // ÑÓ³ÙÊ±¼ä

    public void ActivateWithDelay()
    {
        Invoke("ActivateObject", delay);
    }

    private void ActivateObject()
    {
        gameObject.SetActive(true);
    }

    private void Start()
    {
        gameObject.SetActive(false); // ³õÊ¼×´Ì¬ÎªÒþ²Ø
    }
}
