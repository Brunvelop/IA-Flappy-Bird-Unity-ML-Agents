using System.Collections;
using UnityEngine;
using MLAgents;

public class HallwayAgent : Agent
{
    public GameObject ground;
    public GameObject area;
    public GameObject symbolOGoal;
    public GameObject symbolXGoal;
    public GameObject symbolO;
    public GameObject symbolX;
    public bool useVectorObs;
    RayPerception m_RayPer;
    Rigidbody m_AgentRb;
    Material m_GroundMaterial;
    Renderer m_GroundRenderer;
    HallwayAcademy m_Academy;
    int m_Selection;

    public override void InitializeAgent()
    {
        base.InitializeAgent();
        m_Academy = FindObjectOfType<HallwayAcademy>();
        m_RayPer = GetComponent<RayPerception>();
        m_AgentRb = GetComponent<Rigidbody>();
        m_GroundRenderer = ground.GetComponent<Renderer>();
        m_GroundMaterial = m_GroundRenderer.material;
    }

    public override void CollectObservations()
    {
        if (useVectorObs)
        {
            var rayDistance = 12f;
            float[] rayAngles = { 20f, 60f, 90f, 120f, 160f };
            string[] detectableObjects = { "symbol_O_Goal", "symbol_X_Goal", "symbol_O", "symbol_X", "wall" };
            AddVectorObs(GetStepCount() / (float)agentParameters.maxStep);
            AddVectorObs(m_RayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        }
    }

    IEnumerator GoalScoredSwapGroundMaterial(Material mat, float time)
    {
        m_GroundRenderer.material = mat;
        yield return new WaitForSeconds(time);
        m_GroundRenderer.material = m_GroundMaterial;
    }

    public void MoveAgent(float[] act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = Mathf.FloorToInt(act[0]);
        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
        }
        transform.Rotate(rotateDir, Time.deltaTime * 150f);
        m_AgentRb.AddForce(dirToGo * m_Academy.agentRunSpeed, ForceMode.VelocityChange);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        AddReward(-1f / agentParameters.maxStep);
        MoveAgent(vectorAction);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("symbol_O_Goal") || col.gameObject.CompareTag("symbol_X_Goal"))
        {
            if ((m_Selection == 0 && col.gameObject.CompareTag("symbol_O_Goal")) ||
                (m_Selection == 1 && col.gameObject.CompareTag("symbol_X_Goal")))
            {
                SetReward(1f);
                StartCoroutine(GoalScoredSwapGroundMaterial(m_Academy.goalScoredMaterial, 0.5f));
            }
            else
            {
                SetReward(-0.1f);
                StartCoroutine(GoalScoredSwapGroundMaterial(m_Academy.failMaterial, 0.5f));
            }
            Done();
        }
    }

    public override float[] Heuristic()
    {
        if (Input.GetKey(KeyCode.D))
        {
            return new float[] { 3 };
        }
        if (Input.GetKey(KeyCode.W))
        {
            return new float[] { 1 };
        }
        if (Input.GetKey(KeyCode.A))
        {
            return new float[] { 4 };
        }
        if (Input.GetKey(KeyCode.S))
        {
            return new float[] { 2 };
        }
        return new float[] { 0 };
    }

    public override void AgentReset()
    {
        var agentOffset = -15f;
        var blockOffset = 0f;
        m_Selection = Random.Range(0, 2);
        if (m_Selection == 0)
        {
            symbolO.transform.position =
                new Vector3(0f + Random.Range(-3f, 3f), 2f, blockOffset + Random.Range(-5f, 5f))
                + ground.transform.position;
            symbolX.transform.position =
                new Vector3(0f, -1000f, blockOffset + Random.Range(-5f, 5f))
                + ground.transform.position;
        }
        else
        {
            symbolO.transform.position =
                new Vector3(0f, -1000f, blockOffset + Random.Range(-5f, 5f))
                + ground.transform.position;
            symbolX.transform.position =
                new Vector3(0f, 2f, blockOffset + Random.Range(-5f, 5f))
                + ground.transform.position;
        }

        transform.position = new Vector3(0f + Random.Range(-3f, 3f),
            1f, agentOffset + Random.Range(-5f, 5f))
            + ground.transform.position;
        transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        m_AgentRb.velocity *= 0f;

        var goalPos = Random.Range(0, 2);
        if (goalPos == 0)
        {
            symbolOGoal.transform.position = new Vector3(7f, 0.5f, 22.29f) + area.transform.position;
            symbolXGoal.transform.position = new Vector3(-7f, 0.5f, 22.29f) + area.transform.position;
        }
        else
        {
            symbolXGoal.transform.position = new Vector3(7f, 0.5f, 22.29f) + area.transform.position;
            symbolOGoal.transform.position = new Vector3(-7f, 0.5f, 22.29f) + area.transform.position;
        }
    }
}
