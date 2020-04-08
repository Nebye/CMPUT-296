using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTest1Points : ObstacleDefiner
{


	public override Vector2[][] GetObstaclePoints()
	{
		width = 2;
		height = 2.5f;
		Vector2[] topObstacle = new Vector2[] { new Vector2(-1*width, height-0.2f), new Vector2(width, height - 0.2f), new Vector2(width,height-0.25f), new Vector2(-1*width, height - 0.25f) };
		Vector2[] rightObstacle = new Vector2[] { new Vector2(width, height - 0.25f), new Vector2(width+0.05f, height - 0.25f), new Vector2(width + 0.05f, -1*height + 0.25f), new Vector2(width, -1 * height + 0.25f) };
		Vector2[] leftObstacle = new Vector2[] { new Vector2(-1*width, height - 0.25f), new Vector2(-1 * width - 0.05f, height - 0.25f), new Vector2(-1 * width - 0.05f, -1 * height + 0.25f), new Vector2(-1 * width, -1 * height + 0.25f) };
		Vector2[] downObstacle = new Vector2[] { new Vector2(-1 * width, -1*height + 0.2f), new Vector2(width, -1 * height + 0.2f), new Vector2(width, -1 * height + 0.25f), new Vector2(-1 * width, -1 * height + 0.25f) };


		Vector2[] ghostHouse = new Vector2[] { new Vector2(-0.3f, 0.05f), new Vector2(-0.3f, 0.4f), new Vector2(0.3f, 0.4f), new Vector2(0.3f, 0.05f) };

		List<Vector2[]> arrayOfObstaclePoints = new List<Vector2[]>();
		arrayOfObstaclePoints.Add(topObstacle);
		arrayOfObstaclePoints.Add(rightObstacle);
		arrayOfObstaclePoints.Add(leftObstacle);
		arrayOfObstaclePoints.Add(downObstacle);

		arrayOfObstaclePoints.Add(ghostHouse);

        for (float i = -1; i<= 1; i++)
        {
            if (i != 0)
            {
                //Top two
				arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(i*width-0.3f*i, height-0.5f), new Vector2(i * width - 0.8f * i, height - 0.5f), new Vector2(i * width - 0.8f * i, height - 0.9f), new Vector2(i * width - 0.3f * i, height - 0.9f) });
				arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(i * width - 1.1f * i, height - 0.5f), new Vector2(i * width - 1.85f * i, height - 0.5f), new Vector2(i * width - 1.85f * i, height - 0.9f), new Vector2(i * width - 1.1f * i, height - 0.9f) });

                //Little one
				arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(i * width - 0.3f * i, height - 1.15f), new Vector2(i * width - 0.8f * i, height - 1.15f), new Vector2(i * width - 0.8f * i, height - 1.5f), new Vector2(i * width - 0.3f * i, height - 1.5f) });

                //Top Tetris piece
				arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(i * width - 1.2f * i, height - 1.15f), new Vector2(i * width - 1.4f * i, height - 1.15f), new Vector2(i * width - 1.4f * i, height - 1.5f), new Vector2(i * width - 1.6f * i, height - 1.5f), new Vector2(i * width - 1.6f * i, height - 1.9f), new Vector2(i * width - 1.4f * i, height - 1.9f), new Vector2(i * width - 1.4f * i, 0.3f), new Vector2(i * width - 1.2f * i, 0.3f) });

                //Top edge obstacle
				arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(i * width, height - 1.8f), new Vector2(i * width - 0.8f * i, height - 1.8f), new Vector2(i * width - 0.8f * i, 0.3f), new Vector2(i * width, 0.3f) });

				//Bottom edge obstacle
				arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(i * width, 0), new Vector2(i * width - 0.8f * i, 0), new Vector2(i * width - 0.8f * i, -0.4f), new Vector2(i * width, -0.4f) });

				//Vertical tictac
				arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(i * width - 1.2f * i, 0), new Vector2(i * width - 1.4f * i, 0), new Vector2(i * width - 1.4f * i, -0.4f), new Vector2(i * width - 1.2f * i, -0.4f) });

				//Horizontal tictac
				arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(i * width - 1.2f * i, -0.7f), new Vector2(0.4f * i, -0.7f), new Vector2(0.4f * i, -1f), new Vector2(i * width - 1.2f * i, -1f) });

				//L shape
				arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(i * width - 0.3f * i, -0.7f), new Vector2(i * width - 0.8f * i, -0.7f), new Vector2(i * width - 0.8f * i, -1f), new Vector2(i * width - 0.8f * i, -1.4f), new Vector2(i * width - 0.6f * i, -1.4f), new Vector2(i * width - 0.6f * i, -1f), new Vector2(i * width - 0.3f * i, -1f) });

				//Bottom edge obstacle
				arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(i * width, -1.25f), new Vector2(i * width - 0.2f * i, -1.25f), new Vector2(i * width - 0.2f * i, -1.35f), new Vector2(i * width, -1.35f) });

				//Bottom obstacle
				arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(i * width-0.4f*i, -1.7f), new Vector2(i * width - 0.4f * i, -1.9f), new Vector2(i * 0.4f, -1.9f), new Vector2(i * 0.4f, -1.7f), new Vector2(i * 0.6f, -1.7f), new Vector2(i * 0.6f, -1.3f) , new Vector2(i * width - 1.2f * i, -1.3f), new Vector2(i * width - 1.2f * i, -1.7f) });

			}
		}

        //Center Top Testris Piece
		arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(0.3f, height - 1.15f), new Vector2(-0.3f, height - 1.15f), new Vector2(-0.3f, height-1.3f), new Vector2(-0.1f, height - 1.3f), new Vector2(-0.1f, height - 1.9f), new Vector2(0.1f, height - 1.9f), new Vector2(0.1f, height - 1.3f), new Vector2(0.3f, height - 1.3f) });

		//Center Middle Testris Piece
		arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(0.3f, -0.25f), new Vector2(-0.3f, -0.25f), new Vector2(-0.3f, -0.4f), new Vector2(-0.1f, -0.4f), new Vector2(-0.1f, -1f), new Vector2(0.1f, -1f), new Vector2(0.1f, -0.4f), new Vector2(0.3f, -0.4f) });

		//Center Bottom Testris Piece
		arrayOfObstaclePoints.Add(new Vector2[] { new Vector2(0.3f, -1.25f), new Vector2(-0.3f, -1.25f), new Vector2(-0.3f, -1.4f), new Vector2(-0.1f, -1.4f), new Vector2(-0.1f, -1.9f), new Vector2(0.1f, -1.9f), new Vector2(0.1f, -1.4f), new Vector2(0.3f, -1.4f) });

		height = 2.25F;

		return arrayOfObstaclePoints.ToArray();
	}
}
