using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Obstacles;
using Game.Utilities;

namespace Game.Actions
{
    [CreateAssetMenu(fileName = "Shoot", menuName = "Unit/Shoot", order = 1)]
    class Shoot : Action
    {
        public List<Coordinate> relativeCoordinates;

        public override List<List<Tile>> GetPaths()
        {
            Unit unit = GameController.instance.actionController.currentUnit;

            var coordinatePaths = Coordinate.GetPatternDirections(relativeCoordinates);
            var tilePaths = new List<List<Tile>>();

            foreach(List<Coordinate> path in coordinatePaths)
            {
                var tilePath = new List<Tile>();

                foreach(var coordinate in path)
                {
                    Tile tile = Tile.GetTile(coordinate.x + unit.occupiedTile.x, coordinate.y + unit.occupiedTile.y);

                    tilePath.Add(tile);
                }
                
                if(tilePath.Contains(null))
                {
                    tilePaths.Add(null);
                }
                else
                {
                    tilePaths.Add(tilePath);
                }
            }

            return tilePaths;
        }

        public override IEnumerator InvokeAction()
        {
            ShootPath();

            yield return new WaitForSeconds(0.5f);
        }

        void ShootPath()
        {
            var path = GameController.instance.actionController.GetComponent<BoardSelection>().selectedPath;

            var unit = GameController.instance.actionController.currentUnit;

            foreach (var tile in path)
            {
                tile.ChangeColor(unit.color);
            }
        }
    }
}

