using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowStatisticController : MonoBehaviour
{    
    private List<(int,float)> _speedList = new List<(int,float)>();
    private List<(int,Vector3)> _angleList = new List<(int,Vector3)>();

    //Add throw speed.
    public void AddSpeed(float speed,int ball){

        _speedList.Add((ball,speed));
    }

    //Add throw angle.
    public void AddAngle(Vector3 angle,int ball){

        _angleList.Add((ball,angle));
    }

    //Get throw speed list.
    public List<(int,float)> GetSpeeds(){
        return _speedList;
    }

    //Get throw angle list.
    public List<(int,Vector3)> GetAngles(){
        return _angleList;
    }
}
