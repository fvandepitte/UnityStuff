using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PathController : MonoBehaviour {

    public GameObject Target, Path, Explored;

    private static Node _targetTile;
    private Node _currentNode;
    private Node _startTile;

    private List<Node> _open, _closed;

    public Stack<Vector3> CalculatePath(Tile target, Tile current) {
        _targetTile = new Node { Tile = target };
        
        _startTile = _currentNode = new Node { Tile = current };

        _open = new List<Node>();
        _closed = new List<Node>();
        _closed.Add(_startTile);

        CalculateNext();

        _currentNode = _targetTile;

        Stack<Vector3> points = new Stack<Vector3>();
        while (_currentNode != _startTile)
        {
            points.Push(_currentNode.Tile.Representation.transform.position);
            _currentNode = _currentNode.Parent;
        }

        _open.Clear();
        _closed.Clear();

        return points;
    }

    private void CalculateNext() {
        if (_currentNode.NeighBours.Contains(_targetTile))
        {
            _targetTile.Parent = _currentNode;
        }
        else
        {
            List<Node> neighbours = new List<Node>(_currentNode.NeighBours.Except(_closed));

            _open.AddRange(neighbours.Except(_open));
            
            foreach (Node node in _open.Intersect(neighbours))
            {
                int newF = node.G(_currentNode) + node.H;
                if (node.F == 0 || newF < node.F)
                {
                    node.F = newF;
                    node.Parent = _currentNode;
                }
                
                
            }

            _currentNode = _open.OrderBy(n => n.F).First();
            _closed.Add(_currentNode);
            _open.Remove(_currentNode);
            CalculateNext();
        }

    }

    class Node
    {
        public IEnumerable<Node> NeighBours { get { return Tile.Neighbours.Where(t => t.Walkable).Select(t => new Node { Tile = t }); } }

        public Tile Tile { get; set; }
        public int H { get { return (Mathf.Abs(Tile.Row - _targetTile.Tile.Row) + Mathf.Abs(Tile.Column - _targetTile.Tile.Column)) * 5; } }
        public int G(Node n) 
        {
            return n.Tile.Column == this.Tile.Column || n.Tile.Row == this.Tile.Row ? 10 : 14;
        }

        public int F { get; set; }

        public Node Parent { get; set; }

        public static bool operator ==(Node a, Node b) {
            return a.Tile == b.Tile;
        }

        public static bool operator !=(Node a, Node b) {
            return a.Tile != b.Tile;
        }

        public override bool Equals(object obj) {
            if (typeof(Node) == obj.GetType())
            {
                return this == (obj as Node);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
