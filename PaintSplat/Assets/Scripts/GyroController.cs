using UnityEngine;
using System.Collections;

public class GyroController : MonoBehaviour
{
    public GameObject ControlledObject
    {
        get { return controlledObject; }
        set
        {
            controlledObject = value;
            ResetOrientation();
        }
    }

    public bool Paused { get; set; }

    Quaternion qRefObject = Quaternion.identity;
    Quaternion qRefGyro = Quaternion.identity; // The quat in Unity's coordinate space, left-handed
	Quaternion qRefGyroRight = Quaternion.identity; // The quat in the right-handed space of the Android phone
    Gyroscope gyro;

    GameObject controlledObject;

    private void Awake()
    {
        Paused = false;
    }

    private void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
        gyro.updateInterval = 0.01f;
    }

    private void OnGUI()
    {
        GUILayout.Label("                                ");
        GUILayout.Label("Gyroscope attitude : " + gyro.attitude);
        GUILayout.Label("Gyroscope gravity : " + gyro.gravity);
        GUILayout.Label("Gyroscope rotationRate : " + gyro.rotationRate);
        GUILayout.Label("Gyroscope rotationRateUnbiased : " + gyro.rotationRateUnbiased);
        GUILayout.Label("Gyroscope updateInterval : " + gyro.updateInterval);
        GUILayout.Label("Gyroscope userAcceleration : " + gyro.userAcceleration);
        GUILayout.Label("Ref camera rotation:" + qRefObject);
        GUILayout.Label("Ref gyro attitude:" + qRefGyro);
    }

    // LOOK-1.d:
    // Converts the data returned from gyro from right-handed base to left-handed base.
    // Your device may require a different conversion
    private static Quaternion ConvertRotation(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private void Update()
    {
        if (controlledObject != null && !Paused)
        {
            //   Rotate the camera or cube based on qRefObject, qRefGyro and current 
            //   data from gyroscope


            // PhoneCube is the controlledOject 
            // q0 = qRefObject
            // qref = The phone's initial quat
            // qgyro = Input.Gyro.attitude Note: in right-handed coordinate system
            // deltaQgyro = Inverse(qref) * qgyro
            Quaternion deltaQgyro = Quaternion.Inverse(qRefGyroRight) * Input.gyro.attitude;

            // Convert from right-handed to left-handed coordinate system
            Quaternion deltaQgyroLeftHanded = ConvertRotation(deltaQgyro);

            // qObject = q0 * deltaQgyroLeftHanded;
            controlledObject.transform.rotation = qRefObject * deltaQgyroLeftHanded;

        }
    }

    public void ResetOrientation()
    {
        if (controlledObject == null)
        {
            return;
        }
        qRefObject = controlledObject.transform.rotation;
        qRefGyro = ConvertRotation(Input.gyro.attitude);
		qRefGyroRight = Input.gyro.attitude;
    }

    //Helper function to smooth between gyro and Vuforia
    public void UpdateOrientation(float deltatime)
    {
             float smooth = 1f;
             qRefCam = Quaternion.Slerp(qRefCam, transform.rotation, smooth * deltatime);
             qRefGyro = Quaternion.Slerp(qRefGyro, ConvertRotation(gyro.attitude), smooth * deltatime);
         }
    }
}
