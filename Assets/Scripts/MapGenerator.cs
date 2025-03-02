using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject BrickPref;
    public GameObject Destination;
    public GameObject ObstaclePref;

    private Vector3 _startPos;
    private Vector3 _startPosPlayer;
    private Vector3 _finalPos;
    private int _sizeBrick = 2;

    private List<Transform> _childs;
    private Dictionary<Transform, List<float>> _posObstacle;
    public void InitMap(LevelConfig levelConfig)
    {
        if(this._childs == null)
            this._childs = new List<Transform>();

        this.ClearMap();
        this._childs.Clear();
        this.transform.position = Vector3.zero;
        this._startPos = new Vector3(-2f, 0, -15f);
        for (int i = 0; i < levelConfig.Length; i++)
        {
            for (int j = 0; j < levelConfig.Width; j++)
            {
                string name = string.Format("{0}_{1}", j, i);
                if (levelConfig.Space != null && levelConfig.Space.Contains(name))
                    continue;
                Vector3 pos = this._startPos + new Vector3(this._sizeBrick * j, 0, this._sizeBrick * i);
                GameObject obj = Instantiate(this.BrickPref, this.transform);
                obj.name = name;
                obj.transform.position = pos;

                if (levelConfig.Plants.ContainsKey(name))
                {
                    GameObject obsPref = ResourceManager.Instance.GetObstacle(levelConfig.Plants[name]);
                    if (obsPref != null)
                    {
                        GameObject obsIns = Instantiate(obsPref, obj.transform);
                        obsIns.transform.localPosition = new Vector3(0, 2f, 0);
                    }
                }
                this._childs.Add(obj.transform);
                if (i == levelConfig.Length - 1 && j == levelConfig.Width - 1)
                    this._finalPos = pos;
                if(i == 1 && j == 1) 
                    this._startPosPlayer = pos;
            }
        }

        GameObject houseObj = Instantiate(this.Destination, this.transform);
        houseObj.transform.position = new Vector3(0, houseObj.transform.position.y, this._finalPos.z);
        this.SetObstacle(levelConfig);
        this.transform.position = new Vector3(0, -2f, 0);
    }

    public void ClearMap()
    {
        int k = 0;
        while(this.transform.childCount > 0)
        {
            k++;
            if (k == 100)
                return;

            Transform child = transform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    public void SetObstacle(LevelConfig levelConfig)
    {
        if (this._posObstacle == null)
            this._posObstacle = new Dictionary<Transform, List<float>>();
        this._posObstacle.Clear();
        foreach(var item in levelConfig.Obstacle.Keys)
        {
            GameObject objIns = Instantiate(this.ObstaclePref, this.transform);
            objIns.name = string.Format("{0}[{1}]", "Obstancle", item);
            this._posObstacle[objIns.transform] = new List<float>();
            for(int i = 0; i < this._childs.Count; i++)
            {
                Transform child = this._childs[i];
                if (levelConfig.Obstacle[item].Contains(child.name))
                {
                    child.SetParent(objIns.transform);
                    if (!this._posObstacle[objIns.transform].Contains(child.position.z))
                        this._posObstacle[objIns.transform].Add(child.position.z);
                }
            }
        }
    }

    public Vector3 GetStartPosPlayer() => this._startPosPlayer;

    public Vector3 GetFinalPos() => this._finalPos;

    public bool CheckObstacle(float z)
    {
        foreach(var item in this._posObstacle.Keys)
        {
            if(z >= this._posObstacle[item][0] - 0.5f && z < this._posObstacle[item][1])
            {
                Vector3 newPos = new Vector3(item.position.x, -7.5f, item.position.z);
                item.position =  Vector3.Lerp(item.position, newPos, 10f * Time.deltaTime);
                return true;
            }
        }
        return false;
    }
}
