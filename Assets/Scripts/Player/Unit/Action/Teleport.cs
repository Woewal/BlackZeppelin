using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Actions
{
    [CreateAssetMenu(fileName = "Teleport", menuName = "Unit/Teleport", order = 1)]
    public class Teleport : Action
    {
        protected override void SetupAction()
        {
            if (actionController == null)
                actionController = GameController.instance.actionController;

            //int directions = GetDirections();

            //Debug.Log(directions);

            //if (directions == 0)
            //{
                //GameController.instance.actionController.NextAction();
            //}
        }

        /*int GetDirections()
        {
            Tile[,] tiles = GameController.instance.boardController.tiles;
            var unit = actionController.currentUnit;

            List<Tile> closedTiles = new List<Tile>();
            List<Tile> openTiles = new List<Tile>
            {
                unit.occupiedTile
            };

            while (openTiles.Count != 0)
            {
                //Get neighbours
                foreach(Tile tile in GetNeighbours(openTiles[0], unit.color))
                {
                    if (!closedTiles.Contains(tile) || tile.CheckWalkAble(unit))
                        openTiles.Add(tile);
                }

                closedTiles.Add(openTiles[0]);
                openTiles.RemoveAt(0);
            }

            closedTiles.Remove(unit.occupiedTile);

            return closedTiles.Count;
        }*/

        List<Tile> GetNeighbours(Tile tile, Obstacle.Color color)
        {
            Vector2[] directions = new Vector2[] { new Vector2(0, -1), new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, 1) };

            Tile[,] tiles = GameController.instance.boardController.tiles;

            List<Tile> neighbours = new List<Tile>();

            for (int i = 0; i < directions.Length; i++)
            {

                int x = (int)directions[i].x;
                int y = (int)directions[i].y;

                if (
                    tile.x + x < 0 ||
                    tile.y + y < 0 ||
                    tile.x + x >= tiles.GetLength(0) ||
                    tile.y + y >= tiles.GetLength(1)
                    )
                {
                    continue;
                }

                Tile neighbourTile = tiles[tile.x + x, tile.y + y];
                
                if(color != neighbourTile.color)
                {
                    continue;
                }

                neighbours.Add(neighbourTile);
            }

            return neighbours;
        }

        public override IEnumerator InvokeAction()
        {
            yield return actionController.StartCoroutine(actionController.currentUnit.MoveToTile(actionController.GetComponent<BoardSelection>().selectedTile));

        }
    }
}

