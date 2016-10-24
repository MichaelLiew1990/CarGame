using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class NetPlayer : NetworkBehaviour
{
    [SyncVar]
    //[HideInInspector]
    public float sync_H;//服务器更新客户端读取
    [SyncVar]
    //[HideInInspector]
    public float sync_V;//服务器更新客户端读取


    //下面的不成功，因为外面获取hostIP值时LocalPlayer并不一定有值，有可能跟服务端调用哪个NetPlayer组件发送消息有关
    //public string hostIP;//用于发送姿态的主客户端（IP后三位）
    //[SyncVar]
    ////[HideInInspector]
    //public string sync_HostIP;//服务器更新客户端读取（IP后三位）

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// 将姿态数据发送到座椅驱动端
    /// </summary>
    /// <param name="rotate">一个物体的局部坐标系的Rotation值，前倾后仰（不要超过90度），左右倾俯（不要超过90度），原地旋转姿态</param>
    /// <param name="pos">控制座椅一些特效，Y值控制座椅整体上下（这里需要给0~4的值，2为中位位置）,原点值为Vector3(0, 2, 0)</param>
    /// <param name="clientIP">传过来ClientNetworkMgr里面的localIP即可，不然默认传“xxx”</param>
    [Command]
    public void CmdUpdateCarPose(Quaternion rotate, Vector3 pos, string clientIP)
    {
        //客户端不需要实现
    }

    [ClientRpc]
    void RpcStartGame(string sceneName)
    {
        //TODO:
    }

    [ClientRpc]
    void RpcStopGame()
    {
        //TODO:
    }

    /// <summary>
    /// 用于服务器控制将正在连接的客户端的第一个作为姿态发送端，为同步播放全景视频特殊处理
    /// </summary>
    /// <param name="hostIP">其中一个客户端IP后三位</param>
    [ClientRpc]
    void RpcUpdateHostIP(string hostIP)
    {
        ClientNetworkMgr net = GameObject.FindObjectOfType<ClientNetworkMgr>();
        if (net != null)
        {
            net.hostIP = hostIP;
        }

        //每次更新host时将服务端姿态置为初始值
        CmdUpdateCarPose(Quaternion.identity, new Vector3(0, 2, 0), "xxx");
    }

    [ClientRpc]
    void RpcGameAlreadyStart()
    {
        //TV端实现
    }

    [ClientRpc]
    void RpcGameAlreadyStop()
    {
        //TV端实现
    }
}
