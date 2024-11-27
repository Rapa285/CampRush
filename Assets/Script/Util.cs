using System.Collections.Generic;
using UnityEngine;

public static class SpriteListComparer
{
    public static bool AreListsEqual(List<Sprite> list1, List<Sprite> list2)
    {
        // Check if the lists have the same count
        if (list1.Count != list2.Count)
            return false;

        // Create dictionaries to count occurrences
        Dictionary<Sprite, int> count1 = CountOccurrences(list1);
        Dictionary<Sprite, int> count2 = CountOccurrences(list2);

        // Compare the dictionaries
        foreach (var kvp in count1)
        {
            if (!count2.TryGetValue(kvp.Key, out int count) || count != kvp.Value)
                return false;
        }

        return true;
    }

    private static Dictionary<Sprite, int> CountOccurrences(List<Sprite> list)
    {
        Dictionary<Sprite, int> counts = new Dictionary<Sprite, int>();
        foreach (Sprite sprite in list)
        {
            if (counts.ContainsKey(sprite))
                counts[sprite]++;
            else
                counts[sprite] = 1;
        }
        return counts;
    }
}


public static class SpriteListPrinter
{
    public static void PrintSpriteList(List<Sprite> sprites)
    {
        if (sprites == null || sprites.Count == 0)
        {
            Debug.Log("The sprite list is empty.");
            return;
        }

        Debug.Log("Sprite List:");
        for (int i = 0; i < sprites.Count; i++)
        {
            Sprite sprite = sprites[i];
            if (sprite != null)
            {
                Debug.Log($"Index {i}: {sprite.name}");
            }
            else
            {
                Debug.Log($"Index {i}: Null Sprite");
            }
        }
    }
}
