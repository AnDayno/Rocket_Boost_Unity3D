using UnityEngine.InputSystem;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;

    private void OnEnable()
    {
        thrust.Enable();
    }

    private void Update()
    {
        if(thrust.IsPressed())
        {
            Debug.Log("Thrust is pressed");
        }
    }
}
