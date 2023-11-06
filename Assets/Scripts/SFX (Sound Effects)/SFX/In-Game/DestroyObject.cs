using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject DestroyThisObject;
    public float TimeToDestroy;

    void Start()
    {
        Destroy(DestroyThisObject, TimeToDestroy);
    }
}