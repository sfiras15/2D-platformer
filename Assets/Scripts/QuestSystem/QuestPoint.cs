using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    private bool isNearPlayer = false;

    [SerializeField] private KeyCode interactKey;
    [Header("Quest")]

    [SerializeField] QuestInfoSO questInfo;

    private QuestState currentQuestState;
    private string questId;

    [Header("Config")]

    [SerializeField] private bool startPoint;
    [SerializeField] private bool finishPoint;




    private void Awake()
    {
        questId = questInfo.id;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStateChangeQuest += ChangeQuestState;
    }

    public void ChangeQuestState(Quest quest)
    {
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            Debug.Log("quest with id: " + questId + " updated to state : " + currentQuestState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        interact();
    }

    public void interact()
    {
        if (!isNearPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(interactKey))
        {
            if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
            {
                GameEventsManager.instance.questEvents.StartQuest(questId);
            }
            else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
            {
                GameEventsManager.instance.questEvents.FinishQuest(questId);
            }

           //GameEventsManager.instance.questEvents.AdvanceQuest(questId);

        }
        
    }
}
