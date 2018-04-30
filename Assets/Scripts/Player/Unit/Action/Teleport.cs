using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Obstacles;

namespace Game.Actions
{
    [CreateAssetMenu(fileName = "Teleport", menuName = "Unit/Teleport", order = 1)]
    public class Teleport : Action
    {
        protected override void SetupAction()
        {
            if (actionController == null)
                actionController = GameController.instance.actionController;
        }

        public override List<Tile> GetDirections()
        {
            Unit unit = GameController.instance.roundController.currentUnit;

            return GameController.instance.boardController.pathFinder.GetAllPaths(0, unit);
        }

        public override IEnumerator InvokeAction()
        {
            yield return actionController.StartCoroutine(actionController.currentUnit.MoveToTile(actionController.GetComponent<BoardSelection>().selectedTile));
            yield return new WaitForSeconds(0.5f);
        }
    }
}

