using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Moves { Left, Top, Right };

public class RuteNodes : MonoBehaviour
{
    public int id_;
    public GameObject camino_;

    //public GameEvent gameEvent_;

    private RuteNodes[] nextNodes = new RuteNodes[3];
    private RuteNodes[] prevNodes = new RuteNodes[3];

    #region Setters
    public void SetNextNode(RuteNodes node, int n = 1)
    {
        nextNodes[n] = node;
        GameObject g = Instantiate(camino_, transform);
        g.transform.position = ((node.transform.position - transform.position) / 2f) + transform.position;
        float r = Vector3.SignedAngle((node.transform.position - transform.position), Vector3.up, Vector3.forward);
        g.transform.Rotate(new Vector3(0, 0, -r));
        float d = Vector2.Distance(transform.position, node.transform.position);
        g.transform.localScale = new Vector3(g.transform.localScale.x, d*2, 0);
    }

    public void SetNextNode(RuteNodes[] nodes)
    {
        nextNodes = nodes;
    }

    public void SetPrevNode(RuteNodes node, int n = 1)
    {
        prevNodes[n] = node;
    }

    public void SetPrevNode(RuteNodes[] nodes)
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
