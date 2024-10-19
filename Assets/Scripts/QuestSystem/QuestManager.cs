using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    [SerializeField] private TMP_Text allQuestsText;

    // Static instance variable
    public static QuestManager Instance { get; private set; }

    private void Awake()
    {
        // Check if there is already an instance of GameManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy this instance if it's not the only one
            return;
        }

        Instance = this; // Set the instance to this instance
    }

    public List<Quest> activeQuests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        quest.isActive = true;
        activeQuests.Add(quest);

        UpdateUI();
    }

    void UpdateUI()
    {
        allQuestsText.text = "";
        foreach(Quest quest in activeQuests)
        {
            allQuestsText.text += quest.questTitle + ": " + quest.questDescription + "\n";
        }
    }

    public void RemoveQuest(Quest quest)
    {
        activeQuests.Remove(quest);

        UpdateUI();
    }

    public void EndQuest(string questId)
    {
        foreach(Quest quest in activeQuests)
        {
            if(quest.questId == questId)
            {
                RemoveQuest(quest);
                Debug.Log(quest.questTitle + " Completed");

                Debug.Log("Gained Reward: " + quest.reward);
            }
        }
    }
}
