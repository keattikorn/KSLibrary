using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace KS.AI.FSM.SampleFSMs.SimpleCharacterAI
{
    [RequireComponent(typeof(UnityEngine.Animator))]
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    public class SimpleAIFSM : FiniteStateMachine
    {
        public List<Transform> wayPoints;
        
        public GameObject Npc { get; set; }
        public Animator Anim { get; set; }

        [SerializeField] public Transform player;
        public Transform Player
        {
            get { return player; }
        }
        public NavMeshAgent Agent { get; set; }

        public ThirdPersonCharacter ThirdPersonChar { get; set; }
        public AICharacterControl AICharControl { get; set; }
        
        private void Start()
        {
            //The start state
            CurrentState = new SimpleCharacterAI.Idle(this);
            
            Npc = GetComponent<Transform>().gameObject;
            
            Anim = GetComponent<Animator>();
            Agent = GetComponent<NavMeshAgent>();
            
            ThirdPersonChar = GetComponent<ThirdPersonCharacter>();
            AICharControl = GetComponent<AICharacterControl>();
        }
    }

    
}

