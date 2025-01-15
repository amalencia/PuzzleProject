using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class CommandInteractor : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent; 
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask clickableLayer;
    [SerializeField] private Queue<Command> commands = new Queue<Command>();

    private Command currentCommand; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCommand != null)
        {
            currentCommand.Execute();
            if (currentCommand.IsCompleted())
            {
                currentCommand = null;
            }
        }
        else
        {
            if (commands.Count != 0)
            {
                return; 
            }
            if (commands.Count > 0)
            {
                currentCommand = commands.Dequeue(); //Removes the first one in the queue and assigns to it
            }
        } 
        //Takes the first command and keep track of it as a "currentCommand" 
        //Check if this command is completed, 
        //if completed, remove this command and go to next one 
        //if not completed, just keep executing 

    }

    public void CreateCommand()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 5f, clickableLayer))
        {
            agent.SetDestination(hit.point); 
            commands.Enqueue(new MoveCommand(agent, hit.point));
        } 
    }
}
