using UnityEngine;

public interface IStateMachine<T> 
{
    public abstract void ChangeState(T type);
    public abstract void UpdateState();
}
