using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPatterns.Principles.Journal;

namespace DesignPatterns.Principles
{
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;
        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; //memento
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }


        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
        
        public class Persistence
        {
            public void SaveToFile(Journal journal, string filename, bool overwrite = false)
            {
                if(overwrite || !File.Exists(filename))
                {
                    try
                    {
                        File.WriteAllText(filename, journal.ToString());
                    }
                    catch(IOException e)
                    {
                        Console.WriteLine(e.Message, e.StackTrace);
                    }
                   
                }
            }
        }
    }

    class SingleResponsibility
    {
        static void Main(string[] args)
        {
            var journal = new Journal();
            journal.AddEntry("first entry");
            journal.AddEntry("second entry");
            Console.WriteLine(journal);

            var p = new Persistence();
            var fileName = @"c:\temp\journal.txt";
            p.SaveToFile(journal, fileName, true);
            try
            {
                Process.Start(fileName);
            }
            catch(Exception e)
            {
                Console.WriteLine(@"Could not find directory specified in fileName");
                Console.WriteLine(e.Message, e.StackTrace);
            }       
        }
    }
}
