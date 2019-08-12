using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_Loader : MonoBehaviour
{

    public Transform gate;
    public GameObject waypoint;
    public Gate_Loader loadedGates;

    Vector3 endPointOffset = new Vector3(-8, 0, 0);

    

    public Vector3[,] fullWaypointsList = new Vector3[10, 3];

    // Start is called before the first frame update
    void Awake()
    {
        int[] Rotations = loadedGates.Rotations;

        //loop through the gates
        for (int i = 0; i<10; i++)
        {
            gate = this.gameObject.transform.GetChild(i+10); // gets the gate as a child

            for (int wp = 0; wp<3; wp++)
            {
                waypoint = this.gameObject.transform.GetChild(i+10).GetChild(wp).gameObject; //gets the waypoints as children

                fullWaypointsList[i, wp] = waypoint.transform.position;

                if(Rotations[i] == 1 && wp == 2) // if it is 180 degrees rotated and is the end point then offest endpoint accordingly
                {
                    fullWaypointsList[i, wp] = waypoint.transform.position + endPointOffset;
                }
            }
            
        }

        /*print(Rotations[0]);
        print(fullWaypointsList[0,0]);
        print(fullWaypointsList[0,1]);
        print(fullWaypointsList[0,2]);

        print(Rotations[5]);
        print(fullWaypointsList[5, 0]);
        print(fullWaypointsList[5, 1]);
        print(fullWaypointsList[5, 2]);

        print(Rotations[9]);
        print(fullWaypointsList[9, 0]);
        print(fullWaypointsList[9, 1]);
        print(fullWaypointsList[9, 2]);*/

    }
}


    /*// Assigns the transform of the first child of the Game Object this script is attached to.
gate = this.gameObject.transform.GetChild(10);

print(gate.name);

//Assigns the first child of the first child of the Game Object this script is attached to.
waypoint = this.gameObject.transform.GetChild(10).GetChild(0).gameObject;

print(waypoint.name);
print(waypoint.transform.position);*/
