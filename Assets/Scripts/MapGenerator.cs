using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject BrickPref;
    public GameObject Destination;

    private Vector3 _startPos;
    private Vector3 _startPosPlayer;
    private Vector3 _finalPos;
    /*private int _length = 10;
    private int _width = 3;*/
    private int _sizeBrick = 2;
    public void InitMap(LevelConfig levelConfig)
    {
        this.ClearMap();
        this.transform.position = Vector3.zero;
        this._startPos = new Vector3(-2f, 0, -15f);
        for (int i = 0; i < levelConfig.Length; i++)
        {
            for (int j = 0; j < levelConfig.Width; j++)
            {
                Vector3 pos = this._startPos + new Vector3(this._sizeBrick * j, 0, this._sizeBrick * i);
                GameObject obj = Instantiate(this.BrickPref, this.transform);
                string name = string.Format("{0}_{1}", j, i);
                obj.name = name;
                obj.transform.position = pos;

                if (levelConfig.Obstancles.ContainsKey(name))
                {
                    GameObject obsPref = ResourceManager.Instance.GetObstacle(levelConfig.Obstancles[name]);
                    if (obsPref != null)
                    {
                        GameObject obsIns = Instantiate(obsPref, obj.transform);
                        obsIns.transform.localPosition = new Vector3(0, 2f, 0);
                    }
                }

                if (i == levelConfig.Length - 1 && j == levelConfig.Width - 1)
                    this._finalPos = pos;
                if(i == 1 && j == 1) 
                    this._startPosPlayer = pos;
            }
        }

        GameObject houseObj = Instantiate(this.Destination, this.transform);
        houseObj.transform.position = new Vector3(0, houseObj.transform.position.y, this._finalPos.z);
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

    public Vector3 GetStartPosPlayer() => this._startPosPlayer;

    public Vector3 GetFinalPos() => this._finalPos;
}
