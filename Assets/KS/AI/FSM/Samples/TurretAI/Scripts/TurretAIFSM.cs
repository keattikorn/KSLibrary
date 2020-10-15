using UnityEngine;

namespace KS.AI.FSM.SampleFSMs.TurretAI
{
    public class TurretAIFSM : FiniteStateMachine
    {
        public GameObject Turret { get; set; }
        
        //The player to be tracked
        [SerializeField] public Transform player;
        public Transform Player
        {
            get { return player; }
        }

        [SerializeField] public Transform bulletLauncherPosition;
        [SerializeField] public float bulletForceMagnitude = 50;
                
            
        public float detectDist = 7;
        public float visAngle = 30;
        public float atkAngle = 5;
        
        public float idleRotSpeed = 10;
        public float atkRotSpeed = 30;
        

        private void Start()
        {
            //The start state
            CurrentState = new Idle(this);
            
            Turret = GetComponent<Transform>().gameObject;
            
            //Automatically find player
            this.player = GameObject.FindWithTag("Player").transform;
        }
        
        public bool CanSeePlayer()
        {
            Vector3 direction = player.position - Turret.transform.position; // Provides the vector from the NPC to the player.
            float angle = Vector3.Angle(direction, Turret.transform.forward); // Provide angle of sight.

            // If player is close enough to the NPC AND within the visible viewing angle...
            if(direction.magnitude < detectDist && angle < visAngle)
            {
                return true; // NPC CAN see the player.
            }
            return false; // NPC CANNOT see the player.
        }

        public void Attack()
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = bulletLauncherPosition.position;
            go.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
            Rigidbody rb = go.AddComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.AddForce(bulletLauncherPosition.forward*bulletForceMagnitude,ForceMode.Impulse);
            Destroy(go,5);
        }
    }
}
