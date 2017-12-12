using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour {
    Board board;

    [SerializeField]
    int boardWidth;
    [SerializeField]
    int boardHeight;

    [SerializeField]
    GameObject tilePrefabLight;
    [SerializeField]
    GameObject tilePrefabDark;
    [SerializeField]
    GameObject tilePrefabUnwalkable;

    private void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        board = gameObject.GetComponent<Board>();
        
        board.tiles = new Tile[boardWidth, boardHeight];

        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                Tile tile;
                if (x == 4 && (y > 2 && y < boardHeight - 2))
                {
                    tile = Instantiate(tilePrefabUnwalkable, transform).GetComponent<Tile>();
                }
                else if (x % 2 == 0 && y % 2 != 0 || x % 2 == 1 && y % 2 != 1)
                {
                    tile = Instantiate(tilePrefabLight, transform).GetComponent<Tile>();
                }
                else
                {
                    tile = Instantiate(tilePrefabDark, transform).GetComponent<Tile>();
                }
                tile.transform.Translate(new Vector3((x - boardWidth) * Tile.size, 0, (y - boardHeight) * Tile.size));
                tile.x = x; tile.y = y;

                board.tiles[x, y] = tile;
            }
        }

        board.Initiate();

        Destroy(this);
    }
}
