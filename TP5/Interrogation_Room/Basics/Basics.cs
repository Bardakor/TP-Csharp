using System;
using System.IO;
using System.Collections;

namespace Basics
{
    public class Basics
    {
        public static void FileOrDir(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine(path + " is a file");
            }
            else if (Directory.Exists(path))
            {
                Console.WriteLine(path + " is a directory");
            }
            else
            {
                Console.WriteLine(path + " is neither a file or directory");
            }
        }

        public static void DisplayFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine("Error: \"{0}\" does not exist", path);
            }

            StreamReader myReader = File.OpenText(path);
            int lineNumber = 0;
            string line;

            while ((line = myReader.ReadLine()) != null)
                Console.WriteLine("Line " + lineNumber++ + ": " + line);

            myReader.Close();

        }

        public static void WriteInFile(string path, string content)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(content);
            }
        }

        public static int CopyFile(string source, string dest)
        {
            if (!File.Exists(source))
            {
                return -1;
            }
            if (!File.Exists(dest))
            {
                File.Create(dest);
            }

            using (StreamReader sr = File.OpenText(source))
            {
                using (StreamWriter sw = File.AppendText(dest))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sr.ReadLine();
                        sr.ReadLine();
                        sw.WriteLine(line);
                    }
                }
            }
            return 0;

        }

        public static void PrintReformStr(string path)
        {
            throw new NotImplementedException();
        }


        public static void MiniLs(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.Error.WriteLine("Error: No such file or directory");
            }
            else
            {
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    Console.Write(Path.GetFileName(file) + " ");
                }
            }
        }

        public static void ConstructLine(string path, string branch)
        {
            // TODO
            throw new NotImplementedException();
        }

        public static void ConstructTree(string path, string branch)
        {
            if (!Directory.Exists(path))
            {
                Console.Error.WriteLine("Error: No such file or directory");
            }
            else
            {
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    Console.WriteLine($"{branch} {Path.GetFileName(file)}");
                }
                string[] folders = Directory.GetDirectories(path);
                foreach (string folder in folders)
                {
                    Console.WriteLine($"{branch} {Path.GetFileName(folder)}/");
                    ConstructTree(folder, branch += "-");
                }
            }
        }

        public static void MyTree(string path)
        {
            ConstructTree(path, "|-");
        }

    }
}