using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowArea : MonoBehaviour
{
    public Transform shadowCaster; // Enemy
    public Light sceneLight;       // Light Source
    public Collider shadowCollider; // Collider
    public float sizeMultiplier = 1.0f; 

    void Update()
    {
        UpdateShadowArea();
    }

    void UpdateShadowArea()
    {
        //calculate light dirction and scale
        Vector3 lightDir = sceneLight.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(shadowCaster.position, -lightDir, out hit))
        {
            //point of shadow
            Vector3 shadowPosition = hit.point;

            //change shadow size base on distance
            float distance = Vector3.Distance(shadowCaster.position, shadowPosition);

            //change size
            Vector3 newSize = Vector3.one * distance * sizeMultiplier;
            if (shadowCollider is BoxCollider box)
            {
                box.size = newSize;
                box.center = new Vector3(0, -distance / 2, 0); 
            }
        }
    }
}
