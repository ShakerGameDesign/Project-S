using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInfo : MonoBehaviour
{
    public static WorldInfo i;
    public static Player player;

    public GameObject worldLocAnchorGO;

    private void Start()
    {
        i = this;

    }

    public float worldLocation { get; private set; }
    public float PathCenAtWorldLoc { get; private set; }
    public static int camSideViewDistance = 35;

    public static void setWorldLoc(float nwl)
    {
        i.worldLocation = nwl;
        i.PathCenAtWorldLoc = PathGenerator.pathGen.GetPathCenterAtPos(camSideViewDistance);
        i.worldLocAnchorGO.transform.position = i.worldLocAnchorGO.transform.position.SX(nwl);
        i.worldLocAnchorGO.transform.position = i.worldLocAnchorGO.transform.position.SZ(i.PathCenAtWorldLoc);

        player.cameraAnchor.transform.position = new Vector3(i.worldLocation, 0, i.PathCenAtWorldLoc);
        PathGenerator.pathGen.transform.position = PathGenerator.pathGen.transform.position.SX(i.worldLocation - camSideViewDistance);
    }
}
