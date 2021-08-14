using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOnPath : MonoBehaviour
{
    void Update()
    {
        transform.position = transform.position.SZ(PathGenerator.pathGen.GetPathCenterAtPos(transform.position.x));
    }
}
