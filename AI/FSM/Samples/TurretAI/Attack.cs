using UnityEngine;

namespace KS.AI.FSM.SampleFSMs.TurretAI
{
    public class Attack : State
    {
        private TurretAIFSM turretFSM;

        private float coolDown = -1;
        
        public Attack(FiniteStateMachine fsm) : base(fsm)
        {
            turretFSM = (TurretAIFSM)fsm;
        }
        
        public override void Update()
        {
            
            Vector3 playerPos = turretFSM.player.transform.position;
            Vector3 turretPos = turretFSM.Turret.transform.position;
            
            Quaternion lookatPlayer = Quaternion.LookRotation(playerPos - turretPos);
            
            Vector3 direction = playerPos - turretPos; // Provides the vector from the NPC to the player.
            float angle = Vector3.Angle(direction, turretFSM.Turret.transform.forward); // Provide angle of sight.
            
            //Rotate toward the dected player
            turretFSM.Turret.transform.rotation = Quaternion.Slerp(
                turretFSM.Turret.transform.rotation,
                lookatPlayer, 
                turretFSM.atkRotSpeed * Time.deltaTime);


            if (angle < turretFSM.atkAngle)
            {
                if (coolDown <= 0)
                {
                    turretFSM.Attack();
                    coolDown = 1;
                }
            }

            coolDown -= Time.deltaTime;
            
            
            
            //Player out of range
            if (direction.magnitude > turretFSM.detectDist)
            {
                this.FSM.NextState = new Idle(this.FSM);
                this.StateStage = StateEvent.EXIT;
            }
        }
    }
}