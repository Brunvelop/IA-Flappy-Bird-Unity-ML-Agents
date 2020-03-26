using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TuboController : MonoBehaviour
{
    public GameObject score;

    public GameObject pUp;
    public GameObject pDown;

    public float velodicad;

    private Rigidbody2D rb;

    public bool point;

    public void pointTrue()
    {
        point = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        point = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        movimiento();

        if (this.transform.localPosition.x < -0.5f)
        {
            destroy();
        }

    }

    public void movimiento()
    {
        rb.velocity = Vector2.left * velodicad;
    }

    public void instanciaTubo()
    {
        
    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
