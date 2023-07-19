namespace NumberSorter
{
    class SortArray
    {
        static double min, max, len, arrayLen;
        static int minInt, maxInt, num;
        static int[] array;
        static long timeMili;
        //creates a stopwatch to get times
        System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
        //
        public SortArray(int arrayNum, double percentStart, double percentEnd, int[] intArray)
        {
            //sets the arraylength and the array
            len = intArray.Length;
            num = arrayNum;
            //sets the index it will start on
            min = len * (percentStart / 100);
            max = (len * (percentEnd / 100)) - 1;
            //does some funny math
            minInt = (int)Math.Round(min);
            maxInt = (int)Math.Round(max);
            
            Console.WriteLine("start at index: " + minInt + "\nEnd on Index: " + maxInt);
            
            //gets the new array to be edited and its length
            array = new int[maxInt - minInt + 1];
            arrayLen = array.Length;
            for (int i = minInt; i < maxInt; i++)
            {
                array[i - minInt] = intArray[i];
            }
        }

        public void Run()
        {
            Console.WriteLine("thread Running #" + num);
            //resets and starts a new timer
            time.Restart();
            time.Start();
            bool swapped;
            //does some funny stuff i dont know how it works
            for (int i = 0; i < arrayLen - 1; i++)
            {
                swapped = false;
                for (int s = 0; s < arrayLen - i - 1; s++)
                {
                    if (array[s] > array[s + 1])
                    {
                        int temp = array[s];
                        array[s] = array[s + 1];
                        array[s + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped)
                {
                    break;
                }
            }

            //stops the timer and lists how long it took for the thread to finish
            time.Stop();
            timeMili = time.ElapsedMilliseconds;
            Console.WriteLine("Thread #" + num);
            Console.WriteLine("Miliseconds: " + timeMili);
            Console.WriteLine("Array Size: " + array.Length);

        }
        //cuz goofy stuff i needed this
        public int[] GetArray() 
        { return array; }
       

    }
}
