using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTest2Points : ObstacleDefiner
{

	
	public override Vector2[][] GetObstaclePoints()
	{
		Vector2[] obstacles1 = new Vector2[] { new Vector2(2f, 2f), new Vector2(1f, 1.5f), new Vector2(1f, 0.1f) };
        Vector2[] obstacles2 = new Vector2[] { new Vector2(-2f, -2f), new Vector2(-1f, -1.5f), new Vector2(-1f, -0.1f) };
        Vector2[] obstacles3 = new Vector2[] { new Vector2(0.5f, -0.65f), new Vector2(-0.65f, -0.65f), new Vector2(-0.65f, -0.25f), new Vector2(0.5f, -0.25f) };
        Vector2[] obstacles4 = new Vector2[] { new Vector2(0.5f, 0.65f), new Vector2(-0.65f, 0.65f), new Vector2(-0.65f, 0.25f), new Vector2(0.5f, 0.25f) };

        Vector2[][] arrayOfObstaclePoints = new Vector2[][] { obstacles1, obstacles2, obstacles3, obstacles4 };

		width = 2;
		height = 1;

		return arrayOfObstaclePoints;
	}
}
