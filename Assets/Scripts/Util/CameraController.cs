using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// modes
// 0 - free fly
// 1 - follow top down
// 2 - follow 3rd Person

public class CameraController : MonoBehaviour
{
    int modeId;
    bool isVR;
    RobotMaster rm;
    [SerializeField] GameObject cam;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        rm = GetComponent<RobotMaster>();
        isVR = rm.isVR;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch(modeId)
        {
            case 0:
                if(!isVR)
                {
                    setFreeFly(true);
                    cam.GetComponent<FreeFlyCamera>().setEnabelMovement(true);
                }
                if(rm.teleoperationBot != null)
                {
                    //  setFreeFly(false);
                    cam.GetComponent<FreeFlyCamera>().setEnabelMovement(false);
                    // cam.transform.LookAt(rm.teleoperationBot.transform.position);
                }
                break;
            case 1:
                setFreeFly(false);
                if (rm.teleoperationBot != null)
                    followAbove(rm.teleoperationBot);
                else
                    modeId = 0;
                break;
            case 2:
                followSelected(rm.teleoperationBot);
                break;
        }
    }

    void setFreeFly(bool enableFreeFly)
    {
        cam.GetComponent<FreeFlyCamera>().enabled = enableFreeFly;
    }

    private Vector3 velocity = Vector3.zero;
    public float m_speedCameraRot = 0.5f;
    void followSelected(Robot selectedBot)
    {
        if(selectedBot == null)
            return;
        
        // TODO: wtf is this? find a way to correctly convert offset from global to local
        Vector3 target = selectedBot.transform.position + selectedBot.transform.forward * offset.z + selectedBot.transform.up * offset.y + selectedBot.transform.right * offset.x;
        setCamPos(target);
        cam.transform.LookAt(selectedBot.transform.position);
    }
    // TODO: camera rotates when flying sideways 
    void followAbove(Robot selectedBot)
    {
        if(selectedBot == null)
            return;
        setFreeFly(false);
        Vector3 target = selectedBot.transform.position + new Vector3(0, 2f, -0.01f);
        setCamPos(target);
        cam.transform.rotation = Quaternion.LookRotation(Vector3.down);

    }

    void setCamPos(Vector3 target)
    {
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, target, ref velocity, smoothTime);
        Vector3 lookDirection = target - cam.transform.position;
    }

    public void setMode(int id)
    {
        modeId = id;
    }
    public int getMode()
    {
        return modeId;
    }

    public Robot getHitRobot()
    {
        RaycastHit hit;
        Robot targetedRobot = null;
        if (Physics.Raycast(cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit)) 
        {
            targetedRobot = hit.collider.gameObject.GetComponent<Robot>();
        }

        return targetedRobot;
    }

    public GameObject getHitObject()
    {
        RaycastHit hit;
        GameObject targetedRobot = null;
        if (Physics.Raycast(cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit)) 
        {
            targetedRobot = hit.collider.gameObject;
        }

        return targetedRobot;
    }

    

}
