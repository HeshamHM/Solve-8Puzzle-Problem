using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8_Puzzle
{
     class Node
    {
        public int x { set; get; }
        public int y { set; get; }
        public int[,] gp { set; get; }
    }
    public class MyEqualityComparer : IEqualityComparer<int[,]>
    {
        public bool Equals(int[,] x, int[,] y)
        {
            for (int i = 0; i <3; i++)
                for(int j=0;j<3;j++)
                if (x[i,j] != y[i,j])
                {
                    return false;
                }
            
            return true;
        }

        public int GetHashCode(int[,] obj)
        {
            int result = 17;
            for (int i = 0; i < 3; i++)
            {
                for(int j=0;j<3;j++)
                unchecked
                {
                    result = result * 23 + obj[i,j];
                }
            }
            return result;
        }
    }
    class Graph
    {
        private int[,] grid;
        private int[,] target=new int[3,3] { { 1, 2,3 },
                                             { 4, 5,6 }, 
                                             { 7,8, 0 } };
       // private int[,] target1 = new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };
        private int n;
        private int imove, jmove;
        private int []linear;
        private int count =0;
        public Graph(int n)
        {
            this.n= n;
            grid = new int[n,n];
            linear = new int[n*n];
        }
    
        public void Build(int x ,int y,int num)
        {
            if (num == 0)
            { imove = x; jmove = y; }
            grid[x, y] = num;
            linear[count++]=num;
        }

        private bool IsEqual(int[,] data1, int[,] data2)
        {
            var equal =
            data1.Rank == data2.Rank &&
            Enumerable.Range(0, data1.Rank).All(dimension => data1.GetLength(dimension) == data2.GetLength(dimension)) &&
            data1.Cast<int>().SequenceEqual(data2.Cast<int>());
            return equal;
        }
        private int inversion()
        {
            int inv=0;
            for(int i=0;i<(n*n)-1;i++)
            {
                for (int j = i+1; j < n*n; j++)
                    if (linear[i] != 0 && linear[j] != 0 && linear[i] > linear[j])
                        inv++;
              
            }
            return inv;
        }
        private int bestcost(int [,]arr)
        {
            int num = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (arr[i, j] != target[i, j])
                        num++;
            return num;

        }
        public Stack<Node> BFS()
        {
            Stack<Node> st = new Stack<Node>();
            Queue<Node> que = new Queue<Node>();
            que.Enqueue(new Node { x = imove, y = jmove, gp = grid});
            Dictionary<int[,], Node> way = new Dictionary<int[,], Node>();
            Dictionary<int[,],bool> visted = new Dictionary< int[,],bool>(new MyEqualityComparer());
            visted.Add(grid,true);
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };
            while (que.Count != 0)
            {
                  var curr = que.Dequeue();
                  for (int i = 0; i < 4; i++)
                  {
                        int new_x = curr.x +dx[i] ;
                        int new_y = curr.y + dy[i];
                    if (new_x < n && new_x >= 0 && new_y < n && new_y >= 0)
                    {
                        var temp = curr.gp;
                        int[,] arr = new int[n, n];
                        int num = temp[curr.x, curr.y];
                        int num1 = temp[new_x, new_y];
                        for (int ii = 0; ii < n; ii++)
                            for (int j = 0; j < n; j++)
                                if (ii == new_x && j == new_y)
                                    arr[ii, j] = num;
                                else if (ii == curr.x && j == curr.y)
                                    arr[ii, j] = num1;
                                else
                                    arr[ii, j] = temp[ii, j];

                        if (solve(arr))
                        {
                            if (!visted.ContainsKey(arr))
                            {
                                way.Add(arr, curr);
                                visted.Add(arr, true);
                                que.Enqueue(new Node { x = new_x, y = new_y, gp = arr });
                                
                                if (bestcost(arr) == 0)
                                {
                                    st.Push(new Node { x = new_x, y = new_y, gp = arr });
                                    target = arr;
                                    while (true)
                                    {
                                        Node node = new Node();
                                        node = way[target];

                                        if (IsEqual(grid, way[target].gp))
                                            break;
                                        st.Push(node);
                                        target = node.gp;
                                    }

                                    return st;
                                }

                            }
                          
                        }
                    }
                  
                  }

                
            }
            return st;
           
        }
        public Stack<Node> ASTAR()
        {
            List<KeyValuePair<int, Node>>list=new List<KeyValuePair<int,Node>>();
            list.OrderBy(o => o.Key);
            Stack<Node> st = new Stack<Node>();
            Queue<Node> que = new Queue<Node>();
           list.Add(new KeyValuePair<int,Node>(bestcost(grid),new Node { x = imove, y = jmove, gp = grid }));
            Dictionary<int[,], Node> way = new Dictionary<int[,], Node>();
            Dictionary<int[,], bool> visted = new Dictionary<int[,], bool>(new MyEqualityComparer());
            visted.Add(grid, true);
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };
            while (list.Count != 0)
            {
                var curr = list[0];
                list.RemoveAt(0);
                for (int i = 0; i < 4; i++)
                {
                    int new_x = curr.Value.x + dx[i];
                    int new_y = curr.Value.y + dy[i];
                    if (new_x < n && new_x >= 0 && new_y < n && new_y >= 0)
                    {
                        var temp = curr.Value.gp;
                        int[,] arr = new int[n, n];
                        int num = temp[curr.Value.x, curr.Value.y];
                        int num1 = temp[new_x, new_y];
                        for (int ii = 0; ii < n; ii++)
                            for (int j = 0; j < n; j++)
                                if (ii == new_x && j == new_y)
                                    arr[ii, j] = num;
                                else if (ii == curr.Value.x && j == curr.Value.y)
                                    arr[ii, j] = num1;
                                else
                                    arr[ii, j] = temp[ii, j];

                        if (solve(arr))
                        {
                            if (!visted.ContainsKey(arr))
                            {
                                way.Add(arr, curr.Value);
                                visted.Add(arr, true);
                                list.Add(new KeyValuePair<int, Node>(bestcost(arr), new Node { x = new_x, y = new_y, gp = arr }));
                                if (bestcost(arr) == 0)
                                {
                                    st.Push(new Node { x = new_x, y = new_y, gp = arr });
                                    target = arr;
                                    while (true)
                                    {
                                        Node node = new Node();
                                        node = way[target];

                                        if (IsEqual(grid, way[target].gp))
                                            break;
                                        st.Push(node);
                                        target = node.gp;
                                    }
                                    return st;
                                }
                            }
                        }
                    }

                }


            }
            return st;

        }
        public  bool solve()
        {
            return (inversion() % 2 == 0);
        }
        private bool solve(int [,] temp)
        {
            int counts = 0;
            int[] lin = new int[n * n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    lin[counts++] = temp[i, j];
            return (inversion(lin) % 2 == 0);
        }
        private int inversion(int []lin)
        {
            int inv = 0;
            for (int i = 0; i < (n * n) - 1; i++)
            {
                for (int j = i + 1; j < n * n; j++)
                    if (lin[i] != 0 && lin[j] != 0 && lin[i] > lin[j])
                        inv++;

            }
            return inv;
        }

    }
}
