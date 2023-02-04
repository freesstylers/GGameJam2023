using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuteNodes : MonoBehaviour
{
    public int id_;

    private List<RuteNodes> nextNodes;
    private List<RuteNodes> prevNodes;

    #region Setters
    public void SetNextNode(RuteNodes node)
    {
        nextNodes.Add(node);
    }

    public void SetNextNode(List<RuteNodes> nodes)
    {
        nextNodes = nodes;
    }

    public void SetPrevNode(RuteNodes node)
    {
        prevNodes.Add(node);
    }

    public void SetPrevNode(List<RuteNodes> nodes)
    {
        prevNodes = nodes;
    }
    #endregion

    public bool CanTravel(RuteNodes node)
    {
        bool canTravel = false;

        foreach (RuteNodes n in nextNodes)
        {
            canTravel = canTravel || n == node;
        }

        return canTravel;
    }

}
