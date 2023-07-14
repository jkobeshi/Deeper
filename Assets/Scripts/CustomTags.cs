using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTags : MonoBehaviour
{
    public List<string> tags;

    private void Start()
    {
        for (int i = 0; i < tags.Count; i++)
        {
            tags[i] = tags[i].ToLower();
        }
    }

    public void AddTag(string tag)
    {
        tags.Add(tag.ToLower());
    }

    public void RemoveTag(string tag)
    {
        tags.Remove(tag.ToLower());
    }

    public bool HasTag(string tag)
    {
        return tags.Contains(tag.ToLower()) || gameObject.tag == tag;
    }

    public bool HasTags(List<string> tags_in)
    {
        bool hasTags = false;
        for (int i = 0; i < tags_in.Count; i++)
        {
            hasTags = hasTags || HasTag(tags_in[i]);
        }
        return hasTags;
    }

    public static bool HasTags(Collider other, List<string> tags_in)
    {
        bool hasTags = tags_in.Contains(other.tag);
        CustomTags other_tags = other.GetComponent<CustomTags>();
        if (other_tags != null)
        {
            hasTags = hasTags || other_tags.HasTags(tags_in);
        }
        return hasTags;
    }
}
