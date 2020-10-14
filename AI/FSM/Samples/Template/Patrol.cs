using UnityEngine;

namespace KS.AI.FSM.SampleFSMs.Template
{
    public class Patrol : State
    {
        private FSMTemplate fsmTemplate;
        
        private float timeCountDown = 0;
        
        public Patrol(FiniteStateMachine fsm) : base(fsm)
        {
            //Cast the fsm (FiniteStateMachine) to the specific one
            fsmTemplate = (FSMTemplate) fsm;
            
            //TODO: delete this line
            fsmTemplate.currentStateName = this.GetType().Name;
        }

        public override void Enter()
        {
            //TODO: do something before enter this state
            
            timeCountDown = Random.Range(3, 5);
            
            base.Enter();
        }

        public override void Update()
        {
            //TODO: Do state logic
            timeCountDown -= Time.deltaTime;
            
            if (timeCountDown<=0)
            {
                this.fsmTemplate.NextState = new StateTemplate(this.FSM);
                this.StateStage = StateEvent.EXIT;
            }
        }
        
        public override void Exit()
        {
            //TODO: do something before exit this state
            
            base.Exit();
        }
    }
}

