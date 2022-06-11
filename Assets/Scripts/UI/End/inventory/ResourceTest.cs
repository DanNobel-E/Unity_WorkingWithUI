using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTest : MonoBehaviour
{
    public bool LoadFromResource;
    public string LoadingPath;

    void Update()
    {
        if (LoadFromResource)
        {
            LoadFromResource = false;
            Instantiate(Resources.Load<GameObject>(LoadingPath), transform.position, Random.rotation, transform);
        }

    }
}
