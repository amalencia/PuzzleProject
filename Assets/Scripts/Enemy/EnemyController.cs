using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace Enemy
{
    public enum EnemyState
    {
        Patrol,
        Chase,
        Attack
    }

    public class EnemyController : MonoBehaviour
    {
        private NavMeshAgent _agent;

        [Header("Patrol")]
        [SerializeField] private float _waitTime;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private AudioSource _patrolAudio;

        [Header("Chase")]
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _stopDistance;
        [SerializeField] private AudioSource _chaseAudio;

        [Header("Attack")]
        [SerializeField] private Weapon _weapon;
        [SerializeField] private string _targetTag;
        [SerializeField] private Transform _holdPoint;
        [SerializeField] private AudioSource _attackAudio;

        [Header("Scan")]
        [SerializeField] private float _viewRadius;
        [SerializeField] private float _viewAngle;
        [SerializeField] private LayerMask _playerMask;
        [SerializeField] private LayerMask _obstacleMask;

        private BaseState _currentState;
        private EnemyState _stateName;

        private Vector3 _playerPosition;

        private bool _isPlayerInRange;
        private bool _isPlayerCaught;

        // Start is called before the first frame update
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            ChangeState(new PatrolState(this));
            _stateName = EnemyState.Patrol;

            _weapon.SetUp(_targetTag);
            _weapon.AttachToCharacter(_holdPoint);
        }

        // Update is called once per frame
        void Update()
        {
            CheckSuroudings();
            if (!_isPlayerInRange && _stateName != EnemyState.Patrol)
            {
                ChangeState(new PatrolState(this));
                _stateName = EnemyState.Patrol;
            }
            else if (_isPlayerInRange && !_isPlayerCaught && _stateName != EnemyState.Chase)
            {
                ChangeState(new ChaseState(this));
                _stateName = EnemyState.Chase;
            }
            else if (_isPlayerCaught && _stateName != EnemyState.Attack)
            {
                ChangeState(new AttackState(this));
                _stateName = EnemyState.Attack;
            }

            if (_currentState != null)
            {
                _currentState.RunState();
            }
        }

        public void ChangeState(BaseState newState)
        {
            if (_currentState != null)
                _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }

        public void CheckSuroudings()
        {
            Collider[] playersInRange = Physics.OverlapSphere(transform.position, _viewRadius, _playerMask);
            _playerPosition = new Vector3(0, 100000, 0);
            _isPlayerInRange = false;
            _isPlayerCaught = false;
            for (int i = 0; i < playersInRange.Length; i++)
            {
                Transform player = playersInRange[i].transform;
                Vector3 dirToPlayer = (player.position - transform.position).normalized;
                float distToPlayer = 100000;
                if (Vector3.Angle(transform.forward, dirToPlayer) < _viewAngle / 2f)
                {
                    distToPlayer = Vector3.Distance(transform.position, player.position);
                    if (!Physics.Raycast(transform.position, dirToPlayer, distToPlayer, _obstacleMask))
                    {
                        _isPlayerInRange = true;
                    }
                }

                if (_isPlayerInRange)
                {
                    _playerPosition = player.transform.position;
                    _isPlayerCaught = distToPlayer < _stopDistance;
                    break;
                }
            }
        }

        public void FacePlayer()
        {
            // Calculate the direction to the target
            Vector3 direction = (_playerPosition - transform.position).normalized;

            // Calculate the rotation needed to face the target
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // Smoothly rotate the agent to face the target
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        public Vector3 GetPlayerPosition()
        {
            return _playerPosition;
        }

        public void GetPatrolParams(out NavMeshAgent agent, out float waitTime, out float speed, out Transform[] waypoints, out AudioSource audioSource)
        {
            agent = _agent;
            waitTime = _waitTime;
            speed = _walkSpeed;
            waypoints = _waypoints;
            audioSource = _patrolAudio;
        }

        public void GetChaseParams(out NavMeshAgent agent, out float speed, out float stopDistance, out AudioSource audioSource)
        {
            agent = _agent;
            speed = _runSpeed;
            stopDistance = _stopDistance;
            audioSource = _chaseAudio;
        }

        public void GetAttackParams(out NavMeshAgent agent, out float stopDistance, out Weapon weapon, out AudioSource audioSource)
        {
            agent = _agent;
            stopDistance = _stopDistance;
            weapon = _weapon;
            audioSource = _attackAudio;
        }
    }
}