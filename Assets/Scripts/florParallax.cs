using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class florParallax : MonoBehaviour
{
    public float velocidadParallax = 1.77f;
    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.localPosition.x <= -0.706f)
            transform.localPosition = Vector3.right * -0.228f;

        transform.localPosition = transform.localPosition +  new Vector3(-velocidadParallax * Time.deltaTime, 0, 0);
    }
}
