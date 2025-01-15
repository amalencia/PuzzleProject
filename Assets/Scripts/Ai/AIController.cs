using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private Transform[] targets;
        [SerializeField] private NavMeshAgent agent;

        private AiState currentState;

        // Start is called before the first frame update
        void Start()
        {
            ChangeState(new PatrolState(this));
        }

        // Update is called once per frame
        void Update()
        {
            currentState.OnStateRun();
        }

        public void ChangeState(AiState state)
        {
            if (currentState != null)
            {
                currentState.OnStateExit();
            }
            currentState = state;

            currentState.OnStateEnter();
        }

        public NavMeshAgent GetAgent()
        {
            return agent;
        }

        public Transform[] GetPath()
        {
            return targets;
        }
    }
}