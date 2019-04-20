
using System;
using System.Collections.Generic;

namespace BlueGOAP
{
    public class Graph 
    {
        // 每个ID对应的顶点
        private Dictionary<int, Vertex> _vexDic; 

        public int VexCount
        {
            get { return _vexDic.Count; }
        }

        public int EdgeCount { get; private set; }

        public Graph()
        {
            EdgeCount = 0;
            _vexDic = new Dictionary<int, Vertex>();
        }

        public void AddEdge(int idStart,int idEnd,float weight)
        {
            Edge e = new Edge(new Vertex(idStart), new Vertex(idEnd), weight);
            AddEdgeByVex(e.StartVex.ID, e);
            //同时添加反向边，即可修改为无向图
            //AddEdgeByVex(e.EndVex.ID, e.GetReverseEdge());
            EdgeCount++;
        }

        private void AddEdgeByVex(int id, Edge e)
        {
            if (!_vexDic.ContainsKey(id) || _vexDic[id] == null)
            {
                _vexDic[id] = new Vertex(id);
            }
            _vexDic[id].AddEdge(e);
        }

        public List<Edge> GetEdgesByVex(int v)
        {
            // 返回v相连的边
            return _vexDic[v].GetEdges();
        }

        public Vertex GetVertex(int id)
        {
            if (_vexDic.ContainsKey(id))
            {
                return _vexDic[id];
            }
            else
            {
                return null;
            }
        }

        public List<Edge> GetEdges()
        { 
            // 返回所有边的集合
            List<Edge> edges = new List<Edge>();
            for (int i = 0; i < VexCount; i++)
            {
                foreach (Edge e in _vexDic[i].GetEdges())
                {
                    if (e.GetAnotherVertex(i).ID > i)
                    {
                        edges.Add(e);
                    }
                }
            }
            return edges;
        }

        public override string ToString()
        {
            string s = VexCount + " 个顶点, " + EdgeCount + " 条边\n";
            for (int i = 0; i < VexCount; i++)
            {
                s += i + ": ";
                foreach (Edge e in _vexDic[i].GetEdges())
                {
                    s += e.GetAnotherVertex(i).ID + " [" + e.Weight + "], ";
                }
                s += "\n";
            }
            return s;
        }
    }
}
