using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MobileShooter : MonoBehaviour {
    public Button ShootFrontButton;
    public GameObject ARCamera; // Set within Unity

    private GameObject phoneCube, phoneCube2;
    private TargetBehavior targetBehavior;

    bool started = false;
    float swipespeed_min = 1.0f;
    float swipespeed_max = 100.0f;

    Vector3 mousedown_pos;
    float mousedowned_time;

    bool bMouseDown = false;
    float ballSpeed = 25.0f;

    private void Start()
    {
        targetBehavior = FindObjectOfType<TargetBehavior>();
		ShootFrontButton.enabled = false;
    }

    public void Activate()
    {
        ShootFrontButton.enabled = true;
        ShootFrontButton.onClick.AddListener(ShootBallFront);
        started = true;

        phoneCube = PhotonNetwork.Instantiate("PhoneCubeTransparent", new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, 0);
    }

    // shoot ball on swipe
    private void Update()
    {
        if (!started) return;

        phoneCube.transform.position = targetBehavior.GetPhonePosition();
        phoneCube.transform.rotation = targetBehavior.GetPhoneRotation();

        if (bMouseDown)
        {
            mousedowned_time += Time.deltaTime;
        }

        if (!bMouseDown && Input.GetMouseButtonDown(0))
        {
            mousedown_pos = Input.mousePosition;
            mousedowned_time = 0;
            bMouseDown = true;
        }


        if (Input.GetMouseButtonUp(0))
        {
            if (!bMouseDown || mousedowned_time <= 0.05f) return;

            Vector3 mouseup_pos = Input.mousePosition;
            Vector3 delta = (mouseup_pos - mousedown_pos) / Screen.height;
            Vector3 swipe_vel = delta / mousedowned_time;

            if (swipe_vel.y > swipespeed_min)
            {
                ShootBallUp(System.Math.Min(swipespeed_max, swipe_vel.y * 10.0f));
            }

            bMouseDown = false;
            mousedowned_time = 0;
        }
    }

    public void ShootBall(Vector3 velocity)
    {
        GetComponent<AudioSource>().Play();

        // You may want to use a random nice color so there is one!
        Color color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f, 1f, 1f);
        Vector3 color_v = new Vector3(color.r, color.g, color.b);

        // TODO-2.c PhotonNetwork.Instantiate to shoot a ball!
        // You may want to initialize a RPC function call to RPCInitialize() 
        //   (See BallBehavior.cs) to set the velocity and color
        //   of the ball across all clients (PhotonTargets.All) and transfer 
        //   the ownership of the ball to PC so the ball is correctly destroyed
        //   upon hitting a wall.

        GameObject ball = PhotonNetwork.Instantiate("Ball", ARCamera.transform.position, Quaternion.identity, 0);

        PhotonView photonView = PhotonView.Get(ball);

        photonView.RPC("RPCInitialize", PhotonTargets.All, velocity, color_v);

    }

    public void ShootBallFront()
    {
        ShootBall(ballSpeed * targetBehavior.GetPhoneForward());
    }

    public void ShootBallUp(float speed)
    {
        ShootBall(speed * targetBehavior.GetPhoneUp());
    }
}
