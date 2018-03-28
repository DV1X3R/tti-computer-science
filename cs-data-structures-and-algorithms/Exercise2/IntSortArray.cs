using System;

namespace Exercise2
{
    public class IntSortArray
    {
        public int[] Values { get; private set; }
        public int[] SortedValues { get; private set; }

        public string Array { get { return ArrToStr(Values); } }
        public string SortedArray { get { return ArrToStr(SortedValues); } }
        private string ArrToStr(int[] array)
        {
            String str = "";
            for (int x = 0; x < array.Length; x++)
                str += array[x] + "; ";

            return str;
        }

        public uint CompOpCounter { get; private set; } = 0;
        public void ResetCompOpCounter() => CompOpCounter = 0;

        public IntSortArray(uint Size, int MinValue, int MaxValue)
        {
            Random rand = new Random();
            Values = new int[Size];
            SortedValues = new int[Size];

            for (int x = 0; x < Values.Length; x++)
                Values[x] = rand.Next(MinValue, MaxValue);
        }

        public void SortArrayShell()
        {
            Values.CopyTo(SortedValues, 0);

            int n = SortedValues.Length;
            int t = (int)Math.Log((double)n) - 1; //количество расстояний
            int[] d = new int[t];
            d[0] = 1;

            for (int i = 1; i < t; i++)
            {
                CompOpCounter++;
                d[i] = 2 * d[i - 1] + 1;
            }

            CompOpCounter++;

            //формирование последовательности di по первому варианту 
            for (int m = t - 1; m >= 0; m--)
            {
                CompOpCounter++;

                int k = d[m]; //выбор текущего расстояния 

                for (int i = k; i < n; i++)
                {
                    CompOpCounter++;

                    int x = SortedValues[i]; //запомнить вставляемый ключ 
                    int j = i - k;

                    //сравнение элементов, находящихся на расстоянии d с вставляемым ключом 
                    while (j >= 0)
                    {
                        CompOpCounter += 2;
                        if (x < SortedValues[j])
                        {
                            SortedValues[j + k] = SortedValues[j];
                            j = j - k;
                        }
                        else
                            break;
                    }

                    CompOpCounter++;
                    SortedValues[j + k] = x; //вставка ключа 

                }
                CompOpCounter++;
            }

            CompOpCounter++;
        }

        public void SortArrayRadix()
        {
            Values.CopyTo(SortedValues, 0);

            int[] t = new int[SortedValues.Length];
            int r = 4;
            int b = 32;
            int[] count = new int[1 << r];
            int[] pref = new int[1 << r];
            int groups = (int)Math.Ceiling((double)b / (double)r);
            int mask = (1 << r) - 1;

            for (int c = 0, shift = 0; c < groups; c++, shift += r)
            {
                CompOpCounter++;

                for (int j = 0; j < count.Length; j++)
                {
                    CompOpCounter++;
                    count[j] = 0;
                }
                CompOpCounter++;

                for (int i = 0; i < SortedValues.Length; i++)
                {
                    CompOpCounter++;
                    count[(SortedValues[i] >> shift) & mask]++;
                }
                CompOpCounter++;

                pref[0] = 0;

                for (int i = 1; i < count.Length; i++)
                {
                    CompOpCounter++;
                    pref[i] = pref[i - 1] + count[i - 1];
                }
                CompOpCounter++;

                for (int i = 0; i < SortedValues.Length; i++)
                {

                    CompOpCounter++;
                    t[pref[(SortedValues[i] >> shift) & mask]++] = SortedValues[i];
                }
                CompOpCounter++;

                t.CopyTo(SortedValues, 0);
            }
            CompOpCounter++;
        }

    }
}
