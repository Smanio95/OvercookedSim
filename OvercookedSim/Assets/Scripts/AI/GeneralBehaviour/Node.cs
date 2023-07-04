using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public BT_Status status;
    public int currentChild = 0;
    public List<Node> children = new();

    public Node() { }

    public void AddChild(Node child)
    {
        children.Add(child);
    }

    public virtual BT_Status Process() => children[currentChild].Process();
}
