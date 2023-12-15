using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
namespace Quest
{
    [CreateAssetMenu(fileName = "Quest 1 Level 1", menuName = "Quests/Level 1/Quest 1")]
    public class Level1Quest1Data : QuestData
    {
        [SerializeField] float enemiesNeedToKill;
        public float EnemiesNeetToKill => enemiesNeedToKill;
        public override QuestFunction Initialize(QuestsHandler qHandler)
        {
            return new Level1Quest1Functions(qHandler, this);
        }
    }

    public class Level1Quest1Functions : QuestFunction
    {
        float EnemiesKilled;
        float EnemiesNeededToBeKilled;

        bool AreAllEnemiesKilled = false;
        public Level1Quest1Functions(QuestsHandler qHandler, Level1Quest1Data qData) : base (qHandler, qData)
        {
            EnemiesNeededToBeKilled = qData.EnemiesNeetToKill;
            Debug.Log(EnemiesNeededToBeKilled);
            GlobalEvents.Instance.FindEvent("On Any Enemy Death")?.AddListener(AddEnemiesKilled);
            qHandler.AddFloat(EnemiesKilled, EnemiesNeededToBeKilled, "Enemies Killed");
            qHandler.AddBool(AreAllEnemiesKilled, "Enemies are Dead");
        }

        void AddEnemiesKilled()
        {
            EnemiesKilled += 1;
            Handler.UpdateFloat("Enemies Killed", EnemiesKilled);
            CheckIfDone();
        }

        void CheckIfDone()
        {
            if (EnemiesKilled >= EnemiesNeededToBeKilled)
            {
                Handler.ChangeToNextQuest();
            }
        }
        public override void QuestDiscard()
        {
            GlobalEvents.Instance.FindEvent("On Any Enemy Death").RemoveListener(AddEnemiesKilled);
        }

        public override void QuestUpdateFunc()
        {
            
        }


    }
}

