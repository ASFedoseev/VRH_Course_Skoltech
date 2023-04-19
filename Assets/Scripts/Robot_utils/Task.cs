using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Task
{
    public Task()
    {

    }
    public virtual void addData(KeyValuePair<float, Vector3> data){}
    public virtual void draw(){}
    public virtual void show(){}
    public virtual void erase(){}

}


public class LandTask : Task
{
    public PlatformObjects landPose;
    Robot[] robotList;
    public LandTask(PlatformObjects landPose, Robot[] robotList)
    {
        this.landPose = landPose;
        this.robotList = robotList;
    }
}
