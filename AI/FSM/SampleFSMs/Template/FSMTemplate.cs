using UnityEngine;

namespace KS.AI.FSM.SampleFSMs.Template
{
    public class FSMTemplate : FiniteStateMachine
    {
        public GameObject NpcGameObject { get; set; }
        
        //TODO: delete this line
        public string currentStateName = "";
        
        void Start()
        {
            //The start state, usually be the Idle state
            CurrentState = new StateTemplate(this);
            
            NpcGameObject = GetComponent<Transform>().gameObject;
        }
        
    }
}
