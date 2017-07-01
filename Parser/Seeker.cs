using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parser
{
    class Seeker
    {

        private List<string> listOfWords;

        public Seeker(string toPath, string fromPath)
        {
            listOfWords = ReadFromFile(toPath);

            listOfWords = listOfWords.Distinct().ToList();

            DeleteTabs();

            SeekForSp();

            WriteToFile(fromPath);
            
        }

        private List<string> ReadFromFile(string path)
        {
            List<string> list = new List<string>();

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

            return list;
        }

        private void WriteToFile(string path)
        {

            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {

                foreach(var el in listOfWords)
                {

                    sw.WriteLine(el);
                }
            }
        }

        private void DeleteTabs()
        {
            Regex reg = new Regex("[ ]{2, }", RegexOptions.None);

            for(int i = 0; i < listOfWords.Count; i++)
            {

                listOfWords[i] = Regex.Replace(listOfWords[i], @"\s{2,}", "", RegexOptions.Multiline); ;
            }
            
        }

        private void SeekForSp()
        {

            for (int i = 0; i < listOfWords.Count - 1; i++)
            {
                if (listOfWords[i].EndsWith("sp. ") && listOfWords[i + 1] != null)
                {

                    List<string> elList = listOfWords[i].Split(' ').ToList();
                    List<string> elNextList = listOfWords[i + 1].Split(' ').ToList();

                    if (elList[0].Equals(elNextList[0]))
                    {
                        listOfWords.Remove(listOfWords[i]);
                        i--;
                    }
                    
                }   
            }
        }

    }
}
