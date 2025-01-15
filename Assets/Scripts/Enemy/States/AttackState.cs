using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AttackState : BaseState
    {
        private NavMeshAgent _agent;
        private float _stopDistance;
        private Weapon _weapon;
        private AudioSource _audioSource;

        public AttackState(EnemyController controller) : base(controller)
        {
            _controller.GetAttackParams(out _agent, out _stopDistance, out _weapon, out _audioSource);
        }

        public override void EnterState()
        {
            //Debug.Log("enter attack");
            _agent.isStopped = true;
            _agent.speed = 0;
            _weapon.ActOnAttackHappened += PlayAudio;
        }

        public override void ExitState()
        {
            _weapon.DisableAttack();
            _weapon.BackToIdle();
            _weapon.ActOnAttackHappened -= PlayAudio;
        }

        public override void RunState()
        {
            _controller.FacePlayer();
            _agent.SetDestination(_controller.GetPlayerPosition());
            if (_agent.remainingDistance <= _stopDistance)
            {
                _weapon.Attack();
            }
        }

        private void PlayAudio()
        {
            _audioSource.Play();
        }
    }
}