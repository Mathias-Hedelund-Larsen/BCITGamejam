using UnityEngine;

/// <summary>
/// The class which can operate a machine where it is allowed
/// </summary>
public class Cat : MachineOperator<Cat>
{
    [SerializeField]
    //The mark of the target machine, also exposed to the inspector
    private MachineMarker targetMachine = MachineMarker.CatFSM;

    [SerializeField]
    private RuntimeAnimatorController c;

    public Animator ting { get; private set; }

    /// <summary>
    /// Unity start method, where the machine instance is set by the init methods
    /// <summary>
    private void Start()
    {
        //Running the init of the machineoperator, to find the machine instance
        Init(targetMachine);

        //Calling the must run method for the machine instance, and enabling the change state with types
        MachineInstance.Init(useStateNames: false);

        MachineInstance.ChangeState<NormalState>(this);
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        //Update the active state
        MachineInstance.ExecuteActiveState(this);
    }
}
