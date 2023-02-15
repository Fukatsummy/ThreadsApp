using System.Threading;
using System.Text;
using System.ComponentModel;



namespace ThreadsApp
{
    internal class Program
    {
       static ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(SaveDataToFile);
       
       
        static void Main(string[] args)
        {
            /*MyObject myObject = new MyObject(Int32.Parse(Console.ReadLine()));
            myObject.Start = Int32.Parse(Console.ReadLine());
            myObject.End = Int32.Parse(Console.ReadLine());
            ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(ConsoleWrite);
            /*ThreadStart threadStart = new ThreadStart(ConsoleWrite);//применяется к первой задаче
            Thread thread = new Thread(threadStart);*/
           /* Thread thread = new Thread(parameterizedThreadStart);
            for(int i=0; i<myObject.threads.Length;i++)
            {
                myObject.threads[i] = new Thread(parameterizedThreadStart);
            }
            for (int i = 0; i < myObject.threads.Length; i++)
            {
                myObject.threads[i].Start(myObject);
                myObject.threads[i].Join();
            }*/

            Random random= new Random();//task 4
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

        static void ConsoleWrite(object my) //task 1-2
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
        static void TaskDelegate(object massive)// task 4-5
        {
            ParameterizedThreadStart maxelemstart = new ParameterizedThreadStart(MaxElem);
            ParameterizedThreadStart minelemstart = new ParameterizedThreadStart(MinElem);
            ParameterizedThreadStart averageelemstart = new ParameterizedThreadStart(AverageElem);
            Thread thread1 = new Thread(maxelemstart);
            Thread thread2 = new Thread(minelemstart);
            Thread thread3 = new Thread(averageelemstart);
            thread1.Start(massive);
            thread1.Join();
            thread2.Start(massive);
            thread2.Join();
            thread3.Start(massive);
            thread3.Join();
        }
        /*static void MaxElem(object massive)
        {
            
            Console.WriteLine(((int[])massive).Max().ToString());
            Console.WriteLine("Это читерство");
        }*/
        static void MaxElem(object massive)
        {
            int result = ((int[])massive).Max();
            Console.WriteLine(result.ToString());
            Thread thread = new Thread(parameterizedThreadStart);
            thread.Start(result);
        }
        /*static void MinElem(object massive)
        {
                Console.WriteLine(((int[])massive).Min().ToString());
        }*/
        static void MinElem(object massive)
        {
             int result = ((int[])massive).Min();
            Console.WriteLine(result.ToString());
            Thread thread = new Thread(parameterizedThreadStart);
            thread.Start(result);
        }
        /*static void AverageElem(object massive)
        {
                Console.WriteLine(((int[])massive).Average().ToString());
        }*/
        static void AverageElem(object massive)
        {
            double result = ((int[])massive).Average();
            Console.WriteLine(result.ToString());
            Thread thread = new Thread(parameterizedThreadStart);
            thread.Start(result);
        }
         static void SaveDataToFile(object number)
        {
            FileStreamOptions options= new FileStreamOptions();
            options.Access = FileAccess.Write;
            options.Share = FileShare.Write;
            options.Mode = FileMode.OpenOrCreate;
           StreamWriter writer = new StreamWriter("task5.txt", Encoding.UTF8, options);
            writer.WriteLine(number.ToString());
            writer.Close();
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