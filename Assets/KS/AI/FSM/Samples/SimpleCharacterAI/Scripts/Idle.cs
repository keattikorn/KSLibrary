using UnityEngine;

namespace KS.AI.FSM.SampleFSMs.SimpleCharacterAI
{
    public class Idle : State
    {
        public Idle(FiniteStateMachine fsm) : base(fsm)
        {
        }

        public override void Enter()
        {
            //Proceed to the next stage of the FSM state
            StateStage = StateEvent.UPDATE;
        }

        public override void Update()
        {
            if (Random.Range(0, 100) < 10)
            {
                this.FSM.NextState = new SimpleCharacterAI.Patrol(this.FSM);
                this.StateStage = StateEvent.EXIT; // The next time 'Process' runs, the EXIT stage will run instead, which will then return the nextState.
            }
        }

        public override void Exit()
        {

        }
    }
}