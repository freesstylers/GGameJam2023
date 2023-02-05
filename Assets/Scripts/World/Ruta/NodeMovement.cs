using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Rewired;

public class NodeMovement : MonoBehaviour
{
    public WorldGeneration map_;
    public RuteNodes currentNode;
    public EventHandler evH;

    private void Update()
    {
        if (ReInput.players.Players[0].GetButtonDown("ButtonA"))
        {
            if(EventSystem.current.currentSelectedGameObject != currentNode?.gameObject)
            {
                currentNode?.CleanPrev();
                currentNode = EventSystem.current.currentSelectedGameObject.GetComponent<RuteNodes>();
                currentNode?.CleanPrev();
                currentNode.SetNextNavigation();
                currentNode.SetEvent(map_.GetRandomEvent());
                evH.SetEvento(currentNode.gameEvent_);
                GameManager.instance.levelLoader.LoadTransition(States.GameState);

            }
        }         
    }
}
