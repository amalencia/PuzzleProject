using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MoveCommand : Command
{
    private NavMeshAgent agentToCommand;
    private Vector3 destination; 
    private bool arrivedAtDestination; 

    public override void Execute()
    {
        //Set AI destination to a vector3
        agentToCommand.SetDestination(destination); 

        if(agentToCommand.remainingDistance < 0.2f)
        {
            arrivedAtDestination = true;
        }
        //If arrived at destination 
        //arrivedAtDestination = true; 
    }

    public override bool IsCompleted()
    {
        return arrivedAtDestination;  
    }

    public MoveCommand(NavMeshAgent agent, Vector3 positioin)
    {
        agentToCommand = agent; 
        destination = positioin;
    }
}
