using System.Diagnostics;

namespace NumberSorter
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        //sets a constant for where the text file will go
        private const string txtFile = "C:\\temp\\SortingTest.txt";

        static long totalTime, bubbleTime, averageTime;

        public void ChangeText(string Text, int percent)
        {
            //i needed a way to easily change the text and progress bar from inside other threads
            Invoke(() =>
            {
                lblTesting.Text = Text;
                progressBar1.Value = percent;
            });

        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            //runs the threads so the main program doesnt lock up running the tests
            Thread tests = new Thread(AllTests);
            tests.Start();
        }
        public void AllTests()
        {
            //allows for easy change of array size but the ammount tests are a constant 
            const int tests = 10;
            int arraySize = (int)numSize.Value;

            //the average times from the single sort and thread sort 
            long avgSingle = sortBubbleTests(tests, arraySize);
            long avgThread = sortThreadedTests(tests, arraySize);

            ChangeText("Finished", tests * 2);

            //writes the final bits of info needed for the text file
            StreamWriter bwR = File.AppendText(txtFile);
            double efficencyPercent = ((double)avgSingle / (double)avgThread) * 100;
            bwR.Write("\nTests = " + tests + " Array Size = " + arraySize + "\nAverage Single Sort = " + avgSingle.ToString("N") + " milliseconds\nAverage Threaded Sort = " + avgThread.ToString("N") + " milliseconds"
                        + "\nThreaded Sort effeiceny compared to single sort Percent: " + efficencyPercent.ToString("N") + "%");
            bwR.Write("\n-------------------------------------------------------------------------------");
            bwR.Close();

            //used to just open the file once the test is done
            Process.Start("notepad.exe", txtFile);
        }
        public long sortBubbleTests(int tests, int arraySize)
        {
            bubbleTime = 0;
            averageTime = 0;
            Console.WriteLine("Bubble Tests");
            //opens a writer for the bubble tests
            StreamWriter bwR = File.AppendText(txtFile);
            //runs the test 10 times
            for (int t = 1; t <= tests; t++)
            {
                //changes the text and progress bar 
                ChangeText("Bubble Test # " + t, t);
                Console.WriteLine("Test # " + t);

                //creates an array with random numbers
                int[] nums = new int[arraySize];
                Random r = new Random();
                for (int i = 0; i < arraySize; i++)
                {
                    nums[i] = r.Next(arraySize) + 1;
                    //Console.WriteLine(nums[i]);
                }
                Console.WriteLine("orginal array: " + nums);

                //runs the bubble sort
                bubbleSort(nums);
                //writes the tests stats to the console
                Console.WriteLine("sorted array: " + nums);
                bwR.Write("\nTest " + t + "\nBubble Sort time: " + bubbleTime.ToString("N") + "Milliseconds OR " + (bubbleTime / 1000) + " Seconds");

                //adds the time it took to do this test
                averageTime += bubbleTime;

            }
            //calculates and writes the stats for the bubble tests
            averageTime = averageTime / tests;
            bwR.Write("\n-------------------------------------------------------------------------------");
            bwR.Write("\nArray Size = " + arraySize + "\nTests = " + tests + "\nAverage Time = " + averageTime.ToString("N") + " milliseconds OR " + (averageTime / 1_000) + " Seconds");
            bwR.Write("\n-------------------------------------------------------------------------------");
            bwR.Close();
            //returns the average time of the tests
            return averageTime;

        }
        public long sortThreadedTests(int tests, int arraySize)
        {
            totalTime = 0;
            averageTime = 0;
            //opens the writer for the threaded tests
            StreamWriter bwR = File.AppendText(txtFile);
            Console.WriteLine("Threaded Tests");

            //does the test 10 times
            for (int t = 1; t <= tests; t++)
            {
                //changes the text and progress bar
                ChangeText("Threaded Test # " + t, t + tests);
                Console.WriteLine("Test # " + t);

                //creates a array with random numbers
                int[] nums = new int[arraySize];
                Random r = new Random();
                for (int i = 0; i < arraySize; i++)
                {
                    nums[i] = r.Next(arraySize) + 1;
                }
                //this is needed since it is split into 2 arrays
                int[] numsOrg = nums;

                Console.WriteLine("orginal array: " + nums);
                Console.WriteLine("Array Size: " + nums.Length);

                //creates a stop watch and starts the count
                System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
                time.Restart();
                time.Start();

                //creates 2 threadlings containing information
                SortArray threadling1 = new SortArray(1, 0, 50, nums);
                SortArray threadling2 = new SortArray(2, 50, 100, nums);
                //turns the threadlings into threads that can start
                Thread h1 = new Thread(new ThreadStart(threadling1.Run));
                Thread h2 = new Thread(new ThreadStart(threadling2.Run));
                //starts the sorting
                h1.Start();
                h2.Start();
                //waits until both threads are done
                h1.Join();
                h2.Join();
                Console.WriteLine("All threads are complete");

                //merges the 2 arrays into 1 array
                nums = mergeSort(threadling1.GetArray(), threadling2.GetArray());
                Console.WriteLine("sorted array: " + nums);

                //time stops and saves the time
                time.Stop();
                totalTime = time.ElapsedMilliseconds;
                bwR.Write("\nTest " + t + "\nTotal Time: " + totalTime.ToString("N") + " milliseconds OR " + (totalTime / 1_000) + " Seconds");
                //adds time to be calcualated
                averageTime += totalTime;
            }
            //calculates and writes the stats for the threaded test
            averageTime = averageTime / tests;
            bwR.Write("\n-------------------------------------------------------------------------------");
            bwR.Write("\nArray Size = " + arraySize + "\nTests = " + tests + "\nAverage Time = " + averageTime + " milliseconds OR " + (averageTime / 1_000) + " Seconds");
            bwR.Write("\n-------------------------------------------------------------------------------");
            bwR.Close();
            //returns the average time it took
            return averageTime;
        }
        public static int[] mergeSort(int[] array1, int[] array2)
        {
            //takes the 2 arrays and gets their lenghths
            int len1 = array1.Length;
            int len2 = array2.Length;
            //merges the 2 arrays
            int[] result = new int[len1 + len2];
            int o = 0, t = 0, r = 0;

            //sorts the 2 arrays into 1
            while (o < len1 && t < len2)
            {
                if (array1[o] <= array2[t])
                {
                    result[r++] = array1[o++];
                }
                else
                {
                    result[r++] = array2[t++];
                }
            }

            while (o < len1)
            {
                result[r++] = array1[o++];
            }

            while (t < len2)
            {
                result[r++] = array2[t++];
            }

            Console.WriteLine("Merge sort time & Array Size: " + result.Length);
            //returns the fully merged and sorted array
            return result;
        }
        public static void bubbleSort(int[] array)
        {
            //creates a stopwatch and starts the count
            System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
            time.Reset();
            time.Start();
            //gets the length of the array
            int len = array.Length;

            //does funny stuff i dont rember
            bool swapped;
            for (int i = 0; i < len - 1; i++)
            {
                swapped = false;
                for (int s = 0; s < len - i - 1; s++)
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
            //stops the timer and sets the bubble time 
            time.Stop();
            bubbleTime = time.ElapsedMilliseconds;

            Console.WriteLine("BubbleSort Time: " + bubbleTime.ToString("N") + "Milliseconds OR " + (bubbleTime / 1000).ToString("N") + " Seconds");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
            Application.Exit();
        }
    }
}