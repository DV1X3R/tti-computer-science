using System;

namespace Exercise1
{
    public class IntHashArray
    {
        public int[] Values { get; private set; }
        public int[] HashedValues { get; private set; }

        public string Array { get { return ArrToStr(Values); } }
        public string HashedArray { get { return ArrToStr(HashedValues); } }
        private string ArrToStr(int[] array)
        {
            String str = "";
            for (int x = 0; x < array.Length; x++)
                str += array[x] + "; ";

            return str;
        }

        public uint CompOpCounter { get; private set; } = 0;
        public void ResetCompOpCounter() => CompOpCounter = 0;

        public IntHashArray(uint Size, int MinValue, int MaxValue)
        {
            Random rand = new Random();
            Values = new int[Size];
            HashedValues = new int[(int)Math.Round(Size * 1.5)];

            for (int x = 0; x < Values.Length; x++)
                Values[x] = rand.Next(MinValue, MaxValue);
            
            for (int x = 0; x < HashedValues.Length; x++)
                HashedValues[x] = -1;

            for (int x = 0; x < Size; x++)
            {
                int address = Values[x] % HashedValues.Length;

                if (HashedValues[address] == -1)
                    HashedValues[address] = Values[x];
                else
                {
                    while (HashedValues[address] != -1 && HashedValues[address] != Values[x])
                    {
                        if (address < HashedValues.Length - 1)
                            address++;
                        else
                            address = 0;
                    }

                    HashedValues[address] = Values[x];
                }
            }
        }

        public int FindInArray(int Number)
        {
            for (int x = 0; x < Values.Length; x++)
            {
                CompOpCounter += 2;
                if (Values[x] == Number)
                    return x;
            }

            CompOpCounter++;
            return -1;
        }

        public int FindInHashedArray(int Number)
        {
            int startIndex = Number % HashedValues.Length; //A = f(x) = x mod n
            int endIndex = HashedValues.Length;

            // Search from the specific location
            for (int x = startIndex; x < endIndex; x++)
            {
                CompOpCounter += 2;
                if (HashedValues[x] == Number)
                    return x;

                CompOpCounter++;
                if (HashedValues[x] == -1)
                    return -1;
            }

            CompOpCounter++;

            // Search from the begiining if value has not been found on the previous step
            for (int x = 0; x < startIndex; x++)
            {
                CompOpCounter += 2;
                if (HashedValues[x] == Number)
                    return x;

                CompOpCounter++;
                if (HashedValues[x] == -1)
                    return -1;
            }

            CompOpCounter++;
            return -1;
        }

    }
}
