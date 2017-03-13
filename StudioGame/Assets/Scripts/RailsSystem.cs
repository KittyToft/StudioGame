using UnityEngine;
using System.Collections;

public class RailsSystem : MonoBehaviour {

    [Tooltip("Positive values move player towards camera, negative values move player away from camera.")]
    public float moveDistance;
    float turnDegrees;
    [HideInInspector]
    public string oppositeDirection;

    public enum FromDirection {
        LeftRight, ForwardBackward, Forward, Backward, Left, Right
        }

    public FromDirection fromDirection;

    public enum TurnDirection {
        None, Right, Left
        }

    public TurnDirection turnDirection;

    // Use this for initialization
    void Start() {
        if (turnDirection == TurnDirection.Left) {
            turnDegrees = -0.25f;
            }
        else if (turnDirection == TurnDirection.Right) {
            turnDegrees = 0.25f;
            }

        // Set opposite direction
        if (turnDirection == TurnDirection.Left) {
            if (fromDirection == FromDirection.Forward) {
                oppositeDirection = "Left";
                }
            else if (fromDirection == FromDirection.Backward) {
                oppositeDirection = "Right";
                }
            else if (fromDirection == FromDirection.Left) {
                oppositeDirection = "Backward";
                }
            else if (fromDirection == FromDirection.Right) {
                oppositeDirection = "Forward";
                }
            }
        else if (turnDirection == TurnDirection.Right) {
            if (fromDirection == FromDirection.Forward) {
                oppositeDirection = "Right";
                }
            else if (fromDirection == FromDirection.Backward) {
                oppositeDirection = "Left";
                }
            else if (fromDirection == FromDirection.Left) {
                oppositeDirection = "Forward";
                }
            else if (fromDirection == FromDirection.Right) {
                oppositeDirection = "Backward";
                }
            }
        }

    void OnTriggerEnter(Collider other) {
        other.GetComponent<PlayerController>().isInRailBox = true;
        other.GetComponent<PlayerController>().RailBox = this.gameObject;
        }

    void OnTriggerExit(Collider other) {
        other.GetComponent<PlayerController>().isInRailBox = false;
        other.GetComponent<PlayerController>().RailBox = null;
        }

    public void ChangeRail(GameObject other) {
        iTween.MoveBy(other, iTween.Hash("x", moveDistance, "easeType", "easeInOutSine", "oncomplete", "EndTransitionState"));
        other.GetComponent<PlayerController>().transitionState = true;
        other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

    public void RotateHere(GameObject other) {
        iTween.RotateBy(other, iTween.Hash("y", turnDegrees, "oncomplete", "EndTransitionState", "time", 0.5));
        other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        Vector3 middleOfSquare = this.transform.position;
        middleOfSquare = new Vector3(middleOfSquare.x, other.transform.position.y, middleOfSquare.z);
        iTween.MoveTo(other, iTween.Hash("position", middleOfSquare, "easeType", "easeInOutSine", "time", 0.2));
        other.GetComponent<PlayerController>().transitionState = true;


        // This sets correct direction
        if (turnDirection == TurnDirection.Left) {
            other.GetComponent<PlayerController>().RotateLeft();
            other.GetComponent<PlayerController>().RotateCameraDirectionOnTurnLeft();
            }
        if (turnDirection == TurnDirection.Right) {
            other.GetComponent<PlayerController>().RotateRight();
            other.GetComponent<PlayerController>().RotateCameraDirectionOnTurnRight();
            }

        }
    public void OppositeRotateHere(GameObject other) {
        iTween.RotateBy(other, iTween.Hash("y", -turnDegrees, "oncomplete", "EndTransitionState", "time", 0.5));
        other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        Vector3 middleOfSquare = this.transform.position;
        middleOfSquare = new Vector3(middleOfSquare.x, other.transform.position.y, middleOfSquare.z);
        iTween.MoveTo(other, iTween.Hash("position", middleOfSquare, "easeType", "easeInOutSine", "time", 0.2));
        other.GetComponent<PlayerController>().transitionState = true;

        // This sets correct direction
        if (turnDirection == TurnDirection.Left) {
            other.GetComponent<PlayerController>().RotateRight();
            other.GetComponent<PlayerController>().RotateCameraDirectionOnTurnRight();
            }
        if (turnDirection == TurnDirection.Right) {
            other.GetComponent<PlayerController>().RotateLeft();
            other.GetComponent<PlayerController>().RotateCameraDirectionOnTurnLeft();
            }

        }

    }
