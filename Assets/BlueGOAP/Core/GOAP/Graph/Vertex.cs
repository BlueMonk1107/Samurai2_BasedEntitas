
using System.Collections.Generic;

namespace BlueGOAP
{
    public class Vertex
    {
        public int ID { get; private set; }
        private List<Edge> _edges;

        public Vertex(int id)
        {
            ID = id;
            _edges = new List<Edge>();
        }

        public void AddEdge(Edge edge)
        {
            if(edge.StartVex.ID == ID)
                _edges.Add(edge);
        }

        public List<Edge> GetEdges()
        {
            return _edges;
        }

        public Edge GetEdge(int otherVexId)
        {
            foreach (Edge edge in _edges)
            {
                if (edge.GetAnotherVertex(ID).ID == otherVexId)
                {
                    return edge;
                }
            }

            return null;
        }

        public bool IsThisVertex(int id)
        {
            return ID == id;
        }
    }
}
