using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject BrickPref;
    public GameObject Destination;

    private Vector3 _startPos;
    private Vector3 _finalPos;
    private int _length = 10;
    private int _width = 3;
    private int _sizeBrick = 2;
    public void InitMap(LevelConfig levelConfig)
    {
        this._startPos = new Vector3(-2f, 0, -15f);
        for (int i = 0; i < levelConfig.Length; i++)
        {
            for (int j = 0; j < levelConfig.Width; j++)
            {
                Vector3 pos = this._startPos + new Vector3(this._sizeBrick * j, 0, this._sizeBrick * i);
                GameObject obj = Instantiate(this.BrickPref, this.transform);
                obj.name = string.Format("{0}_{1}", j, i);
                obj.transform.position = pos;
                if (i == levelConfig.Length - 1 && j == levelConfig.Width - 1)
                    this._finalPos = pos;
            }
        }

        GameObject houseObj = Instantiate(this.Destination, this.transform);
        houseObj.transform.position = new Vector3(0, houseObj.transform.position.y, this._finalPos.z);
        this.transform.position = new Vector3(0, -2f, 0);
    }

    public Vector3 GetStartPosMap()
    {
        return this._startPos;
    }
}
