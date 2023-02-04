using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public int vNiveles_;
    public int hNiveles_;
    public float desfase_;
    public Vector2 cuadrado_;
    [Range(1, 3)] public int maxNodos_;

    public RuteNodes prefab_; 
    public RuteNodes ini_;
    public RuteNodes fin_;

    public List<BaseEvent> eventos_;
    public List<int> eventosAp_;
    public GameObject eventoTienda_;

    private int[][] logicMatrix;
    private List<List<RuteNodes>> gm = new List<List<RuteNodes>>();

    public void Start()
    {
        gm.ForEach(x => x = new List<RuteNodes>());
        CreateMap();
       
    }

    private void InitMatrix()
    {
        //Init
        logicMatrix = new int[hNiveles_][];
        for (int i = 0; i < hNiveles_; i++)
        {
            logicMatrix[i] = new int[vNiveles_];
        }
        for (int i = 0; i < vNiveles_; i++)
        {
            logicMatrix[0][i] = 1;
        }

    }

    private void FillLogicMatrix(int level)
    {
        do
        {
            ResetLevel(level);
            for (int i = 0; i < maxNodos_; i++)
            {
                int rnd = Random.Range(0, vNiveles_);
                logicMatrix[level][rnd] = 1;
            }
        } while (!IsLevelOk(level) || !IsLLevelOk(level));
    }

    private void ResetLevel(int level)
    {
        for (int i = 0; i < vNiveles_; i++)
        {
            logicMatrix[level][i] = 0;
        }
    }

    public void Update()
    {
#if DEBUG
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CreateMap();
            /*gm.ForEach(x => x.ForEach(x => Destroy(x)));
            gm.ForEach(x => x.Clear());
            gm.Clear();
            gm = CreateLevel();*/
        }
#endif
    }

    public void CreateMap()
    {
        InitMatrix();
        for (int i = 0; i < hNiveles_; i++)
        {
            FillLogicMatrix(i);
        }

        gm.ForEach(x => x.ForEach(x => Destroy(x)));
        gm.ForEach(x => x.Clear());
        gm.Clear();
        gm = CreateLevel();
        LinkNodes();
    }

    private List<List<RuteNodes>> CreateLevel()
    {
        List<List<RuteNodes>> gg = new List<List<RuteNodes>>();
        for (int i = 0; i < hNiveles_; i++)
        {
            List<RuteNodes> g = new List<RuteNodes>();
            for (int j = 0; j < vNiveles_; j++)
            {
                if (logicMatrix[i][j] == 1)
                {
                    float x = (j - ((vNiveles_ - 1) / 2f)) * cuadrado_.x;
                    x = Random.Range(-desfase_, desfase_) + x;
                    float y = (i - ((hNiveles_ - 1) / 2f)) * cuadrado_.y;
                    y = Random.Range(-desfase_, desfase_) + y;
                    RuteNodes obj = Instantiate(prefab_.gameObject, gameObject.transform).GetComponent<RuteNodes>();
                    obj.transform.position = new Vector2(x, y);
                    g.Add(obj);
                }
                else
                    g.Add(null);
            }
            gg.Add(g);
        }
        return gg;
    }

    public void SpawnMap()
    {
        gm.ForEach(x => x.ForEach(x => Instantiate(x)));
    }

    private bool IsLLevelOk(int level)
    {
        level -= 1;
        if (level < 0 || level >= vNiveles_ - 1)
            return true;

        bool isOk = true;
        for (int i = 0; i < vNiveles_; i++)
        {
            bool[] ok = { false, false, false };
            if (logicMatrix[level][i] != 1)
                continue;

            ok[1] = logicMatrix[level + 1][i] == 1;
            if (i > 0)
                ok[0] = logicMatrix[level + 1][i - 1] == 1;
            if (i < vNiveles_-1)
                ok[2] = logicMatrix[level + 1][i + 1] == 1;

            isOk = isOk && (ok[0] || ok[1] || ok[2]);
        }

        return isOk;
    }

    private bool IsLevelOk(int level)
    {
        if (level <= 0)
            return true;

        bool isOk = true;
        for (int i = 0; i < vNiveles_; i++)
        {
            bool[] ok = { false, false, false };
            if (logicMatrix[level][i] != 1)
                continue;

            ok[1] = logicMatrix[level - 1][i] == 1;
            if (i > 0)
                ok[0] = logicMatrix[level - 1][i - 1] == 1;
            if (i < vNiveles_ - 1)
                ok[2] = logicMatrix[level - 1][i + 1] == 1;

            isOk = isOk && (ok[0] || ok[1] || ok[2]);
        }

        return isOk;
    }

    //Link todos los nodos entre ellos
    #region LINK
    private void LinkNodes()
    {
        for (int i = 0; i < hNiveles_; i++)
        {
            for (int j = 0; j < vNiveles_; j++)
            {
                if(logicMatrix[i][j] == 1)
                {
                    LinkNextNodes(j, i);
                    LinkPrevNodes(j, i);
                }
            }
        }
        LinkIniFin();
    }
    private void LinkNextNodes(int x, int y)
    {
        try
        {
            if (y + 1 == hNiveles_)
            {
                gm[y][x].SetNextNode(fin_);
                return;
            }

            if (logicMatrix[y + 1][x] == 1)
                gm[y][x].SetNextNode(gm[y + 1][x], 1);
            if (x > 0 && logicMatrix[y + 1][x - 1] == 1)
                gm[y][x].SetNextNode(gm[y + 1][x - 1], 0);
            if (x < vNiveles_ - 1 && logicMatrix[y + 1][x + 1] == 1)
                gm[y][x].SetNextNode(gm[y + 1][x + 1], 2);
        }
        catch (System.Exception e)
        {
            Debug.Log("NextNodes: ");
            Debug.Log(y + "-" + x);
            throw e;
        }
        
    }

    private void LinkPrevNodes(int x, int y)
    {
        try
        {
            if (y - 1 < 0)
            {
                gm[y][x].SetPrevNode(ini_);
                return;
            }
            if (logicMatrix[y - 1][x] == 1)
                gm[y][x].SetPrevNode(gm[y - 1][x], 1);
            if (x > 0 && logicMatrix[y - 1][x - 1] == 1)
                gm[y][x].SetPrevNode(gm[y - 1][x - 1], 0);
            if (x < vNiveles_ - 1 && logicMatrix[y - 1][x + 1] == 1)
                gm[y][x].SetPrevNode(gm[y - 1][x + 1], 2);

        }
        catch (System.Exception e)
        {
            Debug.Log("PrevNodes: ");
            Debug.Log(y + "-" + x);
            throw e;
        }
    }


    private void LinkIniFin()
    {
        int f = 0, n = 0;
        for (int i = 0; i < vNiveles_; i++)
        {            
            if (logicMatrix[0][i] == 1)
            {
                ini_.SetNextNode(gm[0][i], n);
                n++;
            }

            if (logicMatrix[hNiveles_ - 1][i] == 1)
            {
                fin_.SetPrevNode(gm[hNiveles_ - 1][i], f);
                f++;
            }
        }
    }

    #endregion

    #region EVENTS
    public BaseEvent GetRandomEvent()
    {
        bool isOk = false;
        BaseEvent e = null;

        do
        {
            int r = Random.Range(0, eventos_.Count);
            if (eventosAp_[r] > 0)
            {
                e = eventos_[r];
                eventosAp_[r]--;
                isOk = true;
            }

        } while (!isOk);

        return e;
    }

    private void SetTiendas(int n)
    {
        for (int i = 0; i < n; i++)
        {

        }
    }

    #endregion
}
