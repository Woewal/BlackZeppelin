using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public Tile occupiedTile;

    public enum Color { Blank, Red, Blue, Green };

    public Color color = Color.Red;

    [SerializeField]
    float movementDuration = 0.5f;

    public IEnumerator Move(List<Tile> tileMovements)
    {
        Tile currentTile;

        int i = 0;

        while (i < tileMovements.Count)
        {
            if (i == 0)
            {
                currentTile = occupiedTile;
            }
            else
            {
                currentTile = tileMovements[i - 1];
            }

            Tile targetTile = tileMovements[i];

            i++;

            yield return StartCoroutine(MoveTile(currentTile, targetTile));            
        }

        occupiedTile = tileMovements[tileMovements.Count - 1];
            
    }

    public IEnumerator MoveTile(Tile tileA, Tile tileB)
    {
        float currentTime = 0;

        while (currentTime < movementDuration)
        {
            transform.position = Vector3.Lerp(tileA.transform.position, tileB.transform.position, currentTime / movementDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        tileB.ChangeColor(color);
    }
}
