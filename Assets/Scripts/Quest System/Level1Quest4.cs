using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
namespace Quest
{
    [CreateAssetMenu(fileName = "Quest 4 Level 1", menuName = "Quests/Level 1/Quest 4")]
    public class Level1Quest4Data : QuestData
    {
        public override QuestFunction Initialize(QuestsHandler qHandler)
        {
            return new Level1Quest4Functions(qHandler, this);
        }
    }

    public class Level1Quest4Functions : QuestFunction
    {
        bool BossDefeated;

        public Level1Quest4Functions(QuestsHandler qHandler, Level1Quest4Data qData) : base (qHandler, qData)
        {
            qHandler.AddBool(BossDefeated, "N/A");
            GlobalEvents.Instance.FindEvent("On Boss Defeated").AddListener(CheckIfDone);
        }

        void CheckIfDone()
        {
            BossDefeated = true;
            Handler.ChangeToNextQuest();
        }
        public override void QuestDiscard()
        {
            GlobalEvents.Instance.FindEvent("On Boss Defeated").RemoveListener(CheckIfDone);
        }

        public override void QuestUpdateFunc()
        {
            
        }


    }
}

