using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateCircle : MonoBehaviour
{
    public List<GameObject> Prefabs= new List<GameObject>();
    public int N;
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

    // Start is called before the first frame update
    void Start()
    {
        

        if (Inside)
        {
            for (int i = 0; i < N; i++)
            {

                int randomIndex = Random.Range(0, Prefabs.Count);
                int randomTextIndex = Random.Range(1, 10);

                GameObject obj = Instantiate(Prefabs[randomIndex], Parent.transform.position, Parent.transform.rotation, Parent);
                Vector2 position = Random.insideUnitCircle * Radius;
                obj.transform.position = new Vector3(position.x+1,1, position.y+1);
                obj.GetComponent<PickableItem_>().TextIndex.text = randomTextIndex.ToString();
                obj.GetComponent<Animator>().SetBool("Spawn", true);
                RandomRotate(obj);

                RandomScaling(obj);


            }



        }
        else
        {

            for (int i = 0; i < N; i++)
            {
                int randomIndex = Random.Range(0, Prefabs.Count);
                GameObject obj = Instantiate(Prefabs[randomIndex], Parent.transform.position, Parent.transform.rotation, Parent);

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
