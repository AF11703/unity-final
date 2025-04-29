using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastLogic : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject afterDeath;

    Enemy skelly;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        skelly = enemy.GetComponent<Enemy>();
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

                if (gg.CompareTag("TrophyUncollect"))
                {
                    enemy.SetActive(true);
                    gg.SetActive(false);
                    afterDeath.SetActive(true);

                }

                if (gg.CompareTag("Trophy") && skelly.getHealth() <= 0f)
                {
                    gg.SetActive(false);
                }
            }
        }
    }
}
