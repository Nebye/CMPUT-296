using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentConstants
{
    public static float SPEED = 1f;
    public static float GHOST_SPEED = 0.9f;
    public static float THRESHOLD = 0.001f;
    public static float EPSILON = 0.00001f;
    public static Vector3 GHOST_START_POS = new Vector3(0,0.5f,0);
    public static Vector3 PACMAN_START_POS = new Vector3(0, -1.1f, 0);
}
