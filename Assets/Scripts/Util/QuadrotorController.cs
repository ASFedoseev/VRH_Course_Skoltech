using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrotorController : MonoBehaviour
{
    [Foldout("Propeller Game objects", true)]
    [SerializeField] Transform fr_prop;
    [SerializeField] Transform fl_prop;
    [SerializeField] Transform br_prop;
    [SerializeField] Transform bl_prop;
    
    [Foldout("Internal phisics attributes", true)]
    [SerializeField] float hoverforce;
    [SerializeField] float maxPropellerForce;
    [SerializeField] float forceSetTime = 0.3f;

    [Foldout("Attitude rate PIDs", true)]
    [SerializeField] Vector3 PID_pitch_rate_gains; 
    [SerializeField] Vector3 PID_roll_rate_gains; 
    [SerializeField] Vector3 PID_yaw_rate_gains; 

    [Foldout("Attitude PIDs", true)]
    [SerializeField] Vector3 PID_pitch_gains; 
    [SerializeField] Vector3 PID_roll_gains; 
    [SerializeField] Vector3 PID_yaw_gains; 

    [Foldout("Velocity PIDs", true)]
    [SerializeField] Vector3 PID_vel_x_gains;
    [SerializeField] Vector3 PID_vel_y_gains;
    [SerializeField] Vector3 PID_vel_z_gains;

    [Foldout("External forces", true)]
    [SerializeField] float windForce;
    [SerializeField] float forceDir;


    //The PID controllers
    private PIDController PID_pitch_rate;
    private PIDController PID_roll_rate;
    private PIDController PID_yaw_rate;

    private PIDController PID_pitch;
    private PIDController PID_roll;
    private PIDController PID_yaw;

    private PIDController PID_vel_x;
    private PIDController PID_vel_y;
    private PIDController PID_vel_z;
    
    Vector3 targetVel;
    float yawRate;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PID_pitch_rate = new PIDController();
        PID_roll_rate = new PIDController();
        PID_yaw_rate = new PIDController();


        PID_pitch = new PIDController();
        PID_roll = new PIDController();
        PID_yaw = new PIDController();

        PID_vel_x = new PIDController();
        PID_vel_y = new PIDController();
        PID_vel_z = new PIDController();


        actualPropForceFR = hoverforce / 4;
        actualPropForceFL = hoverforce / 4;
        actualPropForceBR = hoverforce / 4;
        actualPropForceBL = hoverforce / 4;    
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 anglesandThrottle = applyVelControl(targetVel);
        Vector3 angles = new Vector3(anglesandThrottle.x, 0, anglesandThrottle.y);
        float throttle = anglesandThrottle.z;
        Vector3 angleVels = applyAttitudeControl(angles) + new Vector3(0, yawRate, 0);
        applyAttitudeRateControl(angleVels, throttle);
        // AddExternalForces();

    }
    public void applyControl(Vector3 vel, float yawRate)
    {
        targetVel = vel;
        this.yawRate = yawRate;
    }
    Vector3 applyVelControl(Vector3 vel)
    {
        Vector3 velError = transform.InverseTransformDirection(vel) - transform.InverseTransformDirection(rb.velocity); 
        float PIDPitchOut = PID_vel_z.GetFactorFromPIDController(PID_vel_z_gains, velError.z);
        float PIDRollOut = PID_vel_x.GetFactorFromPIDController(PID_vel_x_gains, velError.x);


        PIDPitchOut = Mathf.Clamp(PIDPitchOut, -30f, 30f);
        PIDRollOut = Mathf.Clamp(PIDRollOut, -30f, 30f);


        float PIDThrottleOut = PID_vel_y.GetFactorFromPIDController(PID_vel_y_gains, velError.y);
        return new Vector3(PIDPitchOut, -PIDRollOut, PIDThrottleOut);
    }
    Vector3 applyAttitudeControl(Vector3 targetAttitude)
    {
        Vector3 anglesInEuler = transform.rotation.eulerAngles;
        Vector3 angError = targetAttitude - anglesInEuler;
        float PIDPitchRateOut = PID_pitch.GetFactorFromPIDController(PID_pitch_gains, WrapAngle(angError.x));
        float PIDRollRateOut = PID_roll.GetFactorFromPIDController(PID_roll_gains, WrapAngle(angError.z));
        float PIDYawRateOut = PID_yaw.GetFactorFromPIDController(PID_yaw_gains, WrapAngle(angError.y));
        return new Vector3(PIDPitchRateOut, PIDYawRateOut, PIDRollRateOut);
    }

    float WrapAngle(float x)
    {
        x = x + 180%360;
        if (x < 0)
            x += 360;
        return x - 180;
    }

    void applyAttitudeRateControl(Vector3 attitudeRate, float throttle)
    {

        Vector3 localAngularVelocity = transform.InverseTransformDirection(rb.angularVelocity);

        Vector3 attitudeError = attitudeRate - localAngularVelocity;
        float PIDPitchRateOut = PID_pitch_rate.GetFactorFromPIDController(PID_pitch_rate_gains, attitudeError.x);
        float PIDRollRateOut = PID_roll_rate.GetFactorFromPIDController(PID_roll_rate_gains, attitudeError.z);
        float PIDYawRateOut = PID_yaw_rate.GetFactorFromPIDController(PID_yaw_rate_gains, attitudeError.y);
        float singlePropThrottle = hoverforce / 4;

        float propForceFR = singlePropThrottle + (-PIDPitchRateOut + PIDRollRateOut + PIDYawRateOut) + throttle;
        float propForceFL = singlePropThrottle + (-PIDPitchRateOut - PIDRollRateOut - PIDYawRateOut) + throttle;
        float propForceBR = singlePropThrottle + (PIDPitchRateOut + PIDRollRateOut - PIDYawRateOut) + throttle;
        float propForceBL = singlePropThrottle + (PIDPitchRateOut - PIDRollRateOut + PIDYawRateOut) + throttle;

        propForceFR = Mathf.Clamp(propForceFR, 0f, maxPropellerForce);
        propForceFL = Mathf.Clamp(propForceFL, 0f, maxPropellerForce);
        propForceBR = Mathf.Clamp(propForceBR, 0f, maxPropellerForce);
        propForceBL = Mathf.Clamp(propForceBL, 0f, maxPropellerForce);


        addPropForce(propForceFR, propForceFL, propForceBR, propForceBL);

    }

    float actualPropForceFR;
    float actualPropForceFL;
    float actualPropForceBR;
    float actualPropForceBL;    
    float propForceVelFR = 0.0f;
    float propForceVelFL = 0.0f;
    float propForceVelBR = 0.0f;
    float propForceVelBL = 0.0f;
    void addPropForce(float frForce, float flForce, float brForce, float blForce)
    {
        actualPropForceFR = Mathf.SmoothDamp(actualPropForceFR, frForce, ref propForceVelFR, forceSetTime);
        actualPropForceFL = Mathf.SmoothDamp(actualPropForceFL, flForce, ref propForceVelFL, forceSetTime);
        actualPropForceBR = Mathf.SmoothDamp(actualPropForceBR, brForce, ref propForceVelBR, forceSetTime);
        actualPropForceBL = Mathf.SmoothDamp(actualPropForceBL, blForce, ref propForceVelBL, forceSetTime);

        rb.AddForceAtPosition(fr_prop.up * actualPropForceFR, fr_prop.position);
        rb.AddForceAtPosition(fl_prop.up * actualPropForceFL, fl_prop.position);
        rb.AddForceAtPosition(br_prop.up * actualPropForceBR, br_prop.position);
        rb.AddForceAtPosition(bl_prop.up * actualPropForceBL, bl_prop.position);

        rb.AddForceAtPosition(fl_prop.right * actualPropForceFL, fl_prop.position);
        rb.AddForceAtPosition(fr_prop.right * actualPropForceFR, fr_prop.position);
        rb.AddForceAtPosition(bl_prop.right * actualPropForceBL, bl_prop.position);
        rb.AddForceAtPosition(br_prop.right * actualPropForceBR, br_prop.position);

        Debug.DrawRay(fr_prop.position, fr_prop.up * actualPropForceFR * 2, Color.green);
        Debug.DrawRay(fl_prop.position, fl_prop.up * actualPropForceFL * 2, Color.green);
        Debug.DrawRay(br_prop.position, br_prop.up * actualPropForceBR * 2, Color.green);
        Debug.DrawRay(bl_prop.position, bl_prop.up * actualPropForceBL * 2, Color.green);

        Debug.DrawRay(fr_prop.position, fr_prop.right * actualPropForceFR, Color.red);
        Debug.DrawRay(fl_prop.position, fl_prop.right * actualPropForceFL, Color.red);
        Debug.DrawRay(br_prop.position, br_prop.right * actualPropForceBR, Color.red);
        Debug.DrawRay(bl_prop.position, bl_prop.right * actualPropForceBL, Color.red);
    }



    //Add external forces to the quadcopter, such as wind
    private void AddExternalForces()
    {
        //Important to not use the quadcopters forward
        Vector3 windDir = -Vector3.forward;

        //Rotate it 
        windDir = Quaternion.Euler(0, forceDir, 0) * windDir;

        rb.AddForce(windDir * windForce);

        //Debug
        //Is showing in which direction the wind is coming from
        //center of quadcopter is where it ends and is blowing in the direction of the line
        // Debug.DrawRay(transform.position, -windDir * 3f, Color.red);
    }

}
