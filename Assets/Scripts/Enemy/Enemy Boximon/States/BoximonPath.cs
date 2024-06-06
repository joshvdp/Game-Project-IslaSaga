using NaughtyAttributes;
using UnityEngine;

namespace StateMachine.Enemy.State
{
    
    [CreateAssetMenu(fileName = "Boximon Path", menuName = "State Machine/Enemy/Boximon/State/Path")]
    public class BoximonPath : BoximonMachineData
    {
        [field: SerializeField, Foldout("Path")] public float Speed { get; protected set; } = 3.5f;
        [field: SerializeField, Foldout("Path")] public float StoppingDistance { get; protected set; } = 0.1f;
        

        public override BoximonMachineFunctions Initialize(BoximonMonoStateMachine machine) => new BoximonPathFunctions(machine, this);
    }

    public class BoximonPathFunctions : BoximonMachineFunctions
    {
        private float _oldSpeed;
        private float _oldStoppingDistance;

        public BoximonPathFunctions(BoximonMonoStateMachine machine, BoximonPath data) : base(machine, data)
        {
            machine.Agent.isStopped = false;
            _oldSpeed = machine.Agent.speed;
            _oldStoppingDistance = machine.Agent.stoppingDistance;
            
            machine.Agent.speed = data.Speed;
            machine.Agent.stoppingDistance = data.StoppingDistance;
            
            machine.Agent.SetDestination(machine.EnemySpawnPosition.GetPoint());
        }
        
        public override void Discard()
        {
            machine.Agent.speed = _oldSpeed;
            machine.Agent.stoppingDistance = _oldStoppingDistance;
            
            base.Discard();
        }
    }
}