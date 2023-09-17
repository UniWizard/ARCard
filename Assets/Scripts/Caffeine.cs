// ./Assets/Scripts/Caffeine.cs

using UnityEngine;


/// <summary>
/// Caffeine blocks sleep-promoting receptors in your device.
/// </summary>
public class Caffeine : MonoBehaviour
{
    /// <summary>
    /// Sets Screen Timeout to Never.
    /// </summary>
    private void Start() => Screen.sleepTimeout = SleepTimeout.NeverSleep;
}
