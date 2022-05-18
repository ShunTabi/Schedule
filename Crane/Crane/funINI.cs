using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace Crane
{
    class funINI
    {
        public static string[] getString(string fileName, string sectionName, string keyName, int logFlag)
        {
            string[] output = new string[] { $"sectionName={sectionName}\nkeyName={keyName}\nresult=ないよ" };
            string line1;
            int numOfSection = -99;
            IEnumerable<string> lines = File.ReadLines(fileName);
            for (int i = 0; i < lines.Count(); i++)
            {
                line1 = lines.Skip(i).First();
                if (line1.IndexOf(";") >= 0 || line1 == "") { continue; }
                else
                {
                    int findIndex1;
                    string findKey;
                    if (line1 == sectionName) { numOfSection = 1; }
                    else if (numOfSection == 1)
                    {
                        if (line1.Substring(0, 1) == "[") { break; }
                        findIndex1 = line1.IndexOf("=");
                        findKey = line1.Substring(0, findIndex1);
                        if (findKey == keyName)
                        {
                            string line2 = line1.Substring(findIndex1 + 1, line1.Length - findIndex1 - 1);
                            output = line2.Split('^');
                        }
                    }
                }
            }
            return output;
        }
    }
}
