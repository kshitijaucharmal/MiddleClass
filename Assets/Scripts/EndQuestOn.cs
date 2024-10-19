using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndQuestOn : MonoBehaviour
{
    public string quest_id;
    public string tagToCheck;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagToCheck))
        {
            QuestManager.Instance.EndQuest(quest_id);
            Debug.Log("Completed Quest: " + quest_id);
        }
    }
}
