using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utilities
{
    public static class TileHelper
    {

        private static Tile[,] Tiles
        {
            get
            {
                if (tiles == null)
                {
                    tiles = BoardController.instance.tiles;
                }
                return tiles;
            }
        }
        private static Tile[,] tiles;

        public static List<Tile> GetRelativeTiles(Tile beginTile, List<Coordinate> relativeCoordinates)
        {
            List<Tile> relativeTiles = new List<Tile>();

            foreach (Coordinate relativeCoordinate in relativeCoordinates)
            {
                if (
                    beginTile.x + relativeCoordinate.x < 0 ||
                    beginTile.y + relativeCoordinate.y < 0 ||
                    beginTile.x + relativeCoordinate.x >= Tiles.GetLength(0) ||
                    beginTile.y + relativeCoordinate.y >= Tiles.GetLength(1)
                )
                {
                    continue;
                }

                Tile tile = Tiles[beginTile.x + relativeCoordinate.x, beginTile.y + relativeCoordinate.y];

                relativeTiles.Add(tile);
            }

            return relativeTiles;
        }
    }

}
