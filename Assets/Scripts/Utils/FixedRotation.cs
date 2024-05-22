using UnityEngine;


/// <summary>
/// This class forces the rotation to be indipendent from parents rotation
/// </summary>
public class FixedRotation : MonoBehaviour
{

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = transform.parent.rotation *
                            Quaternion.Inverse(transform.parent.rotation) * initialRotation;
    }
}
