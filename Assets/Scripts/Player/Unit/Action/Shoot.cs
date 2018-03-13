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

        public List<List<Tile>> directions;

        public override List<Tile> GetDirections()
        {
            var paths = Coordinate.GetPatternDirections(relativeCoordinates);
            List<Coordinate> coordinates = new List<Coordinate>();

            foreach(var path in paths)
            {
                foreach(var coordinate in path)
                {
                    coordinates.Add(coordinate);
                }
            }

            return TileHelper.GetRelativeTiles(GameController.instance.roundController.currentUnit.occupiedTile ,coordinates);
        }

        public override IEnumerator InvokeAction()
        {


            yield return new WaitForSeconds(0.5f);
        }
    }
}

