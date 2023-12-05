using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketMessage : MonoBehaviour
{
    private SocketManagement socketManager;
    // Start is called before the first frame update
    public virtual void Start()
    {
        GameObject socketObject = GameObject.Find("Socket");

        if (socketObject != null)
        {
            socketManager = socketObject.GetComponent<SocketManagement>();
        }
    }

    public virtual void Update()
    {
        if (socketManager != null)
        {
            int currentMessage = socketManager.GetCurrentMessage();

                if (currentMessage == 1)
                {
                    TiltRight();
                }
                else if (currentMessage == 2)
                {
                    TiltLeft();
                } else if (currentMessage == 0)
                {
                    NoTilt();
                }
        }
        else
        {
            NoSocket();
        }
    }

    public virtual void TiltRight()
    {
        //This method will be overwritten (virtual void)
    }

    public virtual void TiltLeft()
    {
        //This method will be overwritten (virtual void)
    }

    public virtual void NoTilt()
    {
        //This method will be overwritten (virtual void)
    }

    public virtual void NoSocket()
    {
        //This method will be overwritten (virtual void)
    }
}
