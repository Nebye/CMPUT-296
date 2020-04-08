using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTest1Points : ObstacleDefiner
{

	
	public override Vector2[][] GetObstaclePoints()
	{
		Vector2[] obstacles1 = new Vector2[] { new Vector2(-1f, -0.8f), new Vector2(1f, -0.8f), new Vector2(1f, 2.5f), new Vector2(0.5f, 2.5f), new Vector2(0.5f, -0.3f), new Vector2(-1f, -0.3f) };

		Vector2[][] arrayOfObstaclePoints = new Vector2[][] { obstacles1};

		width = 2;
		height = 1;

		return arrayOfObstaclePoints;
	}
}
