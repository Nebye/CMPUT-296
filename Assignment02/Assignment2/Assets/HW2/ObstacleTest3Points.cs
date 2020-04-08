using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTest3Points : ObstacleDefiner
{

	
	public override Vector2[][] GetObstaclePoints()
	{
		Vector2[] obstacles1 = new Vector2[] { new Vector2(2f, 1f), new Vector2(0.5f, 1f), new Vector2(1.5f, 0.9f), new Vector2(0.75f, 0.4f), new Vector2(1.5f, 0.7f), new Vector2(1.75f, 0f) };
        Vector2[] obstacles2 = new Vector2[] { new Vector2(-2f, -1f), new Vector2(-0.5f, -1f), new Vector2(-1.5f, -0.9f), new Vector2(-0.75f, -0.4f), new Vector2(-1.5f, -0.7f), new Vector2(-1.5f, 0.0f), new Vector2(-1.75f, -0.2f), new Vector2(-2f, 0.5f) };

        Vector2[][] arrayOfObstaclePoints = new Vector2[][] { obstacles1, obstacles2 };

		width = 2;
		height = 1;

		return arrayOfObstaclePoints;
	}
}
