using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPAnchorController : MonoBehaviour
{
    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------
    private List<Transform> childrenAnchors = new List<Transform>();

    //------------------------------------------------------
    //                  GETTERS/SETTERS
    //------------------------------------------------------
    public List<Transform> GetChildrenAnchors(){return childrenAnchors;}
    public void SetChildrenAnchors(List<Transform> newList){childrenAnchors = newList;}

    public Transform GetSpecificAnchor(int index){return childrenAnchors[index];}
    public void SetSpecificAnchor(int index, Transform newAnchor){childrenAnchors[index] = newAnchor;}

    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------
    public void Awake()
    {
        HandleAnchorChildren();
    }

    //------------------------------------------------------
    //                  CUSTOM GENERAL FUNCTIONS
    //------------------------------------------------------

    public void HandleAnchorChildren()
    {
        foreach (Transform child in transform)
        {
            childrenAnchors.Add(child);
        }
    }
}
