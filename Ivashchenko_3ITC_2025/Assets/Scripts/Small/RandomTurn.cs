using UnityEngine;

public class RandomTurn : MonoBehaviour
{
    [SerializeField] bool TurnX;
    [SerializeField] bool TurnY;
    [SerializeField] bool TurnZ;
    [SerializeField] float MaxTurn;
    void Start()
    {
        if (TurnX) transform.Rotate(Random.Range(-MaxTurn, MaxTurn), 0, 0);
        if (TurnY) transform.Rotate(0, Random.Range(-MaxTurn, MaxTurn), 0);
        if (TurnZ) transform.Rotate(0, 0, Random.Range(-MaxTurn, MaxTurn));
        Destroy(this);
    }
}
