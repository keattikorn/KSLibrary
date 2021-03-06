﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

        public ThirdPersonCharacterKSModified ThirdPersonChar { get; set; }
        public AICharacterControlKSModified AICharControlKsModified { get; set; }
        
        private void Start()
        {
            //The start state
            CurrentState = new SimpleCharacterAI.Idle(this);
            
            Npc = GetComponent<Transform>().gameObject;
            
            Anim = GetComponent<Animator>();
            Agent = GetComponent<NavMeshAgent>();
            
            ThirdPersonChar = GetComponent<ThirdPersonCharacterKSModified>();
            AICharControlKsModified = GetComponent<AICharacterControlKSModified>();
        }
    }

    
}

