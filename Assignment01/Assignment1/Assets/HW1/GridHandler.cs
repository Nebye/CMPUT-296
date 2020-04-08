using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //ASSIGNMENT 1 EDIT BELOW THIS LINE

        

        for (float x = ObstacleHandler.Instance.Width * -0.5f; x <= ObstacleHandler.Instance.Width - (gridSize * 4.6); x += gridSize) //map intersection, pretty much completed
        {
            for (float y = ObstacleHandler.Instance.Height * -0.8f; y <= ObstacleHandler.Instance.Height - (gridSize); y += gridSize)
            {
                Vector3 loc = new Vector3(x, y); // x, y, z (z is by default zero when not inputted)


                Vector3 a01 = new Vector3(x+gridSize, y);
                Vector3 b01 = new Vector3(x + gridSize - 0.1f, y);


                Vector3 a02 = new Vector3(x, y+gridSize);
                Vector3 b02 = new Vector3(x - 0.1f, y + gridSize);


                Vector3 a03 = new Vector3(x+gridSize, y+gridSize);
                Vector3 b03 = new Vector3(x - 0.1f + gridSize, y + gridSize);

                Vector3 aprime = new Vector3(x + gridSize - 0.1f, y + gridSize);

                Vector3 aprime2 = new Vector3(x + gridSize - 0.15f, y + gridSize - 0.14f);

                //only add tile if not intersecting with obstacle
                //
                if (ObstacleHandler.Instance.AnyIntersect(loc, a01) != true && ObstacleHandler.Instance.AnyIntersect(loc, a02) != true && ObstacleHandler.Instance.AnyIntersect(a01, a03) != true && ObstacleHandler.Instance.AnyIntersect(a02, a03) != true
                    && ObstacleHandler.Instance.AnyIntersect(aprime, b01) != true && ObstacleHandler.Instance.AnyIntersect(aprime, b02) != true && ObstacleHandler.Instance.AnyIntersect(b01, b03) != true && ObstacleHandler.Instance.AnyIntersect(b02, b03) != true) 
                    {
                    //
                    //if (ObstacleHandler.Instance.PointInObstacles(aprime) == false || ObstacleHandler.Instance.PointInObstacles(a01) == false || ObstacleHandler.Instance.PointInObstacles(a02) == false || ObstacleHandler.Instance.PointInObstacles(a03) == false)
                    if (ObstacleHandler.Instance.PointInObstacles(loc) == false || ObstacleHandler.Instance.PointInObstacles(a01) == false || ObstacleHandler.Instance.PointInObstacles(a02) == false || ObstacleHandler.Instance.PointInObstacles(a03) == false
                        || ObstacleHandler.Instance.PointInObstacles(aprime) == false || ObstacleHandler.Instance.PointInObstacles(a01) == false || ObstacleHandler.Instance.PointInObstacles(a02) == false || ObstacleHandler.Instance.PointInObstacles(a03) == false)
                        {
                            nodeDictionary.Add(aprime2.ToString(), new GraphNode(aprime2));
                        }
                    else
                        {
                            //nodeDictionary.Add(loc.ToString(), new GraphNode(loc));
                        }
                    }
                
            }
        }
        
        
        //ASSIGNMENT 1 EDIT ABOVE THIS LINE














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
