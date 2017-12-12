using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public Tile occupiedTile;

    [SerializeField]
    float movementDuration = 0.5f;

    public IEnumerator Move(List<Tile> tileMovements)
    {
        int i = 0;
        while (i < tileMovements.Count)
        {
            i++;
            yield return StartCoroutine(MoveTile(occupiedTile, tileMovements[i - 1]));
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
        occupiedTile = tileB;
    }
}
