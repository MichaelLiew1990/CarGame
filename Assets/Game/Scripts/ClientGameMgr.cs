using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ClientGameMgr : MonoBehaviour
{
    private static ClientGameMgr inst = null;
    public static ClientGameMgr Instance
    {
        get
        {
            if (GameObject.Find("GameManager"))
            {
                inst = GameObject.Find("GameManager").GetComponent<ClientGameMgr>();
            }
            else
            {
                GameObject o = new GameObject();
                o.name = "GameManager";
                inst = o.AddComponent<ClientGameMgr>();
            }
            return inst;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
