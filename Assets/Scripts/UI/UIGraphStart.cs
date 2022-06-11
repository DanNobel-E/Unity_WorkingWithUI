using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Draws a custom UI Mesh, based on RawValues[]
//[ExecuteInEditMode]
public class UIGraphStart: Graphic
{
    public Color TopCol, BottomCol, LineColor;
    float[] RawValues;       //Contains values from MinVal to MaxVal
    float[] NormValues= new float[500];             //Normalized values [0,1] from RawValues[], based on [MinVal, MaxVal]
    Vector2[] v;                    //UIcoordinates from NormValues[], based on UI RectTransform Width/Height
    float xStep;                    //Graph x values will start from bottom.left (pivot point), each sample will increase its x by xStep
    float xCurrVal=0;
    float MinVal = 0, MaxVal = 200;    //min/max value range of RawValues[]
    float t;
    float height;
    float width;
    float lineHeight = 0.025f;
    Color alphaLine;

    //Only for debug
    public Vector2 DebugTLv, DebugTRv, DebugBLv, DebugBRv;

    public void Init()
    {
        alphaLine = new Color(LineColor.r, LineColor.g, LineColor.b, 0);
        height = ((RectTransform)rectTransform.parent.transform).rect.height;
        width= ((RectTransform)rectTransform.parent.transform).rect.width;
        SetAllDirty();
    }
    //Call this method to update the graph
    public void UpdateGraphValues(float[] _RawValues)
    {
        RawValues = _RawValues;
        UpdateGraph();
    }

    public void SetxStep(float f)
    {
        xStep = width/f;
    }

