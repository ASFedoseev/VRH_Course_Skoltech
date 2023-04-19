using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crazyflie : Robot
{
    Rigidbody rb;
    QuadrotorController qc;
    bool isArmed = true;

    public override void Start()
    {
        base.Start();
        qc = GetComponent<QuadrotorController>();
    }

    public override void Update()
    {
        base.Update();
        switch(modeId)
        {
            case 0:
                idle();
                break;
            case 1:
                teleoperationMode();
                break;
            case 2:
                //GetComponent<RLVelController>().enabled = true;
                doCurrentTask();
                break;
                

        }

    }

    void arm()
    {
        isArmed = true;
        GetComponent<QuadrotorController>().enabled = true;
    }

    void disarm()
    {
        isArmed = false;
        GetComponent<QuadrotorController>().enabled = false;
    }

    void idle()
    {
        //GetComponent<RLVelController>().enabled = false;
        qc.applyControl(Vector3.zero, 0);
    }

    void teleoperationMode()
    {
        if(!isArmed)
        {
            arm();
        }
        Vector3 controlVel = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
        {
            controlVel += Vector3.forward;
        }
        if(Input.GetKey(KeyCode.S))
        {
            controlVel += -Vector3.forward;
        }
        if(Input.GetKey(KeyCode.A))
        {
            controlVel += Vector3.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
            controlVel += -Vector3.left;
        }
        if(Input.GetKey(KeyCode.I))
        {
            controlVel += Vector3.up;
        }
        if(Input.GetKey(KeyCode.K))
        {
            controlVel += Vector3.down;
        }


        float rotateVel = 0;

        if(Input.GetKey(KeyCode.Q))
        {
            rotateVel = -0.5f;
        }
        if(Input.GetKey(KeyCode.E))
        {
            rotateVel = 0.5f;
        }
        Debug.Log(controlVel);
        qc.applyControl(controlVel.normalized, rotateVel);
    }


   
    public override Task interact(Robot interactRobot)
    {
        throw new System.NotImplementedException();
    }

    public override bool doCurrentTask()
    {
        LandTask convertedTask = (LandTask)(getCurrentTask());
        //GetComponent<RLVelController>().setTarget(convertedTask.landPose);

        

        if((convertedTask.landPose.transform.position - transform.position).magnitude <= 0.1)
        {
            disarm();
            modeId = 0;
            return true;
        }
        return false;
    }
}
