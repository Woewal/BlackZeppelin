using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Unit
{
    public abstract class Action : ScriptableObject
    {
        protected ActionController actionController;

        public bool needsInput;

        private void Awake()
        {
            actionController = GameController.instance.actionController;
        }

        public void StartAction()
        {
            if(needsInput)
            {
                SetupAction();
            }
            else
            {
                SetupAction();
                GameController.instance.actionController.StartCoroutine(GameController.instance.actionController.ExecuteAction(InvokeAction()));
            }
        }

        protected virtual void SetupAction()
        {
        }

        public virtual bool CanExecute()
        {
            return false;
        }

        public abstract IEnumerator InvokeAction();
    }
}

