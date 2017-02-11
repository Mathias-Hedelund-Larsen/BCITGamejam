using UnityEngine;

/// <summary>
/// This is a class for making specified functionality for the state,
/// and to make the creation of the scriptable object possible.
/// </summary>
[CreateAssetMenu(fileName = "TimeCat", menuName = "States/TimeCat", order = 1)]
public class TimeCat : StateGeneric<Cat>
{
    [SerializeField]
    //The name of the state also exposed for the editor
    private string stateName = "TimeCat";

    [SerializeField]
    private RuntimeAnimatorController controller;

    /// <summary>
    /// Method which is called when a user enters this state, normally when the user changes states
    /// </summary>
    /// <param name="user"></param>
    public override void Enter(Cat user)
    {
        DimensionManager.Instance.StopDimensionTime();
    }

    /// <summary>
    /// Mehtod which is called when the user wants to execute, probably an execute is called each frame
    /// </summary>
    /// <param name="user"></param>
    public override void Execute(Cat user)
    {
    }

    /// <summary>
    /// Method which is called when a user exists this state, normally when the user changes states
    /// </summary>
    /// <param name="user"></param>
    public override void Exit(Cat user)
    {
        DimensionManager.Instance.StartDimensionTime();
    }

    /// <summary>
    /// This method is run to check if this state is ready to be exited, if you want a user to be in a state for any amount time this is where you stop it from exiting
    /// </summary>
    /// <param name="user"></param>
    public override bool IsReadyToExit(Cat user)
    {
        return true;
    }

    /// <summary>
    /// Setting the state type
    /// </summary>
    public override void SetStateType()
    {
        StateType = typeof(TimeCat);
    }

    /// <summary>
    /// Setting the state name
    /// </summary>
    public override void SetStateName()
    {
        StateName = stateName;
    }
}
