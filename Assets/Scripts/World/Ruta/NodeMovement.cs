using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Rewired;

public class NodeMovement : MonoBehaviour
{
    //public WorldGeneration map_;

    public RuteNodes currentNode;


    private void Update()
    {
        if (ReInput.players.Players[0].GetButton("ButtonA"))
        {
            if(EventSystem.current.currentSelectedGameObject != currentNode?.gameObject)
            {
                currentNode?.CleanPrev();
                currentNode = EventSystem.current.currentSelectedGameObject.GetComponent<RuteNodes>();
                currentNode?.CleanPrev();
                currentNode.SetNextNavigation();

            }
        }         
    }
}
