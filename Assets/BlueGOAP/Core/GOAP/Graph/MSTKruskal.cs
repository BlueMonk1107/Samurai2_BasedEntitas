
using System.Collections.Generic;
using UnityEngine;

namespace BlueGOAP
{
    //结果是大于0的，第一个数小
    public class ComparerWeight : IComparer<Edge>
    {
        public int Compare(Edge x, Edge y)
        {
            return x.CompareTo(y);
        }
    }
    //普里姆算法 MST = mini span tree
    public class MSTPrim
    {
        private bool[] _isMark; // 生成树的顶点
        private List<Edge> _mstEdges; // 生成树的边
        private PriorityQueue<Edge> pqueue; // 横切边

        public MSTPrim(Graph g)
        {
            _isMark = new bool[g.VexCount];
            _mstEdges = new List<Edge>();
            pqueue = new PriorityQueue<Edge>(new ComparerWeight());
            Visit(g, 0);
            while (pqueue.Count > 0)
            {
                Edge e = pqueue.Pop();
                Vertex a = e.StartVex;
                Vertex b = e.EndVex;

                if (_isMark[a.ID] && _isMark[b.ID])
                    continue; // 无效的边

                _mstEdges.Add(e);

                if (!_isMark[a.ID])
                    Visit(g, a.ID);

                if (!_isMark[b.ID])
                    Visit(g, b.ID);
            }
        }

        private void Visit(Graph g, int vexId)
        { 
            // 访问当前节点，将附近的边全部加进优先队列中
            _isMark[vexId] = true;
            foreach (Edge e in g.GetEdgesByVex(vexId))
            {
                if (!_isMark[e.GetAnotherVertex(vexId).ID])
                {
                    pqueue.Push(e);
                }
            }
        }

        public double Weight()
        {
            double weight = 0;
            foreach (Edge e in GetEdges())
            {
                weight += e.Weight;
            }
            return weight;
        }

        public List<Edge> GetEdges()
        {
            return _mstEdges;
        }
    }

}
