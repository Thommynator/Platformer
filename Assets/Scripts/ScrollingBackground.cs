using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    public int offsetBuffer;
    private Camera cam;
    private float visibleCameraWidth;
    private float backgroundWitdh;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        visibleCameraWidth = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z)).x;
        backgroundWitdh = GetComponent<BoxCollider2D>().size.x;
    }

    void Update()
    {

        RELATIVE_TO_CAMERA whereIs = WhereIsRelativeToCamera();
        float distanceToCameraAtCurrentPosition = DistanceToCameraAtCurrentPosition();
        if (whereIs == RELATIVE_TO_CAMERA.LEFT && DistanceToCameraWhen(RELATIVE_TO_CAMERA.RIGHT) < distanceToCameraAtCurrentPosition)
        {
            Move(RELATIVE_TO_CAMERA.RIGHT);
        }

        else if (whereIs == RELATIVE_TO_CAMERA.RIGHT && DistanceToCameraWhen(RELATIVE_TO_CAMERA.LEFT) < distanceToCameraAtCurrentPosition)
        {
            Move(RELATIVE_TO_CAMERA.LEFT);
        }

    }

    private Vector3 PositionWhen(RELATIVE_TO_CAMERA relativePosition)
    {
        if (relativePosition == RELATIVE_TO_CAMERA.LEFT)
        {
            return new Vector3(transform.position.x - 2 * backgroundWitdh, transform.position.y, transform.position.z);
        }
        else
        {
            return new Vector3(transform.position.x + 2 * backgroundWitdh, transform.position.y, transform.position.z);
        }
    }

    private float DistanceToCameraAtCurrentPosition()
    {
        return Vector3.Distance(transform.position, cam.transform.position);
    }

    private float DistanceToCameraWhen(RELATIVE_TO_CAMERA relativePosition)
    {
        return Vector3.Distance(PositionWhen(relativePosition), cam.transform.position);
    }

    private void Move(RELATIVE_TO_CAMERA direction)
    {
        transform.position = PositionWhen(direction);
    }

    private RELATIVE_TO_CAMERA WhereIsRelativeToCamera()
    {
        if (transform.position.x < cam.transform.position.x)
        {
            return RELATIVE_TO_CAMERA.LEFT;
        }
        return RELATIVE_TO_CAMERA.RIGHT;
    }

    private enum RELATIVE_TO_CAMERA
    {
        LEFT, RIGHT
    }
}
