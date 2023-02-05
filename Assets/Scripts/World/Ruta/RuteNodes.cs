using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Moves { Left, Up, Right };

public class RuteNodes : MonoBehaviour
{
    public int id_;
    public int lvl_;
    public GameObject camino_;

    public BaseEvent gameEvent_ = null;

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

    public void SetEvent(BaseEvent b)
    {
        if(gameEvent_ == null)
            gameEvent_ = b;
    }

    #endregion
    #region GETTER
    public RuteNodes[] GetNextNodes() { return nextNodes; }
    public RuteNodes[] GetPrevNodes() { return prevNodes; }
    #endregion
    #region NAVEGATION
    public void SetNextNavigation()
    {

        Navigation n = GetComponent<Button>().navigation;
        n.mode = Navigation.Mode.Explicit;
        n.selectOnLeft = GetNextNodes()[(int)Moves.Left]?.GetComponent<Button>();
        n.selectOnUp = GetNextNodes()[(int)Moves.Up]?.GetComponent<Button>();
        n.selectOnRight =  GetNextNodes()[(int)Moves.Right]?.GetComponent<Button>();

        foreach (RuteNodes nNodes in nextNodes)
        {
            if(nNodes != null)
                nNodes.SetLastNavigation(this);
        }

        GetComponent<Button>().navigation = n;        
    }

    private void SetLastNavigation(RuteNodes lastNode)
    {
        Navigation n = GetComponent<Button>().navigation;
        n.mode = Navigation.Mode.Explicit;
        n.selectOnDown = lastNode.GetComponent<Button>();

        GetComponent<Button>().navigation = n;
    }

    public void CleanPrev()
    {
        Navigation n = GetComponent<Button>().navigation;
        n.mode = Navigation.Mode.None;
        n.selectOnLeft = null;
        n.selectOnUp = null;
        n.selectOnRight = null;
        n.selectOnDown = null;
        GetComponent<Button>().navigation = n;
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
