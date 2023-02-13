using System.Threading;



namespace ThreadsApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyObject myObject = new MyObject(Int32.Parse(Console.ReadLine()));
            myObject.Start = Int32.Parse(Console.ReadLine());
            myObject.End = Int32.Parse(Console.ReadLine());
            ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(ConsoleWrite);
            /*ThreadStart threadStart = new ThreadStart(ConsoleWrite);//применяется к первой задаче
            Thread thread = new Thread(threadStart);*/
            Thread thread = new Thread(parameterizedThreadStart);
            for(int i=0; i<myObject.threads.Length;i++)
            {
                myObject.threads[i] = new Thread(parameterizedThreadStart);
            }
            for (int i = 0; i < myObject.threads.Length; i++)
            {
                myObject.threads[i].Start(myObject);
                myObject.threads[i].Join();
            }

            Random random= new Random();
            int[] massive = new int[10000];
            for(int i=0; i<10000; i++)
            {
                massive[i] = random.Next(15000);
            }
            ParameterizedThreadStart alltasks = new ParameterizedThreadStart(TaskDelegate);
            Thread threadall= new Thread(alltasks);
            threadall.Start(massive);
            threadall.Join();


            //thread.IsBackground = true;
            //thread.Priority = ThreadPriority.Lowest;
            //thread.Start(myObject);
            //thread.Join();
            /*for (int i = 0; i <= 50; i++)
            {
                Console.WriteLine("Из основной программы: " + i);
            }*/
        }
        /* static void ConsoleWrite()
        {
           Console.WriteLine("Введите число начала массива");//первый вариант вывода заданого диапазона потока
            int istart = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите число конца массива");
            int iend = Int32.Parse(Console.ReadLine());*/
        /* for (int i = 0; i <= 50; i++)
         {
             if(i==25)
             {
                 Console.ReadLine();
             }
             Console.WriteLine("Из потока: " + i);
        }
         }*/
        /*static void ConsoleWrite(object istart) //вторая задача
        {
            
            for (int i = (int)istart; i <= 50; i++)
            {
                Console.WriteLine("Из потока: " + i);
            }
        }*/

        static void ConsoleWrite(object my)
        {
            int start = ((MyObject)my).Start;
            int end = ((MyObject)my).End;
            string message = ((MyObject)my).Message;
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine("Из потока: " + i);
            }
            Console.WriteLine();
        }
        static void TaskDelegate(object massive)
        {
            ParameterizedThreadStart maxelemstart = new ParameterizedThreadStart(MaxElem);
            ParameterizedThreadStart minelemstart = new ParameterizedThreadStart(MinElem);
            ParameterizedThreadStart averageelemstart = new ParameterizedThreadStart(AverageElem);
            Thread thread1 = new Thread(maxelemstart);
            Thread thread2 = new Thread(minelemstart);
            Thread thread3 = new Thread(averageelemstart);
            thread1.Start(massive);
            thread2.Start(massive);
            thread3.Start(massive);
        }
        static void MaxElem(object massive)
        {
            
                Console.WriteLine(((int[])massive).Max().ToString());
            Console.WriteLine("Это читерство");
        }
        static void MinElem(object massive)
        {
                Console.WriteLine(((int[])massive).Min().ToString());
        }
        static void AverageElem(object massive)
        {
                Console.WriteLine(((int[])massive).Average().ToString());
        }

    }
    class MyObject
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string Message { get; set; } = "Это читерство";
       public Thread[] threads;
       public MyObject(int countThread)
        {
            if(countThread>0)
            {
                threads = new Thread[countThread];
            }
            else
            {
                threads = new Thread[1];
            }
        }
    }
}