// ./Assets/Scripts/PrefabController.cs

using System.Collections;

using UnityEngine;


public class MachineryController : MonoBehaviour
{
    // Declaring Variables
    private int zoomingLastFrameSemaphore;
    private bool zoomed, flicked;
    private Vector2[] lastZoomPositions;
    private Vector2 touchPosition = default;
    private Camera ARCamera;
    private Animator aniController;
    private HelpText helpText;

    /// <summary>
    /// Initialize Variables, Flash the "Zoom" HelpText
    /// </summary>
    private void Start()
    {
        zoomingLastFrameSemaphore = 0;

        zoomed = false;
        flicked = false;

        ARCamera = FindObjectOfType<Camera>();

        helpText = FindObjectOfType<HelpText>();
        helpText.StartAnimation(1, 325.0f, "Open Brackets to Boot-Up!");

        aniController = this.GetComponent<Animator>();
        aniController.SetBool("zoomed", zoomed);
        aniController.SetBool("flicked", flicked);
    }

    /// <summary>
    /// Updated the Machinery State according to inputs and current states
    /// </summary>
    void Update()
    {
        // Zoom -> Flick -> Rester inputs

        // 2 inputs, maybe zoom, maybe not
        if (Input.touchCount == 2)
        {
            // Get Both Inputs
            Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };

            //  If first time tapped
            if (zoomingLastFrameSemaphore == 0)
            {
                lastZoomPositions = newPositions;

                // update by 2, since it would be decreased by 1, at the end of update
                zoomingLastFrameSemaphore = 2;
            }

            // If second time tapped, i.e. (Zoomed In or Out)
            else if (zoomingLastFrameSemaphore == 1)
            {
                /// Zoom based on the distance between the new positions compared to the
                /// distance between the previous positions.
                float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                float offset = newDistance - oldDistance;

                // Zoom Out, update Zoom Flag and Flash HelpText for Flicking
                if (!zoomed && offset > 40)
                {
                    zoomed = true;
                    aniController.SetBool("zoomed", zoomed);

                    helpText.StartAnimation(1, 350.0f, "Accelerate (shake) to Initiation!");
                }

                // Zoom In, Not allowed, Fun little Easter Egg
                else if (zoomed && offset < -40)
                {
                    helpText.StartAnimation(0, 600.0f, "ERROR! Irreversible Operation, Try Again in TENETverse?");
                }

                lastZoomPositions = newPositions;
            }
        }

        // If already Zoomed and now Flicked, update Flick Flag and Flash HelpText for Rester
        if (!flicked && zoomed && Input.acceleration.sqrMagnitude > 5)
        {
            flicked = true;
            aniController.SetBool("flicked", flicked);
            helpText.StartAnimation(0, 50.0f, "...");
            helpText.StartAnimation(5, 400.0f, "For more info, Tap the mouting stands.");
        }

        // If already Zoomed and Flicked, now Tapped
        if (flicked && zoomed && Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = ARCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    // Check if the tapped object is "Rester", if so toggle it.
                    Rester rester = hitObject.transform.GetComponent<Rester>();
                    if (rester)
                    {
                        rester.Toggle();
                    }

                    // Check if the tapped object is "Link", if so open it.
                    Link link = hitObject.transform.GetComponent<Link>();
                    if (link)
                    {
                        Application.OpenURL(link.URL);
                    };
                }

            }
        }

        // Reduce Semaphore at every update
        zoomingLastFrameSemaphore = Mathf.Max(zoomingLastFrameSemaphore - 1, 0);
    }
}
