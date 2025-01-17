using UnityEngine;

public class DeleteIn : MonoBehaviour
{
    public int seconds;
    void Start()
    {
        Destroy(gameObject, seconds);
    }
}
