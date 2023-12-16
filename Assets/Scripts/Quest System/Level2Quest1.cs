using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
namespace Quest
{
    [CreateAssetMenu(fileName = "Quest 1 Level 2", menuName = "Quests/Level 2/Quest 1")]
    public class Level2Quest1Data : QuestData
    {
        [SerializeField] float enemiesNeedToKill;
        public float EnemiesNeetToKill => enemiesNeedToKill;
        public override QuestFunction Initialize(QuestsHandler qHandler)
        {
            return new Level2Quest1Functions(qHandler, this);
        }
    }

    public class Level2Quest1Functions : QuestFunction
    {
        bool DoubleJumpAcquired;

        float EnemiesKilled;
        float EnemiesNeededToBeKilled;
        public Level2Quest1Functions(QuestsHandler qHandler, Level2Quest1Data qData) : base (qHandler, qData)
        {
            EnemiesKilled = 0;
            EnemiesNeededToBeKilled = qData.EnemiesNeetToKill;
            qHandler.AddBool(DoubleJumpAcquired, "N/A");
            qHandler.AddFloat(EnemiesKilled, EnemiesNeededToBeKilled, "Enemies Killed");
            GlobalEvents.Instance.FindEvent("On Double Jump Achieved").AddListener(OnAbilityAcquired);
            GlobalEvents.Instance.FindEvent("On Any Enemy Death")?.AddListener(AddEnemiesKilled);
        }

        void AddEnemiesKilled()
        {
            EnemiesKilled++;
            Handler.UpdateFloat("Enemies Killed", EnemiesKilled);
            CheckIfDone();
        }
        void CheckIfDone()
        {
            if(DoubleJumpAcquired && EnemiesKilled >= EnemiesNeededToBeKilled) Handler.ChangeToNextQuest();
        }

        void OnAbilityAcquired()
        {
            DoubleJumpAcquired = true;
            CheckIfDone();
        }
        public override void QuestDiscard()
        {
            GlobalEvents.Instance.FindEvent("On Double Jump Achieved").RemoveListener(CheckIfDone);

            GlobalEvents.Instance.FindEvent("On Any Enemy Death").RemoveListener(AddEnemiesKilled);
        }

        public override void QuestUpdateFunc()
        {
            
        }


    }
}

