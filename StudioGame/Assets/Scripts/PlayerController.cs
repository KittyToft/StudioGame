using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //movment variables
    public float RunSpeed;
    public float WalkSpeed;

    Rigidbody MyRB;
    Animator MyAnim;
    bool FacingRight;  // for the character to be able to flip

    //for jumping
    bool Grounded = false;
    Collider[] GroundCollisions;
    float GroundChackRadius = 0.2f;
    public LayerMask GroundLayer;
    public Transform GroundCheck;
    public float JumpHeight;

    // These are for the rails system
    public bool isInRailBox = false;
    public GameObject RailBox;
    public enum Direction {
        Forward, Backward, Left, Right
        }
    public Direction direction;
    public bool transitionState = false;
    [HideInInspector]
    public string cameraDirection = "front";

    public GameObject MyRotateObject;


    // Use this for initialization
    void Start() {
        MyRB = GetComponent<Rigidbody>();
        MyAnim = GetComponent<Animator>();
        FacingRight = true;

        }

    // Update is called once per frame
    void Update() {

        }

    void FixedUpdate() {
        if (transitionState) return;

        if (Grounded && Input.GetAxis("Jump") > 0) {
            Grounded = false;
            MyAnim.SetBool("Grounded", false);
            MyRB.AddForce(new Vector3(0, JumpHeight, 0));
            print(MyRB.velocity);
            }

        GroundCollisions = Physics.OverlapSphere(GroundCheck.position, GroundChackRadius, GroundLayer);
        if (GroundCollisions.Length > 0) Grounded = true;
        else Grounded = false;

        MyAnim.SetBool("Grounded", Grounded);

        float Move = Input.GetAxis("Horizontal");
        MyAnim.SetFloat("Speed", Mathf.Abs(Move));

        float Sneaking = Input.GetAxisRaw("Fire3"); // left Shift button, (Edit - project settings - imput for controlling what button
        MyAnim.SetFloat("Sneaking", Sneaking);

        if(cameraDirection == "back" || cameraDirection == "left") {
            Move = -Move;
            }

        // TODO: Split/fix this up to allow camera to turn all the way while still controlling character correctly
        if (direction == Direction.Right || direction == Direction.Left) {
            if (Sneaking > 0 && Grounded) {
                //Debug.Log("WalkSpeed " + WalkSpeed);
                MyRB.velocity = new Vector3(Move * WalkSpeed, MyRB.velocity.y, 0);
                //Debug.Log("velocity " + MyRB.velocity);
                }
            else {
                MyRB.velocity = new Vector3(Move * RunSpeed, MyRB.velocity.y, 0);
                }
            }

        else if (direction == Direction.Forward || direction == Direction.Backward) {
            if (Sneaking > 0 && Grounded) {
                //Debug.Log("WalkSpeed " + WalkSpeed);
                MyRB.velocity = new Vector3(0, MyRB.velocity.y, Move * WalkSpeed);
                //Debug.Log("velocity " + MyRB.velocity);
                }
            else {
                MyRB.velocity = new Vector3(0, MyRB.velocity.y, Move * RunSpeed);
                }
            }


        // Making the player move in rails
        if (Grounded && isInRailBox && RailBox.tag == "MoveBox" && Mathf.Abs(Input.GetAxis("Vertical")) > 0) {
            if (RailBox.GetComponent<RailsSystem>().fromDirection.ToString() == "LeftRight") {
                if (direction == Direction.Left || direction == Direction.Right) {
                    RailBox.GetComponent<RailsSystem>().ChangeRail(this.gameObject);
                    }
                }
            else if (RailBox.GetComponent<RailsSystem>().fromDirection.ToString() == "ForwardBackward") {
                if (direction == Direction.Forward || direction == Direction.Backward) {
                    RailBox.GetComponent<RailsSystem>().ChangeRail(this.gameObject);
                    }
                }

            }

        // TODO: Write the code for player to return the other way
        // Making the player rotate
        if (Grounded && isInRailBox && RailBox.tag == "RotateBox" && Input.GetAxis("Horizontal") > 0) {
            if (direction.ToString() == RailBox.GetComponent<RailsSystem>().fromDirection.ToString()) {
                RailBox.GetComponent<RailsSystem>().RotateHere(this.gameObject);
                }
            }
        if (Grounded && isInRailBox && RailBox.tag == "RotateBox" && Input.GetAxis("Horizontal") < 0) {
            if (direction.ToString() == RailBox.GetComponent<RailsSystem>().oppositeDirection) {
                RailBox.GetComponent<RailsSystem>().OppositeRotateHere(this.gameObject);
                }
            }

        if (cameraDirection == "back" || cameraDirection == "left") {
            Move = -Move;
            }

            if (Move > 0 && !FacingRight) {
            Flip();
            }
        else if (Move < 0 && FacingRight) {
            Flip();
            }
        }

    void Flip() {
        FacingRight = !FacingRight;
        MyRotateObject.transform.Rotate(0, 180, 0);
        ChangeDirectionOnTurn();
        }

    void ChangeDirectionOnTurn() {
        if (direction == Direction.Right) {
            direction = Direction.Left;
            }
        else if (direction == Direction.Left) {
            direction = Direction.Right;
            }
        else if (direction == Direction.Forward) {
            direction = Direction.Backward;
            }
        else if (direction == Direction.Backward) {
            direction = Direction.Forward;
            }
        }

    public void RotateRight() {
        if (direction == Direction.Right) {
            direction = Direction.Forward;
            }
        else if (direction == Direction.Left) {
            direction = Direction.Backward;
            }
        else if (direction == Direction.Forward) {
            direction = Direction.Left;
            }
        else if (direction == Direction.Backward) {
            direction = Direction.Right;
            }
        }

    public void RotateLeft() {
        if (direction == Direction.Right) {
            direction = Direction.Backward;
            }
        else if (direction == Direction.Left) {
            direction = Direction.Forward;
            }
        else if (direction == Direction.Forward) {
            direction = Direction.Right;
            }
        else if (direction == Direction.Backward) {
            direction = Direction.Left;
            }
        }

    public void RotateCameraDirectionOnTurnRight() {
        if (cameraDirection == "front") {
            cameraDirection = "left";
            }
        else if (cameraDirection == "left") {
            cameraDirection = "back";
            }
        else if (cameraDirection == "back") {
            cameraDirection = "right";
            }
        else if (cameraDirection == "right") {
            cameraDirection = "front";
            }
        }

    public void RotateCameraDirectionOnTurnLeft() {
        if (cameraDirection == "front") {
            cameraDirection = "right";
            }
        else if (cameraDirection == "left") {
            cameraDirection = "front";
            }
        else if (cameraDirection == "back") {
            cameraDirection = "left";
            }
        else if (cameraDirection == "right") {
            cameraDirection = "back";
            }
        }



    public void EndTransitionState() {
        transitionState = false;
        }
    }
// This is made for a computer
// use of mouse, A and D key