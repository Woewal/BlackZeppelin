using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Actions;

namespace Game.Obstacles
{
    public class Unit : Obstacle
    {
        public List<Action> actions = new List<Action>();

        float movementDuration = 0.1f;

        IEnumerator Move(List<Tile> tileMovements)
        {
            occupiedTile.occupyingObstacle = null;

            Tile currentTile;

            int i = 0;

            while (i < tileMovements.Count)
            {
                if (i == 0)
                {
                    currentTile = occupiedTile;
                }
                else
                {
                    currentTile = tileMovements[i - 1];
                }

                Tile targetTile = tileMovements[i];

                i++;

                yield return StartCoroutine(MoveTile(currentTile, targetTile));
            }

            occupiedTile = tileMovements[tileMovements.Count - 1];
            occupiedTile.occupyingObstacle = this;

        }

        IEnumerator MoveTile(Tile tileA, Tile tileB)
        {
            float currentTime = 0;

            while (currentTime < movementDuration)
            {
                transform.position = Vector3.Lerp(tileA.transform.position, tileB.transform.position, currentTime / movementDuration);
                currentTime += Time.deltaTime;
                yield return null;
            }

            tileB.ChangeColor(color);
        }

        public IEnumerator MoveToTile(Tile destinationTile)
        {
            List<Tile> tilesToDestination = BoardController.instance.pathFinder.GetPath(new Vector2(occupiedTile.x, occupiedTile.y), new Vector2(destinationTile.x, destinationTile.y), this);

            if (tilesToDestination != null)
            {
                yield return StartCoroutine(Move(tilesToDestination));
            }
            else
            {
                Debug.LogError("No path available");
            }
        }

        public override void DestroyObstacle()
        {
            RemoveReferences();
            Destroy(this.gameObject);
        }
    }
}

