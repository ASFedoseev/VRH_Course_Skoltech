using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Robot mode
// 0 - idle
// 1 - teleoperation control
// 2 - 

public abstract class Robot : MonoBehaviour
{

    private Task currentTask;
    private bool isSelected;
    public int modeId = 0;
    [SerializeField] GameObject setSelectSphere;
    //ros utils
    [SerializeField] protected bool useRos;
    [SerializeField] int nodeFreq=10;
    GameObject selectObj;
    // Start is called before the first frame update
    public virtual void Start()
    {
        isSelected = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {
    
    
    }

    public abstract Task interact(Robot interactRobot);
    public abstract bool doCurrentTask();

    public void setSelect(bool toggle)
    {
        isSelected = toggle;
        if(toggle)
        {
            selectObj = Instantiate(setSelectSphere, new Vector3(transform.position.x,transform.position.y, transform.position.z), Quaternion.identity);
            selectObj.transform.parent = gameObject.transform;
        }
        else
        {
            if(selectObj != null)
            {
                Destroy(selectObj);
                selectObj = null;
            }
        }
    }

    public bool getSelect()
    {
        return isSelected;
    }

    public void setCurrentTask(Task newTask)
    {
        currentTask = newTask;
    }

    public Task getCurrentTask()
    {
        return currentTask;
    }

    
}
