using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZFramework.Interfaces;

public class PlayerController : MonoBehaviour , ICharacterController
{
    public IController Controller => this;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    public void InitManager()
    {
        throw new System.NotImplementedException();
    }

    public void InitAction()
    {
        throw new System.NotImplementedException();
    }

    public void CancelAction()
    {
        throw new System.NotImplementedException();
    }
}
