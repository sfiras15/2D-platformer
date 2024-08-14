using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManger : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    [SerializeField] private QuestInfoSO[] questInfoSOs;

    private void Awake()
    {
        questMap = CreateQuestMap();
        var quest = GetQuestById("CollectCoinsQuest");
        Debug.Log(quest.info.displayName);
        Debug.Log(quest.info.goldReward);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        questInfoSOs = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();

        foreach (QuestInfoSO quest in questInfoSOs)
        {
            if (!idToQuestMap.ContainsKey(quest.id))
            {
                idToQuestMap.Add(quest.id, new Quest(quest));
            }
            else
            {
                Debug.LogWarning("Found duplicate Quest id in the dictionary : " + quest.id);
            }
        }
        if (idToQuestMap != null) Debug.Log("Map successfull");
        return idToQuestMap;

    }
    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError("No quest found with this id :" + id);
        }
        return quest;
    }
}
