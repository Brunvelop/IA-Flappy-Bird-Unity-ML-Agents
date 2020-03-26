using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdController : MonoBehaviour
{

    public Controlador controlador;

    public float fSalto;

    private Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    float initY = 7;
    float initX = 1.25f;
    public void Reset()
    {
        controlador.Reset();
        rb.velocity = Vector3.zero;
        transform.localPosition = new Vector3( initX, initY, 0);
        if (transform.parent.name == "Escenario")
            FindObjectOfType<AudioManager>().Play("hit");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        animacion();

        //descomentar para juego NO entrenamiento
        /*
        if (Input.GetMouseButtonDown(0))
        {
            salto();
        }
        */
        
    }

    private void animacion()
    {
        Vector2 v = rb.velocity;
        
        float angulo = Mathf.Atan2(10f, v.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(90 - angulo, Vector3.forward);
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Reset();
    }
    */
    public void salto()
    {
        rb.velocity = Vector2.up * fSalto;
        if (transform.parent.name == "Escenario")
            FindObjectOfType<AudioManager>().Play("salto");
    }

    
}
