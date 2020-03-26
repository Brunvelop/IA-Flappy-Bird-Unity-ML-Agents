using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;

public class BirdAgent : Agent
{
    public birdController birdController;

    private Rigidbody2D rb;

    private bool dead;

    public Text intText;
    private int intento;

    private void Start()
    {
        birdController = GetComponent<birdController>();
        rb = this.GetComponent<Rigidbody2D>();
        dead = false;
        intento = 1;
    }

    public override void AgentReset()
    {
        birdController.Reset();
        dead = false;
    }

    public override void CollectObservations()
    {
        float posY = (transform.position.y - 2.25f)/(9.79f -2.25f);
        float vY = rb.velocity.y / 17f;

        float[] alturas = birdController.controlador.alturasPipetas();

        float altPU = (alturas[0] - 2.25f) / (9.79f - 2.25f);
        float altPD = (alturas[1] - 2.25f) / (9.79f - 2.25f);

        AddVectorObs(posY);
        AddVectorObs(vY);
        AddVectorObs(birdController.controlador.distanciaTubo());
        AddVectorObs(altPU - posY);
        AddVectorObs(posY - altPD);
        

    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //Monitor.Log("Reward", GetCumulativeReward());
        //Monitor.Log("Salto", vectorAction[0]);
        print(vectorAction[0]);

        int salto = Mathf.FloorToInt(vectorAction[0]);

        if (salto == 0)
        {
            print(salto);
            birdController.salto();
        }
        else if (salto == 1)
        {
            
        }

        if (dead)
        {
            print(dead);
            AddReward(-1f);
            Done();
            intento++;
            intText.text = "Intento:" + intento;
        }

        AddReward(0.001f);

    }

    public override float[] Heuristic()
    {
        float[] action = new float[1];

        if (Input.GetMouseButtonDown(0))
        {
            action[0] = 1;
        }
        else
        {
            action[0] = 0;
        }

        return action;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dead = true;
    }
}
