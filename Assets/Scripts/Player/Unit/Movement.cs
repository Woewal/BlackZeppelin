using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Unit
{
    [CreateAssetMenu(fileName = "Movement", menuName = "Unit/Movement", order = 1)]
    public class Movement : Action
    {
        public List<Direction> directions;

        [System.Serializable]
        public class Direction
        {
            public int x;
            public int y;
        }

        protected override void SetupAction()
        {
            if (actionController == null)
                actionController = GameController.instance.actionController;
            GetDirections();
        }

        List<Tile> GetDirections()
        {
            Tile[,] boardTiles = GameController.instance.boardController.tiles;

            var unit = actionController.currentUnit;

            List<Tile> tiles = new List<Tile>();

            foreach (Direction direction in directions)
            {

                if (
                    unit.occupiedTile.x + direction.x < 0 ||
                    unit.occupiedTile.y + direction.y < 0 ||
                    unit.occupiedTile.x + direction.x >= boardTiles.GetLength(0) ||
                    unit.occupiedTile.y + direction.y >= boardTiles.GetLength(1)
                )
                {
                    continue;
                }

                Tile tile = boardTiles[unit.occupiedTile.x + direction.x, unit.occupiedTile.y + direction.y];

                if (!tile.CheckWalkAble(unit))
                {
                    continue;
                }

                tile.HighLight();
                tiles.Add(tile);
            }

            return tiles;
        }

        public override bool CanExecute()
        {
            /*if (actionController.traversableTiles.Contains(actionController.selectedTile))
            {
                return true;
            }
            return false;*/
            return true;
        }

        public override IEnumerator InvokeAction()
        {
            yield return actionController.StartCoroutine(actionController.currentUnit.MoveToTile(actionController.GetComponent<BoardSelection>().selectedTile));
            yield return new WaitForSeconds(0.5f);
        }
    }
}

