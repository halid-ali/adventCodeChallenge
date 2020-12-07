using System;
using System.IO;
using System.Collections.Generic;

namespace day_07
{
    class Program
    {
        private static int childCount = 0;
        private static HashSet<Bag> parents = new HashSet<Bag>();
        private static Dictionary<string, Bag> bags = new Dictionary<string, Bag>();

        static void Main(string[] args)
        {
            BuildBagTree();

            //Part One
            FindParentBagsCount(bags["shiny gold"]);
            Console.WriteLine($"Parent Bags: {parents.Count}");

            //Part Two
            FindChildBagsCount(bags["shiny gold"]);
            Console.WriteLine($"Child Bags: {childCount}");
        }

        static void FindParentBagsCount(Bag currentBag)
        {
            foreach (var parent in currentBag.Parents)
            {
                parents.Add(parent);
                FindParentBagsCount(parent);
            }
        }

        static void FindChildBagsCount(Bag currentBag)
        {
            foreach (var child in currentBag.Childs)
            {
                childCount += child.Value;
                for (int i = 0; i < child.Value; i++)
                {
                    FindChildBagsCount(child.Key);
                }
            }
        }

        static void BuildBagTree()
        {
            foreach (var line in File.ReadLines("input-question.txt"))
            {
                var input = line.Replace(" bags", "").Replace(" bag", "").Replace(".", "");
                var inputData = input.Split(" contain ");
                var parentBag = GetBag(inputData[0]);

                foreach (var child in inputData[1].Split(", "))
                {
                    if(child != "no other")
                    {
                        var childParts = child.Split(' ');
                        var size = int.Parse(childParts[0]);
                        var name = childParts[1] + " " + childParts[2];
                        
                        var childBag = GetBag(name);
                        parentBag.AddChild(childBag, size);
                        childBag.AddParent(parentBag);
                    }
                }
            }
        }

        static void AddBag(Bag bag)
        {
            if(!bags.ContainsKey(bag.Name))
                bags.Add(bag.Name, bag);
        }

        static Bag GetBag(string name)
        {
            if(!bags.ContainsKey(name))
            {
                var bag = new Bag(name);
                AddBag(bag);
                return bag;
            }
            
            return bags[name];
        }
    }

    class Bag
    {
        public Bag(string name)
        {
            Name = name;
            Parents = new List<Bag>();
            Childs = new List<KeyValuePair<Bag, int>>();
        }

        public string Name { get; }
        public IList<Bag> Parents { get; }
        public IList<KeyValuePair<Bag, int>> Childs { get; }

        public void AddParent(Bag bag)
        {
            Parents.Add(bag);
        }

        public void AddChild(Bag bag, int size)
        {
            Childs.Add(new KeyValuePair<Bag, int>(bag, size));
        }
    }
}
