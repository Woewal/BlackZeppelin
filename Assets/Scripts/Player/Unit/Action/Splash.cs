using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Obstacles;

namespace Game.Actions
{
    [CreateAssetMenu(fileName = "Splash", menuName = "Unit/Splash", order = 1)]
    class Splash : Action
    {
        public List<Action.Direction> directions;

        public override IEnumerator InvokeAction()
        {
            Unit unit = GameController.instance.actionController.currentUnit;
            Tile[,] tiles = GameController.instance.boardController.tiles;

            foreach (Action.Direction direction in directions)
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

                tile.ChangeColor(unit.color);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}

