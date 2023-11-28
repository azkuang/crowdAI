using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCAbilities : MonoBehaviour
{
    // A method to pick a random destination for the agents to walk to
    public abstract void PickRandomDestination();

    // A method to start walking to the chosen random destination
    public abstract void WalktoRandomDestination();

    // Boolean value to check if the random destination has been reached
    public abstract bool DidReachDestination();
}
