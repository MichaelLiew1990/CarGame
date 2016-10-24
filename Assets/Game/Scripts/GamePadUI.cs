using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityStandardAssets.Vehicles.Car;


//现在是服务端与客户端公用，以后可以考虑分开
public class GamePadUI : MonoBehaviour
{
    public RawImage imgJoystickCenter;
    public RawImage imgA;
    public RawImage imgB;
    public RawImage imgX;
    public RawImage imgY;
    public CarUserControl clientCar;

    private ClientNetworkMgr net;

    void Start()
    {
        net = NetworkManager.singleton as ClientNetworkMgr;
        if (net == null)
        {
            Debug.LogError("Net is Null");
        }
    }

    void Update()
    {
        ResetAllImage();
        if (net.GetNetPlayer() != null)
        {
            float h = net.GetNetPlayer().sync_H;
            float v = net.GetNetPlayer().sync_V;
            imgJoystickCenter.rectTransform.localPosition = new Vector3(h * 27f, v * 27f, 0);
            clientCar.MoveCar(h, v);
        }

        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            SetButtonDownColor(imgA);
        }

        if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            SetButtonDownColor(imgB);
        }

        if (Input.GetKey(KeyCode.Joystick1Button2))
        {
            SetButtonDownColor(imgX);
        }

        if (Input.GetKey(KeyCode.Joystick1Button3))
        {
            SetButtonDownColor(imgY);
        }
    }

    void FixedUpdate()
    {
        if (net.GetNetPlayer() != null)
        {
            net.GetNetPlayer().CmdUpdateCarPose(clientCar.transform.rotation, new Vector3(0,2,0), net.localIP); 
        }
    }

    void SetButtonDownColor(RawImage img)
    {
        img.color = Color.red;
    }

    void ResetAllImage()
    {
        imgJoystickCenter.rectTransform.localPosition = new Vector3(0, 0, 0);
        imgA.color = Color.white;
        imgB.color = Color.white;
        imgX.color = Color.white;
        imgY.color = Color.white;
    }
}
