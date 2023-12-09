using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using InterfaceAndInheritables;
namespace Quest
{
    public class QuestUIHandler : MonoBehaviour
    {
        public static QuestUIHandler Instance;
        [SerializeField] GameObject QuestContent;
        [SerializeField] Transform ContentParent;
        public TextMeshProUGUI QuestTitle; 
        
        public List<QuestContentHolder> QuestContents;
        private void OnEnable()
        {
            QuestsHandler.Instance.OnChangeQuest.AddListener(DisplayNewQuest);
        }

        private void OnDisable()
        {
            QuestsHandler.Instance.OnChangeQuest.RemoveListener(DisplayNewQuest);
        }
        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;

        }
        private void Start()
        {
            QuestsHandler.Instance.ChangeToQuestByIndex(0);
        }
        void DisplayNewQuest(QuestData newQuest)
        {
            DeleteAllQuestContents();
            QuestTitle.text = newQuest.QuestName;
            AddQuestContent();
        }

        void AddQuestContent()
        {
            if(QuestsHandler.Instance.Bools.Count > 0)
            {
                for (int i = 0; i < QuestsHandler.Instance.Bools.Count; i++)
                {
                    QuestContentHolder questContent = Instantiate(QuestContent, ContentParent).GetComponent<QuestContentHolder>();
                    questContent.QuestRequirementText.text = QuestsHandler.Instance.Bools[i].BooleanName;
                    questContent.QuestRequirementStatusText.text = QuestsHandler.Instance.Bools[i].Boolean.ToString();
                    QuestContents.Add(questContent);
                }
            }
            if (QuestsHandler.Instance.Floats.Count > 0)
            {
                for (int i = 0; i < QuestsHandler.Instance.Floats.Count; i++)
                {
                    QuestContentHolder questContent = Instantiate(QuestContent, ContentParent).GetComponent<QuestContentHolder>();
                    questContent.QuestRequirementText.text = QuestsHandler.Instance.Floats[i].FloatName;
                    questContent.QuestRequirementStatusText.text = QuestsHandler.Instance.Floats[i].Float + "/" + QuestsHandler.Instance.Floats[i].MaxFloat;
                    QuestContents.Add(questContent);
                }
            }
        }

        public void DeleteAllQuestContents()
        {
            for (int i = 0; i < QuestContents.Count; i++)
            {
                Destroy(QuestContents[i].gameObject);
            }
            QuestContents.Clear();
        }

        public void UpdateFloatQuestContent(FloatWithNameAndMaxVal floatVar) => 
            QuestContents.Find(_ => _.QuestRequirementText.text == floatVar.FloatName).QuestRequirementStatusText.text = floatVar.Float + "/" + floatVar.MaxFloat;
    }

}
