using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStates<T,J> where J:MonoBehaviour //si o si J va en la escena
{
    public FSM <T,J>fsm;
    public J avatar;

    public BaseStates<T,J> SetUp (FSM<T,J> newFSM)
    {
        fsm = newFSM;
        return this;
    }

    public BaseStates<T, J> SetAvatar(J newAvatar)
    {
        avatar = newAvatar;
        return this;
    }
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }

}
