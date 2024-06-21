/// <remarks>
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class is applied to the haybale, upon triggering a collider with the tagfilter it will destroy the haybale
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayBoundary : MonoBehaviour
{
    public string tagFilter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagFilter))
        {
            Destroy(gameObject);
        }
    }
}
