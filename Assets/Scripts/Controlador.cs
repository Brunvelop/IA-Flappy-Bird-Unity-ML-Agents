using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
    public GameObject tubo;
    public GameObject bird;

    public GameObject score;

    [Range(1f, 100f)]
    public float timeSpeed;

    

    private float t;
    // Start is called before the first frame update
    void Start()
    {
        timeSpeed = 1;
        t = 2;
    }

    private void Update()
    {
        Time.timeScale = timeSpeed;
    }

    public void Reset()
    {
        

        foreach (Transform child in this.transform)
        {
            
            child.GetComponent<TuboController>().destroy();

            if (child.localPosition.x < 1.25f)
            {
                score.GetComponent<Score>().addScore();
                
            }
        }

        if (transform.parent.name == "Escenario")
            FindObjectOfType<AudioManager>().Play("hit");

        score.GetComponent<Score>().Reset();
    }



    // Update is called once per frame
    void LateUpdate()
    {

        creaTubo();

        foreach (Transform child in this.transform)
        {

            if (child.localPosition.x < 1.25f && !child.GetComponent<TuboController>().point)
            {
                score.GetComponent<Score>().addScore();
                child.GetComponent<TuboController>().pointTrue();

                if (transform.parent.name == "Escenario")
                    FindObjectOfType<AudioManager>().Play("point");
            }
        }

    }

    GameObject newTubo;
    public void creaTubo()
    {
        t += Time.deltaTime;

        if (t > 2f)
        {
            newTubo = Instantiate(tubo, transform.position + new Vector3(6.5f, Random.Range(4, 8), 0), Quaternion.identity, this.transform);
            newTubo.name = "tubo";
            t = 0;
        }
        
    }

    public float distanciaTubo()
    {
        float distancia = 1;

        foreach (Transform child in this.transform)
        {
            float d = (child.localPosition.x - 1.25f)/(6.5f - 1.25f);

            if (child.localPosition.x > 0 && d < distancia)
            {
                distancia = d;
            }
        }

        return distancia;
    }

    public float[] alturasPipetas()
    {
        float distancia = 1;
        Transform tubo = null;
        float pU = 1;
        float pD = 1;
        float[] res = new float[2];
        res[0] = pU;
        res[1] = pD;

        foreach (Transform child in this.transform)
        {
            float d = (child.localPosition.x - 1.25f) / (6.5f - 1.25f);

            if (child.localPosition.x > 0 && d < distancia)
            {
                distancia = d;
                tubo = child;
            }
        }

        if (tubo)
        {
            pD = tubo.GetComponent<TuboController>().pDown.transform.position.y;
            pU = tubo.GetComponent<TuboController>().pUp.transform.position.y;
            res[0] = pU;
            res[1] = pD;
        }

        return res;
    }


}
