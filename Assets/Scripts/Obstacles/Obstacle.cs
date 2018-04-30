using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour {
    public enum Color { Blank, Red, Blue, Green, Yellow };

    public Color color = Color.Red;

    [HideInInspector] public Tile occupiedTile;

    public virtual void DestroyObstacle()
    {
        Destroy(gameObject.gameObject);
    }

    protected void RemoveReferences()
    {
        this.occupiedTile.occupyingObstacle = null;
    }
}
