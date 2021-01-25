using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class NodeFactory
{
    public NodeType type;

    public KeyCode keyCode;

    public LogicNode GetNode()
    {
        switch (type)
        {
            case NodeType.And: return new NodeAnd();
            case NodeType.Or: return new NodeOr();
            case NodeType.Not: return new NodeNot();
            case NodeType.XOr: return new NodeExOr();
            case NodeType.Input: return new NodeInput(keyCode);
            default: return null;
        }
    }

    public static implicit operator LogicNode(NodeFactory obj) =>
        obj.GetNode();

    public enum NodeType : byte
    {
        And,
        Or,
        Not,
        XOr,
        Input
    }
}