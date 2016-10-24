//------------------------------------------------------------------------------
// Copyright 2016 Baofeng Mojing Inc. All rights reserved.
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class SetOverlay : MonoBehaviour {

    Texture tex;
    int texID = 0;
    Camera LCamera;
    Camera RCamera;
    GameObject CenterPointer;
    void Start()
    {
        tex = Resources.Load("star") as Texture;
        LCamera = GameObject.Find("MojingMain/MojingVrHead/VR Camera Left").GetComponent<Camera>();
        RCamera = GameObject.Find("MojingMain/MojingVrHead/VR Camera Right").GetComponent<Camera>();
        CenterPointer = GameObject.Find("MojingMain/MojingVrHead/GazePointer");
        texID = (int)tex.GetNativeTexturePtr();
    }

    void Update()
    {
        DrawOverlay();
    }

    // If TW, ATW or needDistortion enable, render by MojingSDK, Call MojingSDK.Unity_SetOverlay
    void DrawOverlay()
    {
        if (tex)
        {
            if (ConfigItem.TW_STATE || Mojing.SDK.NeedDistortion)
            {
                //MojingSDK.Unity_SetOverlay3D(3, texID, 0.04f, 0.04f, CenterPointer.transform.position.magnitude);
                MojingSDK.Unity_SetOverlay3D(1, texID, 0.04f, 0.04f, Vector3.Distance(LCamera.transform.position,CenterPointer.transform.position));
                MojingSDK.Unity_SetOverlay3D(2, texID, 0.04f, 0.04f, Vector3.Distance(RCamera.transform.position, CenterPointer.transform.position));
                /*------
                 iEyeType:1----Left camera viewport draw
                          2----Right camera viewport draw
                          3---- Both left camera and right camera viewports draw
                 ------*/
            }
        }
        else
            Debug.Log("There is no Texture!");
    }
    
    //If TW, ATW and needDistortion are disable,  not render by MojingSDK, Call GUI.DrawTexture
    void OnGUI()
    {
        if (tex)
        {
#if UNITY_EDITOR
            if (Mojing.SDK.VRModeEnabled)
            {
                GUI.DrawTexture(new Rect(LCamera.WorldToScreenPoint(CenterPointer.transform.position).x - 25, LCamera.WorldToScreenPoint(CenterPointer.transform.position).y - 25, 50, 50), tex);
                GUI.DrawTexture(new Rect(RCamera.WorldToScreenPoint(CenterPointer.transform.position).x - 25, RCamera.WorldToScreenPoint(CenterPointer.transform.position).y - 25, 50, 50), tex);
            }
            else
                GUI.DrawTexture(new Rect(0.5f * Screen.width - 25, 0.5f * Screen.height - 25, 50, 50), tex);
#else
            if (!ConfigItem.TW_STATE && !Mojing.SDK.NeedDistortion && Mojing.SDK.VRModeEnabled)
            {
                GUI.DrawTexture(new Rect(LCamera.WorldToScreenPoint(CenterPointer.transform.position).x - 25, LCamera.WorldToScreenPoint(CenterPointer.transform.position).y - 25, 50, 50), tex);
                GUI.DrawTexture(new Rect(RCamera.WorldToScreenPoint(CenterPointer.transform.position).x - 25, RCamera.WorldToScreenPoint(CenterPointer.transform.position).y - 25, 50, 50), tex);
            }
            else if (!Mojing.SDK.VRModeEnabled)
            {
                GUI.DrawTexture(new Rect(0.5f * Screen.width - 25, 0.5f * Screen.height - 25, 50, 50), tex);
            }
#endif
        }
        else
            Debug.Log("There is no Texture!");
    }

    void OnDestroy()
    {
        MojingSDK.Unity_SetOverlay3D(3, 0, 1, 1, 1);
    }
}
