// ./Assets/Scripts/Rester.cs

using System.Collections.Generic;

using UnityEngine;


/// <summary>
/// Interface for Toggling Rester
/// </summary>
public class Rester : MonoBehaviour
{
    // Declaring Variables
    private bool activated;
    private List<Material> materials = new List<Material>();
    private GameObject info;
    private AudioSource ResterAudio;
    public Rester otherRester;

    /// <summary>
    /// Initialize Variables
    /// </summary>
    private void Awake()
    {
        activated = false;
        info = transform.Find("info").gameObject;
        ResterAudio = GameObject.Find("ResterAudio").GetComponent<AudioSource>();

        // There are multiple LEDs, with same material,
        // but once loaded, Each material acts independently...
        foreach (Transform child in transform.Find("LEDs"))
        {
            materials.Add(child.GetComponent<Renderer>().material);
        }
    }

    /// <summary>
    /// Switch states according <c>activated</c>, Audio must be played every time.
    /// </summary>
    public void Toggle()
    {
        ResterAudio.Play();

        if (activated)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }

    /// <summary>
    /// if <c>activated</c>: Disable Emission, InActivate <c>info</c>, Update GUI
    /// </summary>
    public void Deactivate()
    {
        if (activated)
        {
            activated = false;
            foreach (Material mat in materials)
            {
                mat.DisableKeyword("_EMISSION");
            }
            info.SetActive(false);
            DynamicGI.UpdateEnvironment();
        }
    }

    /// <summary>
    /// Deactivate Other, Enable Emission, Activate <c>info</c>, Update GUI and <c>activated</c>
    /// </summary>
    public void Activate()
    {
        activated = true;
        if (otherRester.activated)
        {
            otherRester.Deactivate();
        }

        foreach (Material mat in materials)
        {
            mat.EnableKeyword("_EMISSION");
        }
        info.SetActive(true);
        DynamicGI.UpdateEnvironment();
    }
}
