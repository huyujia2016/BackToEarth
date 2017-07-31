using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public int currentLevel;
    public int totallyenergy;
    public int ss;
    // Update is called once per frame
    void Update () {
        currentLevel = DataSet.Instance().CurrentLevel;
        ss= PlayerPrefs.GetInt("CurrentLevel");;
        totallyenergy = DataSet.Instance().TotalEnergyPoint;
    }
}
