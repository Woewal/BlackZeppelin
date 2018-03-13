using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Obstacles;

public class PathFinder
{
    Tile[,] tiles;
    List<Tile> openTiles;
    List<Tile> closedTiles;

    public PathFinder(Tile[,] boardTiles)
    {
        tiles = boardTiles;
    }

    public List<Tile> GetPath(Vector2 beginPos, Vector2 endPos, Unit unit)
    {

        openTiles = new List<Tile>();
        closedTiles = new List<Tile>();

        openTiles.Add(tiles[(int)beginPos.x, (int)beginPos.y]);

        Tile startTile = tiles[(int)beginPos.x, (int)beginPos.y];
        Tile endTile = tiles[(int)endPos.x, (int)endPos.y];

        while (openTiles.Count > 0)
        {
            Tile currentTile = openTiles[0];
            SetCurrentTile(currentTile);

            openTiles.Remove(currentTile);
            closedTiles.Add(currentTile);

            if (currentTile == endTile)
            {
                return RetracePath(tiles[(int)beginPos.x, (int)beginPos.y], endTile);
            }

            List<Tile> neighbours = GetNeighbours(currentTile);
            foreach (Tile neighbour in neighbours)
            {
                if (closedTiles.Contains(neighbour) || !neighbour.CheckWalkAble(unit))
                {
                    continue;
                }

                int newDistanceToNeighbour = currentTile.gCost + GetDistance(currentTile, neighbour);
                if (newDistanceToNeighbour < currentTile.gCost || !openTiles.Contains(neighbour))
                {
                    neighbour.gCost = newDistanceToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, endTile);
                    neighbour.parent = currentTile;

                    if (!openTiles.Contains(neighbour))
                    {
                        openTiles.Add(neighbour);
                    }
                }
            }
        }

        Debug.LogError("No path");
        return null;
    }

    public List<Tile> GetAllPaths(int steps, Unit unit)
    {
        List<Tile> openNodes = new List<Tile>();
        List<Tile> closedNodes = new List<Tile>();

        openNodes.Add(unit.occupiedTile);

        for (int x = 0; x < steps + 1; x++)
        {
            List<Tile> nextNodes = new List<Tile>();

            foreach(var tile in openNodes)
            {
                foreach(var neighbour in GetNeighbours(tile))
                {
                    if(!closedNodes.Contains(neighbour))
                    {
                        if(neighbour.CheckWalkAble(unit))
                        {
                            nextNodes.Add(neighbour);
                        }
                    }
                }
                closedNodes.Add(tile);
            }
            openNodes.Clear();
            foreach(var node in nextNodes)
            {
                openNodes.Add(node);
            }
        }

        closedNodes.Remove(unit.occupiedTile);
        return closedNodes;
    }

    List<Tile> RetracePath(Tile startTile, Tile endTile)
    {
        List<Tile> path = new List<Tile>();
        Tile currentTile = endTile;

        while (currentTile != startTile)
        {
            path.Add(currentTile);
            currentTile = currentTile.parent;
        }

        path.Reverse();

        return path;
    }

    void SetCurrentTile(Tile currentTile)
    {
        for(int i = 0; i < openTiles.Count; i++)
        {
            if (openTiles[i].FStop < currentTile.FStop || openTiles[i].FStop == currentTile.FStop && openTiles[i].hCost < currentTile.hCost)
            {
                currentTile = openTiles[i];
            }
        }
    }

    List<Tile> GetNeighbours(Tile tile)
    {
        List<Tile> neighboors = new List<Tile>();

        if(tile.x - 1 >= 0)
        {
            neighboors.Add(tiles[tile.x - 1, tile.y]);
        }
        if(tile.y - 1 >= 0)
        {
            neighboors.Add(tiles[tile.x, tile.y -1]);
        }
        if(tile.x + 1 < tiles.GetLength(0))
        {
            neighboors.Add(tiles[tile.x + 1, tile.y]);
        }
        if (tile.y + 1 < tiles.GetLength(1))
        {
            neighboors.Add(tiles[tile.x, tile.y + 1]);
        }

        return neighboors;
    }

    int GetDistance(Tile tileA, Tile tileB)
    {
        int x = Mathf.Abs(tileA.x - tileB.x);
        int y = Mathf.Abs(tileA.y - tileB.y);

        return x + y;
    }
}
