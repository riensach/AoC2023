using System;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Collections.Immutable;
using static AoC2022.solution.AoCDay7;
using System.Xml.Linq;

namespace AoC2022.solution
{
    public class AoCDay7
    {

        public AoCDay7(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            TreeNode<string> root = new TreeNode<string>("/");
            TreeNode<string> currentNode = root;

            foreach (string line in lines)
            {
                string[] commands = line.Split(' ');
                if (commands[0] == "dir")
                {
                    // This is listing an available directory, so we must create it
                    //TreeNode<string> node = new TreeNode<string>(commands[1]);
                    currentNode.AddChild(commands[1]);
                }
                else if (commands[0] == "$" && commands[1] == "ls")
                {
                    // The next few items are just going to be files/directorys in this directory
                }
                else if (commands[1] == "cd" && commands[2] == "..")
                {
                    // This will be a change directory - go up one level
                    //System.Console.WriteLine("got here1");
                    currentNode = currentNode.Parent;
                }
                else if (commands[1] == "cd")
                {
                    // This will be a change directory - go down one level
                    //currentNode = currentNode.Children.;
                    foreach (TreeNode<string> child in currentNode.Children)
                    {
                        //System.Console.WriteLine("got here3");
                        //get child node here
                        if (child.Value == commands[2])
                        {
                            //System.Console.WriteLine("got here");
                            currentNode = child;
                        }
                    }
                }
                else
                {
                    // This will be a file size then a file name
                    string fileSize = commands[0];
                    string fileName = commands[1];
                    currentNode.AddChild(fileSize);
                }
            }


            printNode(root);
            int rootNodeSize = nodeSize(root);
            int tempSize;
            int sumSize = 0;

            //root.Traverse(nodeSize(child));


            List<int> directorySizes = new List<int>();

            foreach (TreeNode<string> child in root.Children)
            {
                int numericValue;
                bool isNumber = int.TryParse(child.Value, out numericValue);
                if (!isNumber)
                {
                    tempSize = nodeSize(child);
                    if (tempSize <= 100000)
                    {
                        sumSize = sumSize + tempSize;
                    }
                    directorySizes.Add(tempSize);

                    foreach (TreeNode<string> child2 in child.Children)
                    {
                        isNumber = int.TryParse(child2.Value, out numericValue);
                        if (!isNumber)
                        {
                            tempSize = nodeSize(child2);
                            if (tempSize <= 100000)
                            {
                                sumSize = sumSize + tempSize;
                            }
                            directorySizes.Add(tempSize);

                            foreach (TreeNode<string> child3 in child2.Children)
                            {
                                isNumber = int.TryParse(child3.Value, out numericValue);
                                if (!isNumber)
                                {
                                    tempSize = nodeSize(child3);
                                    if (tempSize <= 100000)
                                    {
                                        sumSize = sumSize + tempSize;
                                    }
                                    directorySizes.Add(tempSize);

                                    foreach (TreeNode<string> child4 in child3.Children)
                                    {
                                        isNumber = int.TryParse(child4.Value, out numericValue);
                                        if (!isNumber)
                                        {
                                            tempSize = nodeSize(child4);
                                            if (tempSize <= 100000)
                                            {
                                                sumSize = sumSize + tempSize;
                                            }
                                            directorySizes.Add(tempSize);

                                            foreach (TreeNode<string> child5 in child4.Children)
                                            {
                                                isNumber = int.TryParse(child5.Value, out numericValue);
                                                if (!isNumber)
                                                {
                                                    tempSize = nodeSize(child5);
                                                    if (tempSize <= 100000)
                                                    {
                                                        sumSize = sumSize + tempSize;
                                                    }
                                                    directorySizes.Add(tempSize);

                                                    foreach (TreeNode<string> child6 in child5.Children)
                                                    {
                                                        isNumber = int.TryParse(child6.Value, out numericValue);
                                                        if (!isNumber)
                                                        {
                                                            tempSize = nodeSize(child6);
                                                            if (tempSize <= 100000)
                                                            {
                                                                sumSize = sumSize + tempSize;
                                                            }
                                                            directorySizes.Add(tempSize);

                                                            foreach (TreeNode<string> child7 in child6.Children)
                                                            {
                                                                isNumber = int.TryParse(child7.Value, out numericValue);
                                                                if (!isNumber)
                                                                {
                                                                    tempSize = nodeSize(child7);
                                                                    if (tempSize <= 100000)
                                                                    {
                                                                        sumSize = sumSize + tempSize;
                                                                    }
                                                                    directorySizes.Add(tempSize);

                                                                    foreach (TreeNode<string> child8 in child7.Children)
                                                                    {
                                                                        isNumber = int.TryParse(child8.Value, out numericValue);
                                                                        if (!isNumber)
                                                                        {
                                                                            tempSize = nodeSize(child8);
                                                                            if (tempSize <= 100000)
                                                                            {
                                                                                sumSize = sumSize + tempSize;
                                                                            }
                                                                            directorySizes.Add(tempSize);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


            }

            int totalSize = nodeSize(root);
            int targetSize = 70000000;
            int remainingSize = targetSize - totalSize;
            int selectedDirSize = 0;
            int tempNum = 0;

            directorySizes.Sort();
            foreach (var number in directorySizes) 
            {
                tempNum = Int32.Parse(number.ToString());
                int temp2 = remainingSize + tempNum;
                //System.Console.WriteLine("h21 - " + tempNum + " - " + remainingSize + " - " + temp2 + "\n");
                if (remainingSize + tempNum >= 30000000)
                {
                    //System.Console.WriteLine("h2");
                    selectedDirSize = tempNum;
                    break;
                }
            }
                

            output = "Part A: " + sumSize;
            output += "\nPart B: " + selectedDirSize;

            



        }



        public void printNode(TreeNode<string> node)
        {
            System.Console.WriteLine(node.Value+" - ");
            foreach (TreeNode<string> child in node.Children)
            {
                printNode(child); //<-- recursive
            }
        }

        public int nodeSize(TreeNode<string> node)
        {
            int size = 0;
            int numericValue;
            bool isNumber = int.TryParse(node.Value, out numericValue);
            if (isNumber)
            {
                size = numericValue;
            }
            System.Console.WriteLine(node.Value + " - ");
            foreach (TreeNode<string> child in node.Children)
            {
                size = size + nodeSize(child); //<-- recursive
            }

            return size;
        }



        public class TreeNode<T>
        {
            private readonly T _value;
            private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

            public TreeNode(T value)
            {
                _value = value;
            }

            public TreeNode<T> this[int i]
            {
                get { return _children[i]; }
            }

            public TreeNode<T> Parent { get; private set; }

            public T Value { get { return _value; } }

            public ReadOnlyCollection<TreeNode<T>> Children
            {
                get { return _children.AsReadOnly(); }
            }

            public TreeNode<T> AddChild(T value)
            {
                var node = new TreeNode<T>(value) { Parent = this };
                _children.Add(node);
                return node;
            }

            public TreeNode<T> InsertChild(TreeNode<T> parent, T value) { 
                var node = new TreeNode<T>(value) { Parent = parent }; 
                parent._children.Add(node); return node; 
            }
            

            public TreeNode<T>[] AddChildren(params T[] values)
            {
                return values.Select(AddChild).ToArray();
            }

            public bool RemoveChild(TreeNode<T> node)
            {
                return _children.Remove(node);
            }

            public void Traverse(Action<T> action)
            {
                action(Value);
                foreach (var child in _children)
                    child.Traverse(action);
            }

            public IEnumerable<T> Flatten()
            {
                return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
            }
        }

        public string output;
    }
}