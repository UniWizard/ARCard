// ./Assets/Scripts/Card.cs

using UnityEngine;


/// <summary>
/// Override <c>OnEnable</c> to randomly enable of the the <c>set</c>.
/// </summary>
public class Card : MonoBehaviour
{
    // Declaring Variables

    /// <summary>
    /// A <c>set</c> is collection of <c>name</c> and <c>pic</c>.
    /// </summary>
    private GameObject set1, set2;

    /// <summary>
    /// Initialize Sets.
    /// </summary>
    private void Awake()
    {
        set1 = transform.Find("set1").gameObject;
        set2 = transform.Find("set2").gameObject;
    }

    /// <summary>
    /// Randomly enabling one set whilst disabling the other.
    /// </summary>
    private void OnEnable()
    {
        // 60% Chance of Set 1
        bool active = Random.Range(0, 5) < 3;

        set1.SetActive(active);
        set2.SetActive(!active);
    }
}
