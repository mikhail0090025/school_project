using System.Collections.Generic;
using UnityEngine;

public class DeadbodyScript : MonoBehaviour
{
    [SerializeField] List<Renderer> bodyParts;
    public void PaintMe(Color color)
    {
        foreach (var part in bodyParts)
        {
            part.material.color = color;
        }
    }
}
