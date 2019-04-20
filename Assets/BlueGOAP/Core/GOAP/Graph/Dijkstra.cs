
using System.Collections.Generic;
using UnityEngine;

namespace BlueGOAP
{
    /// <summary>
    /// 迪杰斯特拉算法
    /// </summary>
    public class ShortestPathDijkstra
    {
        //标记顶点是否已经遍历过
        private int[] _markList;
        //记录起始点到每个点的权重
        private float[] _weight;
        //记录路径
        private int[] _path;

        /// <summary>
        /// Dijkstrah实现最短路径算法
        /// </summary>
        public void GetShortestPath(Graph graph, int startId, int endId)
        {
            int vexCount = graph.VexCount;

            //初始化数据
            Init(vexCount, graph, startId);

            //初始化起始点，它到它自身的权重为0
            _weight[startId] = 0;
            //初始化起始点，标记为已遍历
            _markList[startId] = 1;

            for (int i = 0; i < vexCount; i++)
            {
                float minWeight = float.MaxValue;
                int nextVexId = startId;

                //获取未遍历过的最小权重及其节点Id
                GetWeightAndNextId(vexCount, ref minWeight, ref nextVexId);

                //标记为已遍历
                _markList[nextVexId] = 1;

                //判断是否已找到路径
                if (JudgeEnd(nextVexId, startId, endId))
                    break;

                SetCheapestWeight(graph, vexCount, nextVexId, minWeight);
            }
        }

        /// <summary>
        /// 初始化数组的初始数据
        /// </summary>
        /// <param name="vexCount"></param>
        /// <param name="graph"></param>
        /// <param name="startId"></param>
        private void Init(int vexCount, Graph graph, int startId)
        {
            _markList = new int[vexCount];
            _weight = new float[vexCount];
            _path = new int[vexCount];

            for (int i = 0; i < vexCount; i++)
            {
                if (graph.GetVertex(startId).GetEdge(i) == null)
                {
                    _weight[i] = float.MaxValue;
                }
                else
                {
                    _weight[i] = graph.GetVertex(startId).GetEdge(i).Weight;
                }

                _markList[i] = -1;
                _path[i] = -1;
            }
        }
        /// <summary>
        /// 获取未遍历过的最小权重及其节点Id
        /// </summary>
        /// <param name="vexCount"></param>
        /// <param name="minWeight"></param>
        /// <param name="nextVexId"></param>
        private void GetWeightAndNextId(int vexCount,ref float minWeight,ref int nextVexId)
        {
            for (int i = 0; i < vexCount; i++)
            {
                //取出未遍历过，且权重最小的值
                if (_markList[i] < 0 && (_weight[i] < minWeight))
                {
                    minWeight = _weight[i];
                    nextVexId = i;
                }
            }
        }

        /// <summary>
        /// 判断是否已找到路径
        /// </summary>
        /// <param name="nextVexId"></param>
        /// <param name="startId"></param>
        /// <param name="endId"></param>
        /// <returns></returns>
        private bool JudgeEnd(int nextVexId,int startId,int endId)
        {
            if (nextVexId == endId)
            {
                List<int> path = new List<int>();
                path.Add(startId);
                int index = nextVexId;
                for (int i = 0; i < _path.Length; i++)
                {
                    if (_path[index] < 0)
                        break;
                    index = _path[index];
                    path.Add(index);
                }
                path.Add(endId);
                return true;
            }

            return false;
        }
        /// <summary>
        /// 遍历所有顶点，如果发现到顶点更小的权重，就修改_weight中存储的值
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="vexCount"></param>
        /// <param name="nextVexId"></param>
        /// <param name="minWeight"></param>
        private void SetCheapestWeight(Graph graph, int vexCount,int nextVexId,float minWeight)
        {
            float weight = 0;
            Vertex tempVertex = null;

            for (int i = 0; i < vexCount; i++)
            {
                if (_markList[i] < 0)
                {
                    //获取nextVexId对应的顶点对象
                    tempVertex = graph.GetVertex(nextVexId);
                    //查到当前的顶点，是否有到ID=i的顶点的边
                    if (tempVertex != null && tempVertex.GetEdge(i) != null)
                    {
                        //存在这条边
                        //这条边的权重加上当前的minWeight
                        //就是当前的起点到ID = i的顶点的总权重
                        weight = tempVertex.GetEdge(i).Weight + minWeight;
                    }
                    else
                    {
                        //不存在，把权重设成最大值
                        weight = float.MaxValue;
                    }

                    //如果，当前获取到的权重，要低于保存的
                    //起始点到ID=i的点的权重
                    if (weight < _weight[i])
                    {
                        //那么就把数组中的权重更新一下
                        _weight[i] = weight;
                        //保存当前顶点id到路径数组中
                        _path[i] = nextVexId;
                    }
                }
            }
        }
    }
}