using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float speed;
    [SerializeField] int camFollowDist;
    [SerializeField] float baseSpeed;
    [SerializeField] GameObject goHereIndicatorPrefab;
    [SerializeField] GameObject mouseTrackedObject;
    //public GameObject cameraAnchor;

    private Rigidbody rb;
    private GameObject goHereIndicator;
    private Vector3 moveGoalPos;
    private bool atMoveGoalPos;

    // Start is called before the first frame update
    public void PlayerStart()
    {
        rb = GetComponent<Rigidbody>();
        WorldInfo.player = this;
        transform.position = transform.position.SZ(PathGenerator.pathGen.GetPathCenterAtPos(WorldInfo.camSideViewDistance));
        WorldInfo.setWorldLoc(transform.position.x + camFollowDist);
        goHereIndicator = Instantiate(goHereIndicatorPrefab);
        goHereIndicator.GetComponent<Renderer>().enabled = false;
        goHereIndicator.GetComponent<LineRenderer>().enabled = false;
        atMoveGoalPos = true;
    }

    // Update is called once per frame
    public void PlayerUpdate()
    {
        goHereIndicator.GetComponent<LineRenderer>().SetPosition(0, transform.position.SY(0.1f));
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            mouseTrackedObject.GetComponent<LineRenderer>().enabled = true;
            mouseTrackedObject.transform.position = hit.point;
            mouseTrackedObject.GetComponent<LineRenderer>().SetPosition(0, transform.position.SY(0.1f));
            mouseTrackedObject.GetComponent<LineRenderer>().SetPosition(1, mouseTrackedObject.transform.position);
            if (Input.GetMouseButtonDown(1))
            {
                var hiP = hit.point;
                if (hit.collider.CompareTag("NotRayMoveTo"))
                {
                    hiP = GetClosestPointToEdge(transform.position, hiP);
                    Vector3 GetClosestPointToEdge(Vector3 startPoint, Vector3 endPoint, int recurNum = 0)
                    {
                        Vector3 newStartPoint;
                        Vector3 newEndPoint;

                        //check if the half way point is on the path or not
                        var halfPoint = new Vector3((startPoint.x + endPoint.x) / 2, endPoint.y, (startPoint.z + endPoint.z) / 2);

                        var cenP = PathGenerator.pathGen.GetPathCenterAtPos(halfPoint.x);

                        if (cenP + (PathGenerator.pathGen.width / 2) >= halfPoint.z && cenP - (PathGenerator.pathGen.width / 2) <= halfPoint.z)
                        {
                            //on path
                            newStartPoint = halfPoint;
                            newEndPoint = endPoint;
                        }
                        else
                        {
                            //off path
                            newStartPoint = startPoint;
                            newEndPoint = halfPoint;
                        }

                        if (Vector3.Distance(newStartPoint, newEndPoint) < 0.05f || recurNum > 100)
                        {
                            return newEndPoint;
                        }
                        else
                        {
                            return GetClosestPointToEdge(newStartPoint, newEndPoint, recurNum + 1);
                        }
                    }
                }
                goHereIndicator.transform.position = hiP;
                goHereIndicator.GetComponent<Renderer>().enabled = true;
                goHereIndicator.GetComponent<LineRenderer>().enabled = true;
                goHereIndicator.GetComponent<LineRenderer>().SetPosition(1, hiP.SY(0.1f));
                moveGoalPos = hiP;
                atMoveGoalPos = false;
            }
        }
        else
        {
            mouseTrackedObject.GetComponent<LineRenderer>().enabled = false;
        }
        if (!atMoveGoalPos)
        {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(moveGoalPos.x, moveGoalPos.z)) < 0.1f)
            {
                goHereIndicator.GetComponent<Renderer>().enabled = false;
                goHereIndicator.GetComponent<LineRenderer>().enabled = false;
                atMoveGoalPos = true;
            }
            else
            {
                var p = transform.position;
                var rot = Mathf.Atan2(moveGoalPos.z - transform.position.z, moveGoalPos.x - transform.position.x);
                p.x += Mathf.Cos(rot) * Time.deltaTime * baseSpeed;
                p.z += Mathf.Sin(rot) * Time.deltaTime * baseSpeed;
                transform.position = p;
            }
        }
        if (transform.position.x > WorldInfo.i.worldLocation - camFollowDist)
        {
            WorldInfo.setWorldLoc(transform.position.x + camFollowDist);
        }
    }
}
