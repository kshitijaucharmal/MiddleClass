using System;

[System.Serializable]
public class Quest
{
    public string questId = "q_sample_quest";
    public string questTitle = "Sample Quest";
    public string questDescription = "Fetch Some Item";

    public string reward = "1 key";

    public bool isActive = false;
}
