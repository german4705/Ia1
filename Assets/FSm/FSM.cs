using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM <T,J> where J :MonoBehaviour
{
    public BaseStates<T,J> _actualState;
    public Dictionary<T, BaseStates<T,J>> _possibleStates = new Dictionary<T, BaseStates<T,J>>();
    
  
    

    public void ChangeState(T newState)
    {
        if (!_possibleStates.ContainsKey(newState)) return; //si no hay nada en el diccionario.

        _actualState?.OnExit(); // preguntar si no es nulo.
        _actualState = _possibleStates[newState];   //Cambio de estados con el diccionario.
        _actualState.OnEnter();

    }
    
}

public enum EnemyStates   // estados 
{
    FollowLeaderState,
    AttackState,
    GoToBaseState,
    RecoverState,
    BeingAttackedState
}

public enum LeaderStates
{  
    LeaderMove,
    LeaderRecovery,
    LeaderGoToBase
}

public enum Team
{
    TeamA,
    TeamB
}
