using NaughtyAttributes;
using UnityEngine;

namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Marquis Path", menuName = "State Machine/Enemy/Marquis/State/Path")]
    public class MarquisPath : MarquisMachineData
    {
        [field: SerializeField, Foldout("Path")] public float Speed { get; protected set; } = 3.5f;
        [field: SerializeField, Foldout("Path")] public float StoppingDistance { get; protected set; } = 0.1f;
        
        public override MarquisMachineFunctions Initialize(MarquisMonoStateMachine machine) => new MarquisPathFunctions(machine, this);
    }

    public class MarquisPathFunctions : MarquisMachineFunctions
    {
        private float _oldSpeed;
        private float _oldStoppingDistance;

        public MarquisPathFunctions(MarquisMonoStateMachine machine, MarquisPath data) : base(machine, data)
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