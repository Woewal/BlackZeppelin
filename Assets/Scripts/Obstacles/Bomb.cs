using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Actions;
using Game.Utilities;
using System;

namespace Game.Obstacles
{
    public class Bomb : Obstacle
    {
        [SerializeField] List<Coordinate> relativeCoordinates;

        public override void DestroyObstacle()
        {
            Detonate();
        }

        private void Detonate()
        {
            foreach(var tile in TileHelper.GetRelativeTiles(occupiedTile, relativeCoordinates))
            {
                tile.ChangeColor(Color.Red);
            }

            RemoveReferences();
            Destroy(gameObject);
        }
    }
}

