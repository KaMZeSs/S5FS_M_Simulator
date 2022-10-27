//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace S5FS
//{
//    internal class Program
//    {
//        static S5FS GetNewFS()
//        {
//            while (true)
//            {
//                Console.WriteLine("Создать новую ФС, или открыть существующую?");
//                Console.WriteLine("1. Создать новую\n2. Открыть существующую");
//                var key = Console.ReadKey(true);
//                switch (key.KeyChar)
//                {
//                    case '1':
//                    {
//                        String name;
//                        do
//                        {
//                            Console.Write("Введите имя файла: ");
//                            name = Console.ReadLine();
//                        }
//                        while (File.Exists(name));

//                        UInt32 block_size = 0;

//                        while (block_size is 0)
//                        {
//                            Console.WriteLine("Выберите размер кластера из предложенных");
//                            Console.WriteLine("1. 512 байт\n2. 1024 байт\n3. 2048 байт\n4. 4096 байт");
//                            key = Console.ReadKey(true);

//                            switch (key.KeyChar)
//                            {
//                                case '1':
//                                {
//                                    block_size = 512;
//                                    break;
//                                }
//                                case '2':
//                                {
//                                    block_size = 1024;
//                                    break;
//                                }
//                                case '3':
//                                {
//                                    block_size = 2048;
//                                    break;
//                                }
//                                case '4':
//                                {
//                                    block_size = 4096;
//                                    break;
//                                }
//                            }
//                        }

//                        UInt64 size = 0;

//                        while (size is 0)
//                        {
//                            Console.WriteLine("Введите размер диска в мб");
//                            Console.WriteLine("Минимум: 20\nМаксимум: 10240");
//                            ulong FreeSpaceLeft = (ulong)System.IO.DriveInfo.GetDrives()
//                                .Where(x => Environment.CurrentDirectory.StartsWith(x.Name)).First().TotalFreeSpace;
//                            try
//                            {
//                                size = UInt64.Parse(Console.ReadLine());
//                                if (size * 1024 * 1024 >= FreeSpaceLeft)
//                                {
//                                    Console.WriteLine("На диске нет места");
//                                }
//                                else if (size > 10240 || size < 20)
//                                    size = 0;
                                
//                            }
//                            catch (Exception) { }
//                        }


//                        try
//                        {
//                            return S5FS.format(name, block_size, size);
//                        }
//                        catch (Exception)
//                        {
//                            Console.WriteLine("Непредвиденная ошибка при создании файла");
//                        }

//                        break;
//                    }
//                    case '2':
//                    {
//                        String name;
//                        do
//                        {
//                            Console.Write("Введите имя файла: ");
//                            name = Console.ReadLine();
//                        }
//                        while (!File.Exists(name));

//                        try
//                        {
//                            return S5FS.load_from_file(name);
//                        }
//                        catch (Exception)
//                        {
//                            Console.WriteLine("Непредвиденная ошибка при открытии файла");
//                            Console.WriteLine("Возможно файл поврежден");
//                        }
                        
//                        break;
//                    }
//                    default:
//                    {
//                        break;
//                    }
//                }
//            }
//        }

//        static void Main(String[] args)
//        {
//            new Emulator(GetNewFS()).Work();
//        } 
//    }
//}
