using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMaster : MonoBehaviour
{
    [SerializeField] Robot[] robotList;
    public bool isVR = false;
    public bool isTeleop = false;
    public Robot teleoperationBot;
    int amountOfRobotsSelected = 0;
    int currentViewMode = 0;
    int maxViewModes = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            toggleSelectOnClick();
        }
        if((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Alpha0)))
        {
            isTeleop = false;
            resetSelected();
        }
        if(amountOfRobotsSelected == 1 && Input.GetKeyDown(KeyCode.Alpha1))
        {
            isTeleop = true;
            setTeleoperationMode();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject landRobot = GetComponent<CameraController>().getHitObject();
            LandableObject landableObject = landRobot.GetComponent<LandableObject>();
            if(landableObject != null)
            {
                PlatformObjects[] landingPlatforms = landableObject.platformPoses;
                giveLandingTask(landingPlatforms);
            }

        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            toggelView();
        }
    }


    void giveLandingTask(PlatformObjects[] platforms)
    {
        int platformStartId = 0;
        foreach(Robot robot in robotList)
        {
            if(robot.getSelect())
            {
                for(int i = platformStartId; i < platforms.Length; i++)
                {
                    if(!platforms[i].getOccupation())
                    {
                        robot.setCurrentTask(new LandTask(platforms[i], robotList));
                        robot.modeId = 2;
                        platformStartId++;
                        break;
                    }
                }

            }
        }
    }

    void toggleSelectOnClick()
    {
        Robot targetedRobot =  GetComponent<CameraController>().getHitRobot();
        if(targetedRobot != null)
        {
            setSelect(targetedRobot, !targetedRobot.getSelect());
        }
    }

    void setSelect(Robot robot, bool selectState)
    {
        robot.setSelect(selectState);
        if (selectState)
            amountOfRobotsSelected++;
        else
            amountOfRobotsSelected--;
    }

    void setTeleoperationMode()
    {
        // GetComponent<CameraController>().setMode(1); // 1 -- follow 3rd person
        foreach(Robot robot in robotList)
        {
            if(robot.getSelect())
            {
                robot.modeId = 1;
                teleoperationBot = robot;
                // setSelect(robot,  false);
                break;
            }
        }
    }


    void resetSelected()
    {
        currentViewMode = 0;
        GetComponent<CameraController>().setMode(currentViewMode); // freefly
        foreach(Robot robot in robotList)
        {
            teleoperationBot = null;
            if(robot.getSelect())
            {
                robot.modeId = 0;
                setSelect(robot, false);
            }
        }
    }

    void toggelView()
    {
        currentViewMode++;
        if(currentViewMode >= maxViewModes)
        {
            currentViewMode = 0;
        }
        GetComponent<CameraController>().setMode(currentViewMode);
    }

}
