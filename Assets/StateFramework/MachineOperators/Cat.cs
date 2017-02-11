using UnityEngine;

/// <summary>
/// The class which can operate a machine where it is allowed
/// </summary>
public class Cat : MachineOperator<Cat>
{
    [SerializeField]
    //The mark of the target machine, also exposed to the inspector
    private MachineMarker targetMachine = MachineMarker.CatFSM;

    private Animator animator;
    public Rigidbody2D Rigidbody { get; set; }
    private bool canJump;
    public GameObject hairBallPrefab;
    public Transform hairBallSpawner;
    private Vector3 curLoc;
    private Vector3 preLoc;
    private float xScale;

    /// <summary>
    /// Unity start method, where the machine instance is set by the init methods
    /// <summary>
    private void Start()
    {
        //Running the init of the machineoperator, to find the machine instance
        Init(targetMachine);

        //Calling the must run method for the machine instance, and enabling the change state with types
        MachineInstance.Init();

        Rigidbody = GetComponent<Rigidbody2D>();

        MachineInstance.ChangeState<NormalState>(this);

        xScale = transform.localScale.x;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        //Update the active state
        MachineInstance.ExecuteActiveState(this);


        InputListen();
    }


    /// <summary>
    /// Select the state to go to here
    /// </summary>
    private void SelectState()
    {
        // if we are not in the normal state return
        if (!(ActiveState is NormalState))
            return;

        //Get the state in the carousel

        MachineInstance.ChangeState<GravityState>(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            canJump = false;
        }
    }

    public void Fire(GameObject hairBallPrefab, Transform hairBallSpawner)
    {
        //Create the hairball!
        var hairBall = Instantiate(hairBallPrefab, hairBallSpawner.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        if (transform.localScale.x < 0)
        {
            hairBall.GetComponent<BulletController>().Init(-0.1f);
        }
        if (transform.localScale.x > 0)
        {
            hairBall.GetComponent<BulletController>().Init(0.1f);
        }

        //Rigidbody2D rigidBody = hairBall.GetComponent<Rigidbody2D>();
        //rigidBody.AddForce(hairBall.transform.right * 750f);
        //Debug.Log(rigidBody);


        Destroy(hairBall, 5.0f);
    }

    public void ChangeAnimatorController(RuntimeAnimatorController controller)
    {
        //Enable this when we have an animator and the states have a controller
        //animator.runtimeAnimatorController = controller;
    }

    public void ChangeCollisionLayer(string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    private void InputListen()
    {
        preLoc = curLoc;


        curLoc = transform.position;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectState();
        }
        if (canJump && Input.GetKeyDown(KeyCode.W))
        {
            Rigidbody.AddForce(Vector2.up * 250);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Fire(hairBallPrefab, hairBallSpawner);
        }
        //left movement
        if (Input.GetKey(KeyCode.A))
        {
            curLoc -= new Vector3(1 * Time.fixedDeltaTime, 0);
            transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
        }
        //Right movement
        if (Input.GetKey(KeyCode.D))
        {
            curLoc += new Vector3(1 * Time.fixedDeltaTime, 0);
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);

        }

        transform.position = curLoc;
    }
}

