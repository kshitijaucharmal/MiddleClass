using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Quest quest;

    [SerializeField] private Action<Quest> questStart;
    [SerializeField] private Action<Quest> questEnd;


    // Start is called before the first frame update
    void Start()
    {
        questStart += QuestSetup;
        questEnd += QuestCleanup;
    }

    void AcceptQuest()
    {
        
    }

    void RejectQuest()
    {

    }

    void QuestSetup(Quest quest) {
        Debug.Log("Setup Quest");
        QuestManager.Instance.AddQuest(quest);
    }
    void QuestCleanup(Quest quest) {
        Debug.Log("Cleanup Quest");
        QuestManager.Instance.RemoveQuest(quest);
        Destroy(this);
    }

    public void GiveQuest() {
        questStart?.Invoke(quest);
    }
}
