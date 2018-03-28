using System;

namespace Exercise3
{
    public class IntList
    {
        public int[] Values { get; private set; }           // Массив данных
        public int[] LeftIndexes { get; private set; }      // Массив индексов при движении <-
        public int[] RightIndexes { get; private set; }     // Массив индексов при движении ->
        public int ToLeftStart { get; private set; }        // Начальный индекс при движении <-
        public int ToRightStart { get; private set; }       // Начальный индекс при движении ->

        public IntList(uint Size, int MinValue, int MaxValue)
        {
            int n = (int)Size;  // Количество элементов
            int c = n * 2;      // Количество ячеек
            ToLeftStart = n - 1;
            ToRightStart = 0;

            Values = new int[c];
            LeftIndexes = new int[c];
            RightIndexes = new int[c];

            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                Values[i] = rand.Next(MinValue, MaxValue);
                LeftIndexes[i] = (i - 1 >= 0) ? i - 1 : -1;
                RightIndexes[i] = (i + 1 >= n) ? -1 : i + 1;
            }
        }

        public void RunRight()
        {
            for (int i = ToRightStart; i != -1; i = RightIndexes[i])
                Console.WriteLine("index: {0}\tl: {1}\tr: {2}\t\tvalue: {3}",
                                  i, LeftIndexes[i], RightIndexes[i], Values[i]);
        }

        public void Insert(uint Position, int Value)
        {
            // Searching for the untaken array spot
            int newIndex = -1;
            bool[] indexStates = new bool[Values.Length];

            for (int i = 0; i < indexStates.Length; i++)
                indexStates[i] = false;

            for (int i = ToRightStart; i != -1; i = RightIndexes[i])
                indexStates[i] = true;

            for (int i = 0; i < indexStates.Length; i++)
                if (indexStates[i] == false)
                {
                    newIndex = i;
                    break;
                }

            // Searching for the element to move
            int prevIndex = ToRightStart;

            for (int i = 0; i < Position && prevIndex != -1; i++)
                prevIndex = RightIndexes[prevIndex];

            // Filling the new array element
            Values[newIndex] = Value;
            RightIndexes[newIndex] = prevIndex;
            LeftIndexes[newIndex] = prevIndex == -1 ? ToLeftStart : LeftIndexes[prevIndex];

            // Update right index of the element on the left
            if (LeftIndexes[newIndex] != -1)
                RightIndexes[LeftIndexes[newIndex]] = newIndex;

            // Update left index of the element on the right (previous)
            if (prevIndex != -1)
                LeftIndexes[prevIndex] = newIndex;

            // Update beginning / ending of the list
            if (LeftIndexes[newIndex] == -1)
                ToRightStart = newIndex;
            else if (RightIndexes[newIndex] == -1)
                ToLeftStart = newIndex;
        }

        public void Remove(uint Position)
        {
            int index = ToRightStart;
            for (int i = 0; i < Position; i++)
                index = RightIndexes[index];

            RightIndexes[LeftIndexes[index]] = RightIndexes[index];
            LeftIndexes[RightIndexes[index]] = LeftIndexes[index];
        }

        public double Average
        {
            get
            {
                int sum = 0;
                int count = 0;

                for (int i = ToRightStart; i != -1; i = RightIndexes[i])
                {
                    sum += Values[i];
                    count++;
                }

                return (double)sum / count;
            }
        }

    }
}
