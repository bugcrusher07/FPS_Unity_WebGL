
using UnityEngine;

public class InputTester : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
            Debug.Log("Key pressed: " + Input.inputString);

        if (Input.GetKeyDown(KeyCode.P))
            Debug.Log("P key detected via KeyCode");
    }
}