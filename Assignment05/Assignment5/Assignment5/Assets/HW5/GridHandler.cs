using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

//Assignment 1 Part A Script
public class GridHandler : NodeHandler
{

    
    //Size of grid points
    private float gridSize = 0.2f;

    //Holds all of the nodes
    private Dictionary<string, GraphNode> nodeDictionary;
    public override void CreateNodes()
    {
        nodeDictionary = new Dictionary<string, GraphNode>();

        for (float x = ObstacleHandler.Instance.Width * -1+gridSize; x <= ObstacleHandler.Instance.Width; x += gridSize)
        {
            for (float y = ObstacleHandler.Instance.Height * -1+gridSize*2; y <= ObstacleHandler.Instance.Height-gridSize; y += gridSize)
            {
                Vector3 loc = new Vector3(x, y);
                if (!ObstacleHandler.Instance.PointInObstacles(new Vector2(x, y)))
                {
                    nodeDictionary.Add(loc.ToString(), new GraphNode(loc));
                }
            }
        }

        //Create Neighbors
        foreach (KeyValuePair<string, GraphNode> kvp in nodeDictionary)
        {
            //Left
            if (nodeDictionary.ContainsKey((kvp.Value.Location + (Vector3.left * gridSize)).ToString()))
            {
                kvp.Value.AddNeighbor(nodeDictionary[(kvp.Value.Location + (Vector3.left * gridSize)).ToString()]);
            }
            //Right
            if (nodeDictionary.ContainsKey((kvp.Value.Location + (Vector3.right * gridSize)).ToString()))
            {
                kvp.Value.AddNeighbor(nodeDictionary[(kvp.Value.Location + (Vector3.right * gridSize)).ToString()]);
            }
            //Up
            if (nodeDictionary.ContainsKey((kvp.Value.Location + (Vector3.up * gridSize)).ToString()))
            {
                kvp.Value.AddNeighbor(nodeDictionary[(kvp.Value.Location + (Vector3.up * gridSize)).ToString()]);
            }
            //Down
            if (nodeDictionary.ContainsKey((kvp.Value.Location + (Vector3.down * gridSize)).ToString()))
            {
                kvp.Value.AddNeighbor(nodeDictionary[(kvp.Value.Location + (Vector3.down * gridSize)).ToString()]);
            }
        }
    }

    public override void VisualizeNodes()
    {
        //Visualize grid points
        foreach (KeyValuePair<string, GraphNode> kvp in nodeDictionary)
        {
            //Draw left line
            Debug.DrawLine(kvp.Value.Location + Vector3.left * gridSize / 2f + Vector3.up * gridSize / 2f, kvp.Value.Location + Vector3.left * gridSize / 2f + Vector3.down * gridSize / 2f, Color.white);
            //Draw right line
            Debug.DrawLine(kvp.Value.Location + Vector3.right * gridSize / 2f + Vector3.up * gridSize / 2f, kvp.Value.Location + Vector3.right * gridSize / 2f + Vector3.down * gridSize / 2f, Color.white);
            //Draw top line
            Debug.DrawLine(kvp.Value.Location + Vector3.up * gridSize / 2f + Vector3.left * gridSize / 2f, kvp.Value.Location + Vector3.up * gridSize / 2f + Vector3.right * gridSize / 2f, Color.white);
            //Draw bottom line
            Debug.DrawLine(kvp.Value.Location + Vector3.down * gridSize / 2f + Vector3.left * gridSize / 2f, kvp.Value.Location + Vector3.down * gridSize / 2f + Vector3.right * gridSize / 2f, Color.white);
        }
    }

    //Find closest node (used for pathing)
    public override GraphNode ClosestNode(Vector3 position)
    {
        float minDist = 1000;
        GraphNode closest = null;
        foreach (KeyValuePair<string, GraphNode> kvp in nodeDictionary)
        {
            float dist = (kvp.Value.Location - position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                closest = kvp.Value;
            }
        }
        return closest;
    }
}
