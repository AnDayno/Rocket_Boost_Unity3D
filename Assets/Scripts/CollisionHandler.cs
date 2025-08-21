using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly collision detected.");
                break;
            case "Fuel":
                Debug.Log("Fuel collision detected.");
                break;
            case "Finish":
                Debug.Log("Finish line reached.");
                break;
            default:
                Debug.Log("Collision with an unknown object detected.");
                break;
        }
    }
}
