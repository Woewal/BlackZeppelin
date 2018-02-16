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

        void GetDirections()
        {
            Tile[,] tiles = GameController.instance.boardController.tiles;

            var unit = actionController.currentUnit;

            foreach (Direction direction in directions)
            {

                if (
                    unit.occupiedTile.x + direction.x < 0 ||
                    unit.occupiedTile.y + direction.y < 0 ||
                    unit.occupiedTile.x + direction.x >= tiles.GetLength(0) ||
                    unit.occupiedTile.y + direction.y >= tiles.GetLength(1)
                )
                {
                    continue;
                }

                Tile tile = tiles[unit.occupiedTile.x + direction.x, unit.occupiedTile.y + direction.y];

                if (!tile.CheckWalkAble(unit))
                {
                    continue;
                }

                tile.HighlightTraversable();
                actionController.traversableTiles.Add(tile);
            }
        }

        public override bool CanExecute()
        {
            if (actionController.traversableTiles.Contains(actionController.selectedTile))
            {
                return true;
            }
            return false;
        }

        public override IEnumerator InvokeAction()
        {
            yield return actionController.StartCoroutine(actionController.currentUnit.MoveToTile(actionController.selectedTile));
            yield return new WaitForSeconds(0.5f);
        }
    }
}

