using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
   
    public RectTransform Label, Origin;
    public AnimationCurve WidthCurve;

    LineRenderer lr;
    int vertices=20;
    float path;
    float fraction;
    // Start is called before the first frame update
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();

        lr.positionCount = vertices;

        
        path = (Label.position - Origin.position).magnitude;
        fraction = path / (vertices-1);


        VerticesUpdate();




        lr.widthCurve = WidthCurve;





    }

    // Update is called once per frame
    void Update()
    {
        VerticesUpdate();

    }

    void VerticesUpdate()
    {

        for (int i = 0; i < vertices; i++)
        {
            lr.SetPosition(i, Vector3.Lerp(Origin.position, Label.position, (i * fraction) / path));
        }
    }
}
