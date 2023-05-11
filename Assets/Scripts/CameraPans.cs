using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPans : MonoBehaviour
{
    public string PanningType;
    public GameObject[] players = new GameObject[0];
    public GameObject target;
    public float zoomSize;
    public float maxZoomSize;
    public float minZoomSize;
    // Start is called before the first frame update
    void Start()
    {
        PanningType = "Players";
        zoomSize = 5;
        maxZoomSize = 15;
        minZoomSize = 5;
    }


    Vector3[] distance = { Vector3.zero, Vector3.zero };
    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        //Awesome Math that I hate
        if (players.Length > 1)
        {
            distance = new Vector3[] { Vector3.zero, Vector3.zero };
            foreach (GameObject playerA in players)
            {
                foreach (GameObject playerB in players)
                {
                    if (Vector3.Distance(playerA.transform.position, playerB.transform.position) > Vector3.Distance(distance[0], distance[1]))
                    {
                        distance[0] = playerA.transform.position;
                        distance[1] = playerB.transform.position;
                    }
                }
            }
        }
        switch (PanningType)
        {
            case "Players": //x is average of all players' doesn't move if within a certain y methinks?
                if (players.Length > 1) {
                    float sumx = 0;
                    float sumy = 0;
                    foreach (GameObject player in players)
                    {
                        sumx += player.transform.position.x;
                        sumy += player.transform.position.y;
                    }
                    Vector3 targetPoint = new Vector3(sumx/players.Length, sumy / players.Length, -10);
                    if (Vector3.Distance(transform.position, targetPoint) > .3f)
                        transform.position = Vector3.MoveTowards(transform.position, targetPoint, 10 * Time.deltaTime);
                    else
                    {
                        transform.position = targetPoint;
                    }
                    if (((Mathf.Abs(distance[0].x - distance[1].x) * .275f > zoomSize * (2f / 3f)) || (Mathf.Abs(distance[0].y - distance[1].y) * .495f > zoomSize * (2f / 3f))) && zoomSize < maxZoomSize)
                    {
                        zoomSize += .01f;
                    }
                    else if (((Mathf.Abs(distance[0].x - distance[1].x) * .275f < zoomSize * (1f / 2f)) && (Mathf.Abs(distance[0].y - distance[1].y) * .495f < zoomSize * (1f / 2f))) && zoomSize > minZoomSize)
                    {
                        zoomSize -= .01f;
                    }
                }
                if (players.Length == 1)
                {
                    zoomSize = 5;
                    Vector3 targetPoint = new Vector3(players[0].transform.position.x, players[0].transform.position.y + 2, -10);
                    transform.position = Vector3.MoveTowards(transform.position, targetPoint, 10 * Time.deltaTime);
                }
                break;
            case "Target":
                if (target != null)
                {
                    if (Vector3.Distance(transform.position, target.transform.position) > .1f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 20 * Time.deltaTime);
                    }
                    else
                        transform.position = target.transform.position;

                    zoomSize = (target.GetComponent<BoxCollider2D>().size.x + 2) * .285f;
                }
                break;
        }
        //zooming
        if (Mathf.Abs(GetComponent<Camera>().orthographicSize - zoomSize) > .02f)
        {
            if (GetComponent<Camera>().orthographicSize > zoomSize)
                GetComponent<Camera>().orthographicSize -= .01f;
            else
                GetComponent<Camera>().orthographicSize += .01f;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
