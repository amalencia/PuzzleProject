using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class PatrolState : BaseState
    {
        private NavMeshAgent _agent;

        private float _waitTime;
        private float _speed;
        private Transform[] _waypoints;
        private AudioSource _audioSource;

        private int _index;

        private float _timeWaited;

        public PatrolState(EnemyController controller) : base(controller)
        {
            _controller.GetPatrolParams(out _agent, out _waitTime, out _speed, out _waypoints, out _audioSource);
        }

        public override void EnterState()
        {
            //Debug.Log("enter patrol");
            _index = Random.Range(0, _waypoints.Length);
            _audioSource.Play();
        }

        public override void ExitState()
        {
            _audioSource.Stop();
        }

        public override void RunState()
        {
            Patrol();
        }

        private void Patrol()
        {
            Move(_speed, _waypoints[_index].position);
            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (_timeWaited <= 0)
                {
                    SetNextWayPointIndex();
                    _timeWaited = _waitTime;
                }
                else
                {
                    Stop();
                    _timeWaited -= Time.deltaTime;
                }
            }
        }

        private void SetNextWayPointIndex()
        {
            _index = (_index + 1) % _waypoints.Length;
        }

        private void Move(float speed, Vector3 pos)
        {
            _agent.isStopped = false;
            _agent.speed = speed;
            _agent.SetDestination(pos);
        }

        private void Stop()
        {
            _agent.isStopped = true;
            _agent.speed = 0;
        }
    }
}

