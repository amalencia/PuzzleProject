using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public abstract class AiState
    {
        protected AIController controller;
        public AiState(AIController contr)
        {
            controller = contr;
        }

        public abstract void OnStateEnter();
        public abstract void OnStateRun();
        public abstract void OnStateExit();
    }
}