using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Actions
{
    public abstract class Action : ScriptableObject
    {
        protected ActionController actionController;
        
        public enum InputType { None, Cursor, Path}

        public InputType inputType = InputType.None;

        public void StartAction()
        {
            if(inputType != InputType.None)
            {
                SetupAction();
            }
            else
            {
                SetupAction();
                GameController.instance.actionController.StartCoroutine(GameController.instance.actionController.ExecuteAction(InvokeAction()));
            }
        }

        public virtual List<Tile> GetDirections()
        {
            return null;
        }

        public virtual List<List<Tile>> GetPaths()
        {
            return null;
        }

        protected virtual void SetupAction()
        {
        }

        public abstract IEnumerator InvokeAction();

        [System.Serializable]
        public class Direction
        {
            public int x;
            public int y;
        }
    }
}

