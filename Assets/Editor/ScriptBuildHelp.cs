using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CustomTool
{
    public class ScriptBuildHelp
    {
        private StringBuilder _stringBuilder;
        private string _lineBrake = "\r\n";
        private int currentIndex = 0;
        public int IndentTimes { get; set; }

        public ScriptBuildHelp()
        {
            _stringBuilder = new StringBuilder();
        }

        private void Write(string context, bool needIndent = false)
        {
            if (needIndent)
            {
                context = GetIndent() + context;
            }

            if (currentIndex == _stringBuilder.Length)
            {
                _stringBuilder.Append(context);
            }
            else
            {
                _stringBuilder.Insert(currentIndex, context);
            }

            currentIndex += context.Length;
        }

        private void WriteLine(string context, bool needIndent = false)
        {
            Write(context + _lineBrake, needIndent);
        }

        private string GetIndent()
        {
            string indent = "";
            for (int i = 0; i < IndentTimes; i++)
            {
                indent += "    ";
            }
            return indent;
        }

        private int WriteCurlyBrackets()
        {
            var start = _lineBrake+GetIndent() + "{" + _lineBrake;
            var end = GetIndent() + "}" + _lineBrake;
            Write(start + end, true);
            return end.Length;
        }

        public void WriteUsing(string nameSpaceName)
        {
            WriteLine("using "+ nameSpaceName + ";");
        }

        public void WriteEmptyLine()
        {
            WriteLine("");
        }

        public void WriteNamespace(string name)
        {
            Write("namespace "+ name);
            int length = WriteCurlyBrackets();
            currentIndex -= length;
        }

        public void WriteClass(string name)
        {
            Write("public class "+ name+" : MonoBehaviour ",true);
            int length = WriteCurlyBrackets();
            currentIndex -= length;
        }

        public void WriteFun(string name,params string[] paraName)
        {
            StringBuilder temp = new StringBuilder();
            temp.Append("public void " + name + "()");
            if (paraName.Length > 0)
            {
                foreach (string s in paraName)
                {
                    temp.Insert(temp.Length - 1, s + ",");
                }
                temp.Remove(temp.Length - 2, 1);
            }
            Write(temp.ToString(), true);
            WriteCurlyBrackets();
        }

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }
    }
}
