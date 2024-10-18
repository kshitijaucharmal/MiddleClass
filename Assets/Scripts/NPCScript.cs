using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
struct Conversation
{
    public string conversationTopic;
    public string[] lines;
}

public class NPCScript : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] private float playerDetectRange = 3f;
    [SerializeField] private float talkRate = 0.1f;
    [SerializeField] private float pauseTime = 0.3f;

    [Header("Dialogue")]
    [SerializeField] private List<Conversation> conversations;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private GameObject dialogueTextBox;
    [SerializeField] private GameObject inputPrompt;

    private bool _speaking = false;
    private bool _skipDialogue = false;
    private Transform _player;

    // Start is called before the first frame update
    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        dialogueTextBox.SetActive(false);
        inputPrompt.SetActive(false);
        GetComponent<QuestGiver>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Null Check
        if(_player == null) {
            Debug.Log("Player not in scene");
            return;
        }
        
        // In Range
        var dist = Vector3.Distance(transform.position, _player.position);
        if (dist < playerDetectRange && !_speaking)
        {
            inputPrompt.SetActive(true);
            if (Input.GetButtonDown("StartDialogue"))
            {
                dialogueTextBox.SetActive(true);
                inputPrompt.SetActive(false);
                _speaking = true;
                StartCoroutine(SayDialogue(0));
            }
            // Disable Player Movement here
        }
        else if(dist > playerDetectRange && _speaking) {
            dialogueTextBox.SetActive(false);
            inputPrompt.SetActive(false);
            _speaking = false;
            dialogueText.text = "";
            StopAllCoroutines();
        }
        else 
            inputPrompt.SetActive(false);

        if (Input.GetButtonDown("SkipDialogue"))
        {
            Debug.Log("Skipped this dialogue");
            _skipDialogue = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerDetectRange);
    }

    IEnumerator SayDialogue(int convoIndex)
    {
        if (!_speaking) {
            dialogueText.text = "";
            yield return null;
        }
        Conversation dialogue = conversations[convoIndex];
        foreach(string line in dialogue.lines) {
            dialogueText.text = "";
            bool flag = false;
            foreach(char c in line) {
                if (_skipDialogue)
                {
                    flag = true;
                    dialogueText.text = "";
                    break;
                }
                dialogueText.text += c;
                yield return new WaitForSeconds(talkRate);
            }
            _skipDialogue = false;
            if (flag) yield return null;
            yield return new WaitForSeconds(pauseTime);
        }

        dialogueTextBox.SetActive(false);
        // Done with dialogue, give quest
        GetComponent<QuestGiver>().enabled = true;
    }
}
