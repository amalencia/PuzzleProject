using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public abstract class BaseState
    {
        protected EnemyController _controller;

        public BaseState(EnemyController controller)
        {
            _controller = controller;
        }

        public abstract void EnterState();
        public abstract void RunState();
        public abstract void ExitState();
    }
}

