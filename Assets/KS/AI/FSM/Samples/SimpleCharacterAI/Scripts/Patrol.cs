using UnityEngine;
using UnityEngine.AI;

namespace KS.AI.FSM.SampleFSMs.SimpleCharacterAI
{
    public class Patrol : State
    {
        private SimpleAIFSM simpleAIFSM;
        
        private int currentWaypointIdx = -1;

        private NavMeshAgent agent;
        private ThirdPersonCharacterKSModified thirdPersonCharacterKsModified;
        private AICharacterControlKSModified aiCharacterControlKsModified;
        
        public Patrol(FiniteStateMachine fsm) : base(fsm)
        {
            simpleAIFSM = ((SimpleAIFSM) this.FSM);
            
            agent = ((SimpleAIFSM) this.FSM).Agent;
            thirdPersonCharacterKsModified = ((SimpleAIFSM) this.FSM).ThirdPersonChar;
            aiCharacterControlKsModified = ((SimpleAIFSM) this.FSM).AICharControlKsModified;

            
        }

        public override void Enter()
        {
            float lastDist = Mathf.Infinity; // Store distance between NPC and waypoints.
            
            // Calculate closest waypoint by looping around each one and calculating the distance between the NPC and each waypoint.
            for (int i = 0; i < simpleAIFSM.wayPoints.Count; i++)
            {
                GameObject thisWP = simpleAIFSM.wayPoints[i].gameObject;
                float distance = Vector3.Distance(simpleAIFSM.Npc.transform.position, thisWP.transform.position);
                if(distance < lastDist)
                {
                    currentWaypointIdx = i;
                    lastDist = distance;

                    simpleAIFSM.AICharControlKsModified.SetTarget(thisWP.transform);
                }            
            }
            
            //Proceed to the next stage of the FSM
            base.Enter();
        }

        public override void Update()
        {
            if (aiCharacterControlKsModified.target != null)
                agent.SetDestination(aiCharacterControlKsModified.target.position);

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                //Move the agent
                thirdPersonCharacterKsModified.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                //increase waypoint index
                if (currentWaypointIdx >= simpleAIFSM.wayPoints.Count-1)
                    currentWaypointIdx = 0;
                else
                    currentWaypointIdx++;

                //Set target to the next waypoint
                aiCharacterControlKsModified.SetTarget(simpleAIFSM.wayPoints[currentWaypointIdx]);
                agent.SetDestination(aiCharacterControlKsModified.target.position);

                //Stop the character movement
                thirdPersonCharacterKsModified.Move(Vector3.zero, false, false);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}