using System;
using System.IO;
using System.Collections.Generic;

namespace CurseCase
{
    public class GenTree
    {
        private GenTree right;
        private GenTree left;
        private string name;
        private int suspicion;

        public GenTree Right => right;
        public GenTree Left => left;
        public string Name
        {
            get => name;
            set => name = value;
        }
        public int Suspicion
        {
            get => suspicion;
            set => suspicion = value;
        }

        // TODO
        public GenTree(string name, int suspicion, GenTree left, GenTree right)
        {
            this.name = name;
            this.suspicion = suspicion;
            this.left = left;
            this.right = right;
        }

        /**
         * \brief Takes the path of a .gen file and builds the GenTree from it
         */
        public GenTree(string path)
        {
            StreamReader reader = new StreamReader(path);

            string data = reader.ReadLine();
            string[] lines = data.Split('-');

            reader.Close();

            GenTree tree = Build(lines, 0);
            this.right = tree.right;
            this.left = tree.left;
            this.name = tree.name;
            this.suspicion = tree.suspicion;
        }

        private GenTree Build(string[] lines, int i)
        {
            if (i >= lines.Length)
                return null;

            string[] names = lines[i].Split(' ');
            string name = names[0];
            int suspicion = Int32.Parse(names[1]);

            return new GenTree(name, suspicion, Build(lines, 2 * i + 1), Build(lines, 2 * i + 2));
        }

        // TODO
        public void __PrintTree()
        {
            Console.WriteLine(x, sep = "", end = "");
        }

        public void PrintTree()
        {
           //if tree is empty
           if (this == null)
           {
               __PrintTree("_");
               return;
           }
           else
           {
                __PrintTree("<" + this.name + ",");
                PrintTree(this.left);
                __PrintTree(",");
                PrintTree(this.right);
                __PrintTree(">");
           }
        }

        // TODO
        public static bool ChangeName(GenTree root, string oldname, string newname)
        {
            if (root == null)
                return false;

            if (root.name == oldname)
            {
                root.name = newname;
                return true;
            }

            if (ChangeName(root.left, oldname, newname))
                return true;

            if (ChangeName(root.right, oldname, newname))
                return true;

            return false;
        }

        // TODO
        public static bool FindPath(GenTree root, string name, List<string> path)
        {
            if (root == null)
                return false;

            if (root.name == name)
            {
                path.Add(root.name);
                return true;
            }

            if (FindPath(root.left, name, path))
            {
                path.Add(root.name);
                return true;
            }

            if (FindPath(root.right, name, path))
            {
                path.Add(root.name);
                return true;
            }

            return false;
        }

        // TODO
        public static string LowestCommonDescendant(GenTree root, string PersonA, string PersonB)
        {
            List<string> pathA = new List<string>();
            List<string> pathB = new List<string>();

            FindPath(root, PersonA, pathA);
            FindPath(root, PersonB, pathB);

            int i = 0;
            while (i < pathA.Count && i < pathB.Count && pathA[i] == pathB[i])
                i++;

            return pathA[i - 1];
        }

        // TODO
        public void ToDot()
        {
            StreamWriter writer = new StreamWriter("tree.dot");
            writer.WriteLine("digraph G {");

            ToDot(writer, this, 0);

            writer.WriteLine("}");
            writer.Close();
        }
    }
}
