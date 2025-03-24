using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    private static readonly System.Random random = new System.Random();
    public static T GetRandomItem<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
            throw new System.InvalidOperationException("Cannot get a random item from an empty or null list.");

        int index = random.Next(list.Count);
        return list[index];
    }
}
