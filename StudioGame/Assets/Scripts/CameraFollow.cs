using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Transform Target;
    public float Smoothing = 5f;
    string cameraDirection = "front";
    Vector3 initialOffset;

    Vector3 Offset;

    // Use this for initialization
    void Start() {
        Offset = transform.position - Target.position;
        initialOffset = Offset;
        }

    // Update is called once per frame
    void Update() {
        Vector3 TargetCamPos = Target.position + Offset;
        Vector3 TargetCamRot = MoveOffsetByDirection();
        Quaternion TargetRotQuat = new Quaternion();
        TargetRotQuat.eulerAngles = TargetCamRot;
        transform.position = Vector3.Lerp(transform.position, TargetCamPos, Smoothing * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotQuat, Smoothing * Time.deltaTime);
        print(transform.rotation);
        

        // TODO: Make the camera turn with the player character
        // Modify Offset, and just rotate camera
        }

    Vector3 MoveOffsetByDirection() {
        cameraDirection = Target.parent.GetComponent<PlayerController>().cameraDirection;
        if (cameraDirection == "front") {
            Offset = initialOffset;
            return new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            }
        else if (cameraDirection == "left") {
            Offset = new Vector3(initialOffset.z, initialOffset.y, initialOffset.x);
            return new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
            }
        else if (cameraDirection == "back") {
            Offset = new Vector3(initialOffset.x, initialOffset.y, -initialOffset.z);
            return new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            }
        else if (cameraDirection == "right") {
            Offset = new Vector3(-initialOffset.z, initialOffset.y, -initialOffset.x);
            return new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
            }
        else {
            return transform.rotation.eulerAngles;
            }
        }
    }
