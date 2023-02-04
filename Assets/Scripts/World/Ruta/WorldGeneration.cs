using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public int hNiveles_;
    public int vNiveles_;
    public float desfase_;
    [Range(1, 5)] public int maxNodos_;

    public RuteNodes prefab_; 
    public RuteNodes ini_;
    public RuteNodes fin_;

    public List<GameObject> eventos_;

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
        logicMatrix = new int[vNiveles_][];
        for (int i = 0; i < vNiveles_; i++)
        {
            logicMatrix[i] = new int[hNiveles_];
        }
        for (int i = 0; i < hNiveles_; i++)
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
                int rnd = Random.Range(0, hNiveles_);
                logicMatrix[level][rnd] = 1;
            }
        } while (!IsLevelOk(level) || !IsLLevelOk(level));
    }

    private void ResetLevel(int level)
    {
        for (int i = 0; i < hNiveles_; i++)
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
            gm.ForEach(x => x.ForEach(x => Destroy(x)));
            gm.ForEach(x => x.Clear());
            gm.Clear();
            gm = CreateLevel();
        }
#endif
    }

    public void CreateMap()
    {
        InitMatrix();
        for (int i = 0; i < vNiveles_; i++)
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
        for (int i = 0; i < vNiveles_; i++)
        {
            List<RuteNodes> g = new List<RuteNodes>();
            for (int j = 0; j < hNiveles_; j++)
            {
                if (logicMatrix[i][j] == 1)
                {
                    float x = Random.Range(-desfase_, desfase_) + j;
                    float y = Random.Range(-desfase_, desfase_) + i;
                    g.Add(Instantiate(prefab_.gameObject, new Vector2(x, y), Quaternion.identity).GetComponent<RuteNodes>());
                }
                else
                    g.Add(null);
            }
            gg.Add(g);
        }
        return gg;
    }

    private bool IsLLevelOk(int level)
    {
        level -= 1;
        if (level < 0 || level >= hNiveles_ - 1)
            return true;

        bool isOk = true;
        for (int i = 0; i < hNiveles_; i++)
        {
            bool[] ok = { false, false, false };
            if (logicMatrix[level][i] != 1)
                continue;

            ok[1] = logicMatrix[level + 1][i] == 1;
            if (i > 0)
                ok[0] = logicMatrix[level + 1][i - 1] == 1;
            if (i < hNiveles_-1)
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
        for (int i = 0; i < hNiveles_; i++)
        {
            bool[] ok = { false, false, false };
            if (logicMatrix[level][i] != 1)
                continue;

            ok[1] = logicMatrix[level - 1][i] == 1;
            if (i > 0)
                ok[0] = logicMatrix[level - 1][i - 1] == 1;
            if (i < hNiveles_ - 1)
                ok[2] = logicMatrix[level - 1][i + 1] == 1;

            isOk = isOk && (ok[0] || ok[1] || ok[2]);
        }

        return isOk;
    }

    //Link todos los nodos entre ellos
    #region LINK
    private void LinkNodes()
    {
        for (int i = 0; i < vNiveles_; i++)
        {
            for (int j = 0; j < hNiveles_; j++)
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
            if (y + 1 == vNiveles_)
            {
                gm[y][x].SetNextNode(fin_);
                return;
            }

            if (logicMatrix[y + 1][x] == 1)
                gm[y][x].SetNextNode(gm[y + 1][x], 1);
            if (x > 0 && logicMatrix[y + 1][x - 1] == 1)
                gm[y][x].SetNextNode(gm[y + 1][x - 1], 0);
            if (x < hNiveles_ - 1 && logicMatrix[y + 1][x + 1] == 1)
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
            if (x < hNiveles_ - 1 && logicMatrix[y - 1][x + 1] == 1)
                gm[y][x].SetPrevNode(gm[y - 1][x + 1], 2);

        }
        catch (System.Exception e)
        {
            Debug.Log("PrevNodes: ");
            Debug.Log(y + "-" + x);
            throw e;
        }
    }

    //No me gusta esto, ver otra forma porfa =(
    private void LinkIniFin()
    {
        int f = 0, n = 0;
        for (int i = 0; i < hNiveles_; i++)
        {            
            if (logicMatrix[0][i] == 1)
            {
                ini_.SetNextNode(gm[0][i], n);
                n++;
            }

            if (logicMatrix[vNiveles_ - 1][i] == 1)
            {
                fin_.SetPrevNode(gm[vNiveles_ - 1][i], f);
                f++;
            }
        }
    }

    #endregion
}
