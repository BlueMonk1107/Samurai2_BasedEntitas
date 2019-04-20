
using System;

namespace BlueGOAP
{
    /// <summary>
    /// 图的边
    /// </summary>
    public class Edge
    {
        //图的两个顶点
        public Vertex StartVex { get; private set; }
        public Vertex EndVex { get; private set; }
        // 边的权重
        public float Weight { get; set; }

        public Edge(Vertex startVex, Vertex endVex, float weight)
        {
            StartVex = startVex;
            EndVex = endVex;
            Weight = weight;
        }

        public Vertex GetAnotherVertex(int id)
        {
            if (StartVex.IsThisVertex(id))
                return EndVex;
            if (EndVex.IsThisVertex(id))
                return StartVex;
            return null;
        }

        public int CompareTo(Edge e)
        {
            // 根据权重比较
            if (Weight > e.Weight) return 1;
            else if (Weight < e.Weight) return -1;
            return 0;
        }
        /// <summary>
        /// 获取反向边
        /// </summary>
        public Edge GetReverseEdge()
        {
            return new Edge(EndVex, StartVex,Weight);
        }

        public override string ToString()
        {
            return StartVex + " to " + EndVex + ", _vexBeight: " + Weight;
        }
    }
}
