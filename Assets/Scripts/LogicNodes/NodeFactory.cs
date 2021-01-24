using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class NodeFactory
{
    public NodeType type;

    public LogicNode GetNode()
    {
        switch (type)
        {
            case NodeType.And: return new NodeAnd();
            case NodeType.Or: return new NodeOr();
            case NodeType.Not: return new NodeNot();
            default: return null;
        }
    }

    public static implicit operator LogicNode(NodeFactory obj) =>
        obj.GetNode();

    public enum NodeType : byte
    {
        And,
        Or,
        Not
    }
}