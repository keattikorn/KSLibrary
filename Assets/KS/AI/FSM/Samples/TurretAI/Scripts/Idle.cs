using UnityEngine;

namespace KS.AI.FSM.SampleFSMs.TurretAI
{
    public class Idle : State
    {
        private TurretAIFSM turretFSM;
        private float rotateTime;
        private bool clockwise = false;
        
        public Idle(FiniteStateMachine fsm) : base(fsm)
        {
            turretFSM = (TurretAIFSM)fsm;
            
            RandomRotationParams();
        }

        private void RandomRotationParams()
        {
            rotateTime = Random.Range(1,3);
            clockwise = Random.Range(0, 100) > 50 ? true : false;
        }

        public override void Update()
        {
            //Idle rotation
            turretFSM.Turret.transform.Rotate(
                new Vector3(0,1,0),
                (clockwise==true ? 1 :-1 )*turretFSM.idleRotSpeed*Time.deltaTime,
                Space.Self);

            rotateTime -= Time.deltaTime;
            if (rotateTime <= 0 )
            {
                RandomRotationParams();
            }
            
            //If it does detect player in range
            if (turretFSM.CanSeePlayer())
            {
                this.FSM.NextState = new SampleFSMs.TurretAI.Attack(this.FSM);
                this.StateStage = StateEvent.EXIT;
            }
        }

        
    }
}
