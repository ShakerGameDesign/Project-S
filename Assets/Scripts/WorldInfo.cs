using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInfo
{
    public static WorldInfo i = new WorldInfo();
    public static Player player;
    private WorldInfo() { }

    public float worldLocation { get; private set; }
    public static int camSideViewDistance = 35;

    public static void setWorldLoc(float nwl)
    {
        i.worldLocation = nwl;
        player.cameraAnchor.transform.position = new Vector3(i.worldLocation, 0, PathGenerator.pathGen.GetPathCenterAtPos(camSideViewDistance));
        PathGenerator.pathGen.transform.position = PathGenerator.pathGen.transform.position.SX(i.worldLocation - camSideViewDistance);
    }
}
