using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateCircleSpheres : MonoBehaviour
{
    public UIGraphValuesProviderStart Provider;
    public GameObject Prefab;
    public int N;
    public int InitSick=5;
    public Transform Parent;
    public bool Inside;
    public bool OutsideUniform;


    //Rotation
    public bool RandomRotation;

    //Scale
    public bool RandomUniformScale;
    public bool RandomScale;
    public float RandomMinScale;
    public float RandomMaxScale;

    public float Radius;

    int currSick;
    Color[] colors = new Color[3] { Color.green, Color.red, Color.blue };


    // Start is called before the first frame update
    void Start()
    {
        Provider.Init(N);

        if (Inside)
        {
            for (int i = 0; i < N; i++)
            {


                GameObject obj = Instantiate(Prefab, Parent.transform.position, Parent.transform.rotation, Parent);
                Vector2 position = Random.insideUnitCircle * Radius;
                obj.transform.position = new Vector3(position.x,0, position.y);
                SphereMovement s= obj.GetComponent<SphereMovement>();

                SphereState state;

                if (currSick < InitSick)
                {
                    state = SphereState.Sick;
                    UIGraphValuesProviderStart.InfectCounter++;
                    currSick++;
                }
                else
                {
                    state = SphereState.Healthy;
                }
                s.SetState(state, colors[(int)state]);


                RandomRotate(obj);

                RandomScaling(obj);


            }



        }
        else
        {

            for (int i = 0; i < N; i++)
            {
               
                GameObject obj = Instantiate(Prefab, Parent.transform.position, Parent.transform.rotation, Parent);

                float alpha;

                if (OutsideUniform)
                {
                    alpha = (360f / N)*i;

                }
                else
                {
                 alpha = Random.Range(0, 360f);

                }


                obj.transform.Rotate(0, 0, alpha, Space.World);
                Vector2 position = new Vector2(Mathf.Cos((alpha*Mathf.PI)/180)*Radius, Mathf.Sin((alpha * Mathf.PI) / 180)*Radius);
                obj.transform.localPosition = position;

                RandomRotate(obj);

                RandomScaling(obj);
            }


        }
        void RandomRotate(GameObject obj)
        {
            if (RandomRotation)
            {
                obj.transform.Rotate(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));

            }
        }

        void RandomScaling(GameObject obj)
        {
            if (RandomScale || RandomUniformScale)
            {
                if (RandomUniformScale)
                {
                    float random = Random.Range(RandomMinScale, RandomMaxScale);
                    obj.transform.localScale = new Vector3(random, random, random);

                }
                else
                {
                    obj.transform.localScale = new Vector3(Random.Range(RandomMinScale, RandomMaxScale), Random.Range(RandomMinScale, RandomMaxScale), Random.Range(RandomMinScale, RandomMaxScale));

                }

            }
        }

    }

}
