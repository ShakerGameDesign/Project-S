using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInfo : MonoBehaviour
{
    public static WorldInfo i;
    public static Player player;

    public GameObject worldLocAnchorGO;

    void Awake()
    {
        i = this;
    }

    public float worldLocation;
    public float PathCenAtWorldLoc;
    public static int camSideViewDistance = 35;

    public static void setWorldLoc(float nwl)
    {
        i.worldLocation = nwl;
        i.PathCenAtWorldLoc = PathGenerator.pathGen.GetPathCenterAtPos(i.worldLocation);
        i.worldLocAnchorGO.transform.position = i.worldLocAnchorGO.transform.position.SX(nwl);
        i.worldLocAnchorGO.transform.position = i.worldLocAnchorGO.transform.position.SZ(i.PathCenAtWorldLoc);

        //player.cameraAnchor.transform.position = new Vector3(i.worldLocation, 0, i.PathCenAtWorldLoc);
        PathGenerator.pathGen.transform.position = PathGenerator.pathGen.transform.position.SX(i.worldLocation - camSideViewDistance);
    }
}
