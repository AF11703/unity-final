using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastLogic : MonoBehaviour
{

    [SerializeField] Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Vector3 screenCenter = new(Screen.width / 2f, Screen.height / 2f);
            Ray rayOrigin = cam.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(rayOrigin, out RaycastHit hit, 1f))
            {
                GameObject gg = hit.transform.gameObject;

                if (gg.CompareTag("Door"))
                {
                    Debug.Log("");
                }

                if (gg.CompareTag("Trophy"))
                {
                    Debug.Log("Trophy collected");
                }
            }
        }
    }
}
