using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class ChaseState : BaseState
    {
        private NavMeshAgent _agent;
        private float _speed;
        private float _stopDistance;
        private AudioSource _audioSource;

        public ChaseState(EnemyController controller) : base(controller)
        {
            _controller.GetChaseParams(out _agent, out _speed, out _stopDistance, out _audioSource);
        }

        public override void EnterState()
        {
            //Debug.Log("enter chase");
            _agent.speed = _speed;
            _agent.SetDestination(_controller.GetPlayerPosition());
            _audioSource.Play();
        }

        public override void ExitState()
        {
            _audioSource.Stop();
        }

        public override void RunState()
        {
            _controller.FacePlayer();
            _agent.SetDestination(_controller.GetPlayerPosition());
            if (_agent.remainingDistance > _stopDistance)
            {
                Move();
            }
            else
            {
                Stop();
            }
        }

        private void Move()
        {
            _agent.isStopped = false;
            _agent.speed = _speed;
        }

        private void Stop()
        {
            _agent.isStopped = true;
            _agent.speed = 0;
        }
    }
}
