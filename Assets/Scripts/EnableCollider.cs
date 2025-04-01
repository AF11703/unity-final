using UnityEngine;

public class EnableCollider : MonoBehaviour
{
    [SerializeField] GameObject itemWithCollider;
    Collider cd;


    private void Awake()
    {
        cd =  itemWithCollider.GetComponent<Collider>();
    }

    
    public void Enable()
    {
        if (cd != null)
            cd.enabled = true;
    }

    public void Disable()
    {
        if (cd != null)
            cd.enabled = false;
    }
}
