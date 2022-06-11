using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SphereState { Healthy, Sick, Recovered}
public class SphereMovement : MonoBehaviour
{
    public float Velocity=100;
    public float InfectionProbability = 25;
    public float ReInfectionProbability = 10;

    public float HealingTime = 10;
    public bool Reinfect;
    public SphereState State { get; protected set; }

    bool startHealing;
    Rigidbody rb;
    Color c;
    float timer;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f, 1f)).normalized * Velocity;
    }

    private void FixedUpdate()
    {
        //Vector3 dir = transform.position + transform.forward * Velocity * Time.fixedDeltaTime;
        //rb.MovePosition(dir);

        rb.velocity = rb.velocity.normalized * Velocity;
    }

    private void Update()
    {
       

        if (startHealing)
        {
            timer += Time.deltaTime;

            if (timer >= HealingTime)
            {
                SetState(SphereState.Recovered, Color.blue);
                UIGraphValuesProviderStart.RecoveredCounter++;
                UIGraphValuesProviderStart.InfectCounter--;
                startHealing = false;
                timer = 0;
            }


        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SphereMovement s;

        if(collision.gameObject.TryGetComponent<SphereMovement>(out s))
        {
            if (State == SphereState.Sick)
            {
                if (s.State == SphereState.Healthy)
                {
                    float percentage = InfectionProbability / 100;
                    float probability = Random.Range(0f, 1f);
                    if (probability <= percentage)
                    {
                        s.SetState(SphereState.Sick,c);
                        UIGraphValuesProviderStart.InfectCounter++;
                    }
                }
                else if (s.State == SphereState.Recovered)
                {
                    if (Reinfect)
                    {
                        float percentage = ReInfectionProbability / 100;
                        float probability = Random.Range(0f, 1f);
                        if (probability <= percentage)
                        {
                            s.SetState(SphereState.Sick, c);
                            UIGraphValuesProviderStart.RecoveredCounter--;
                            UIGraphValuesProviderStart.InfectCounter++;

                        }
                    }
                }

            }


        }
    }

    public void SetState(SphereState state, Color color)
    {
        State = state;
        
        if (State == SphereState.Sick)
            startHealing = true;

        c = color;
        GetComponent<MeshRenderer>().material.color = c;
    }
}
