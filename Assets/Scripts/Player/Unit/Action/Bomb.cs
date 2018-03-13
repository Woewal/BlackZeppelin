using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Obstacles;

namespace Game.Actions
{
    [CreateAssetMenu(fileName = "Bomb", menuName = "Unit/Bomb", order = 1)]
    public class Bomb : Action
    {
        [SerializeField] int steps;
        [SerializeField] Obstacles.Bomb bombPrefab;

        public override List<Tile> GetDirections()
        {
            Unit unit = GameController.instance.roundController.currentUnit;

            var list = GameController.instance.boardController.pathFinder.GetAllPaths(steps, unit);

            return list;
        }

        void Place()
        {
            Obstacles.Bomb bomb = Instantiate(bombPrefab);
            Tile destinationTile = GameController.instance.actionController.GetComponent<BoardSelection>().selectedTile;

            bomb.transform.position = new Vector3(destinationTile.transform.position.x, destinationTile.transform.position.y, destinationTile.transform.position.z);
            destinationTile.occupyingObstacle = bomb;
            bomb.occupiedTile = destinationTile;
        }

        public override IEnumerator InvokeAction()
        {
            Place();
            yield return new WaitForSeconds(0.5f);
        }
    }
}