    public void UpdateGraph()
    {
        SetAllDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (RawValues != null)
        {


            for (int i = 0; i < NormValues.Length; i++)
            {
                NormValues[i] = RawValues[i] / MaxVal;
            }
            //RawVal[] Contains values from MinVal to MaxVal
            //Transform RawVal into NormVal [0..1]: 0 will have vRaw.y = 0, 1 will have vRaw.y = GraphPanel.height


            int upIndex = 1;
            int downIndex = 0;
            int lineDepthIndex = 2;

            int lineUpIndex = 7;
            int lineDownIndex = 6;



            for (int i = 0; i < NormValues.Length - 1; i++)
            {
                if (i == 0)
                {
                    UIVertex vUI0 = UIVertex.simpleVert;
                    vUI0.position = new Vector3(0, 0, 0);
                    vUI0.color = BottomCol;
                    UIVertex vUI1 = UIVertex.simpleVert;
                    vUI1.position = new Vector3(0, 0, 0);
                    vUI1.color = BottomCol;
                    vh.AddVert(vUI0);
                    vh.AddVert(vUI1);

                    UIVertex vUI2 = UIVertex.simpleVert;
                    vUI2.position = new Vector3(0, lineHeight * height, 0);
                    vUI2.color = BottomCol;
                    UIVertex vUI3 = UIVertex.simpleVert;
                    vUI3.position = new Vector3(xStep, (NormValues[i + 1] * height) + (lineHeight * height), 0);
                    vUI3.color = BottomCol;

                    vh.AddVert(vUI2);
                    vh.AddVert(vUI3);

                }
                else
                {
                    UIVertex vUI3 = UIVertex.simpleVert;
                    vUI3.position = new Vector3(xCurrVal + xStep, (NormValues[i + 1] * height) + (NormValues[i + 1] != 0 ? (lineHeight * height) : 0), 0);
                    vUI3.color = BottomCol;

                    vh.AddVert(vUI3);

                }



                UIVertex vUI4 = UIVertex.simpleVert;
                vUI4.position = new Vector3(xCurrVal + xStep, NormValues[i + 1] * height, 0);
                vUI4.color = Color.Lerp(BottomCol, TopCol, NormValues[i + 1]);
                UIVertex vUI5 = UIVertex.simpleVert;
                vUI5.position = new Vector3(xCurrVal + xStep, 0, 0);
                vUI5.color = BottomCol;

                vh.AddVert(vUI4);
                vh.AddVert(vUI5);




                if (i == 0)
                {
                    vh.AddTriangle(downIndex, upIndex, upIndex + 3);
                    vh.AddTriangle(upIndex + 3, upIndex + 4, downIndex);
                    vh.AddTriangle(upIndex, lineDepthIndex, lineDepthIndex + 1);
                    vh.AddTriangle(lineDepthIndex + 1, upIndex + 3, upIndex);


                    lineDepthIndex += 1;
                    upIndex += 3;
                    downIndex += 5;
                }
                else if(i==1)
                {
                    ////Add 2 triangles to UIMesh
                    vh.AddTriangle(downIndex, upIndex, upIndex + 7);
                    vh.AddTriangle(upIndex + 7, downIndex + 7, downIndex);
                    vh.AddTriangle(upIndex, lineDepthIndex, lineDepthIndex + 7);
                    vh.AddTriangle(lineDepthIndex + 7, upIndex + 7, upIndex);

                    lineDepthIndex += 7;
                    upIndex += 7;
                    downIndex += 7;

                }
                else
                {
                    vh.AddTriangle(downIndex, upIndex, upIndex + 5);
                    vh.AddTriangle(upIndex + 5, downIndex + 5, downIndex);
                    vh.AddTriangle(upIndex, lineDepthIndex, lineDepthIndex + 5);
                    vh.AddTriangle(lineDepthIndex + 5, upIndex + 5, upIndex);

                    lineDepthIndex += 5;
                    upIndex += 5;
                    downIndex += 5;
                }

                //LineVertex
               
                    if (i == 0)
                    {
                        UIVertex vUI6 = UIVertex.simpleVert;
                        vUI6.position = new Vector3(0, lineHeight * height, 0);
                        vUI6.color = NormValues[i + 1] == 0 ? alphaLine : LineColor;
                    UIVertex vUI7 = UIVertex.simpleVert;
                        vUI7.position = new Vector3(0, (lineHeight * height) * 2, 0);
                        vUI7.color = NormValues[i+1]==0? alphaLine: LineColor;
                        vh.AddVert(vUI6);
                        vh.AddVert(vUI7);


                    }

                    UIVertex vUI8 = UIVertex.simpleVert;
                    vUI8.position = new Vector3(xCurrVal + xStep, (NormValues[i + 1] * height) + ((lineHeight * height) * 2), 0);
                    vUI8.color = NormValues[i + 1] == 0 ? alphaLine : LineColor;

                UIVertex vUI9 = UIVertex.simpleVert;
                    vUI9.position = new Vector3(xCurrVal + xStep, (NormValues[i + 1] * height) + (lineHeight * height), 0);
                    vUI9.color = NormValues[i + 1] == 0 ? alphaLine : LineColor;

                xCurrVal += xStep;

                    vh.AddVert(vUI8);
                    vh.AddVert(vUI9);

                if (i == 0)
                {
                    vh.AddTriangle(lineDownIndex, lineUpIndex, lineUpIndex + 1);
                    vh.AddTriangle(lineUpIndex + 1, lineUpIndex + 2, lineDownIndex);

                    lineUpIndex += 1;
                    lineDownIndex += 3;
                }
                else
                {
                    ////Add 2 triangles to UIMesh
                    vh.AddTriangle(lineDownIndex, lineUpIndex, lineUpIndex + 5);
                    vh.AddTriangle(lineUpIndex + 5, lineDownIndex + 5, lineDownIndex);

                    lineUpIndex += 5;
                    lineDownIndex += 5;

                }
               

            }

            xCurrVal = 0;

        }
        /* Split the graph in adjacent Quads. Every quad has 2 triangles. First quad is:
         * 1_____2
         * |    /|
         * |   / |
         * |  /  |
         * | /   |
         * |/    |
         * 0_____3
         * 
         * Based on: https://docs.unity3d.com/2019.1/Documentation/ScriptReference/UI.Graphic.html
         * Quad Triangles are: 0,1,2; 2,3,0
         */
        //vh.Clear();
        //UIVertex vUI0 = UIVertex.simpleVert;
        //vUI0.position = DebugBLv;
        //vUI0.color = Random.ColorHSV();
        //UIVertex vUI1 = UIVertex.simpleVert;
        //vUI1.position = DebugTLv;
        //vUI1.color = Random.ColorHSV();
        //UIVertex vUI2 = UIVertex.simpleVert;
        //vUI2.position = DebugTRv;
        //vUI2.color = Random.ColorHSV();
        //UIVertex vUI3 = UIVertex.simpleVert;
        //vUI3.position = DebugBRv;
        //vUI3.color = Random.ColorHSV();

        ////Add 4 vertices to UIMesh
        //vh.AddVert(vUI0);
        //vh.AddVert(vUI1);
        //vh.AddVert(vUI2);
        //vh.AddVert(vUI3);

        ////Add 2 triangles to UIMesh
        //vh.AddTriangle(0, 1, 2);
        //vh.AddTriangle(2, 3, 0);
    }
}
