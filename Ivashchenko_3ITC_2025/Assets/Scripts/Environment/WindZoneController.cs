using UnityEngine;

public class WindZoneController : MonoBehaviour
{
    WindZone wz;
    void Start()
    {
        wz = GetComponent<WindZone>();
    }

    void Update()
    {
        transform.Rotate(Random.Range(-1f, 3f) * Time.deltaTime, Random.Range(-1f, 3f) * Time.deltaTime, Random.Range(-1f, 3f) * Time.deltaTime, Space.World);
        wz.windTurbulence += (Random.Range(-2f, 2f) * 0.001f);
        if(wz.windTurbulence < 0 || wz.windTurbulence > 3)
        {
            wz.windTurbulence = 1.5f;
        }
    }
}
