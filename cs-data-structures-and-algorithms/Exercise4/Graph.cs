using System;
using System.Collections.Generic;

namespace Exercise4
{
    public class Graph
    {
        public int[,] IncidenceList { get; private set; }
        public char[] SourceName { get; private set; }
        public char[] SortedName { get; private set; }

        private List<int> path { get; set; } = new List<int>();
        public readonly IReadOnlyList<int> Path;

        public Graph(int[,] GraphIncidenceList, char[] GraphSourceName)
        {
            IncidenceList = GraphIncidenceList;
            SourceName = GraphSourceName;
            SortedName = new char[SourceName.Length];
            Path = path.AsReadOnly();
        }

        public void ShowIncidenceList()
        {
            for (int i = 0; i < IncidenceList.GetLength(0); i++)
            {
                Console.Write(i + ": ");
                for (int n = 0; n < IncidenceList.GetLength(1); n++)
                {
                    if (IncidenceList[i, n] == -1)
                        break;

                    if (n != 0)
                        Console.Write(" -> ");

                    Console.Write(IncidenceList[i, n]);
                }
                Console.Write("\n");
            }
        }

        public void FillPathWidth()
        {
            path.Clear();

            //Шаг 1. Всем вершинам графа присваивается значение не посещенная. Выбирается первая вершина и заносится в очередь.
            bool[] vertex = new bool[IncidenceList.GetLength(0)];
            List<int> stack = new List<int>() { 0 };

            while (stack.Count != 0)
            {
                if (!vertex[stack[0]])
                {
                    //Шаг 2. Посещается первая вершина из очереди (если она не помечена как посещенная).
                    vertex[stack[0]] = true;
                    path.Add(stack[0]);

                    //Все ее соседние вершины заносятся в очередь.
                    for (int i = 0; i < IncidenceList.GetLength(1); i++)
                    {
                        if (IncidenceList[stack[0], i] == -1) break;
                        stack.Add(IncidenceList[stack[0], i]);
                    }
                }

                //После этого она удаляется из очереди.
                stack.RemoveAt(0);
            }
            //Шаг 3. Повторяется шаг 2 до тех пор, пока очередь не пуста 
        }

        public void SortName()
        {
            for (int i = 0; i < path.Count; i++)
                SortedName[path[i]] = SourceName[i];
        }

        public void ShowPath()
        {
            for (int i = 0; i < path.Count; i++)
            {
                Console.Write(path[i]);

                if (i != path.Count - 1)
                    Console.Write(" -> ");
            }
            Console.Write("\n");
        }

        public void ShowSourceName() => ShowName(SourceName);
        public void ShowSortedName() => ShowName(SortedName);

        private void ShowName(char[] Name)
        {
            for (int i = 0; i < path.Count; i++)
                Console.Write(Name[path[i]]);
            Console.Write("\n");
        }

    }
}
