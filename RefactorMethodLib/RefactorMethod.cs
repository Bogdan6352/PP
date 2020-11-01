﻿using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace RefactorMethodLib
{
    public class RefactorMethod
    {
        public string DelParam(string str, string method, string parametr)
        {
            Stack<string> code = new Stack<string>();
            int last_index = 0;
            int tmp_index = 0;
            last_index = str.IndexOf(method, 0);
            code.Push(str.Substring(0,last_index));//всё до нашего метода
            tmp_index = str.IndexOf("(", last_index)+1;
            code.Push(str.Substring(last_index,tmp_index-last_index));//название метода
            last_index = tmp_index;
            int stop_index = str.IndexOf("{", last_index);
            if(stop_index == -1)
            {
                stop_index = str.Length;
            }
            while(last_index<stop_index)
            {
                tmp_index = str.IndexOf(",", last_index); //параметры
                if(tmp_index == -1)
                {
                    code.Push(str.Substring(last_index, stop_index - last_index-2));//если запятая не найдена обрезаем строку до '{'
                    break;
                }
                code.Push(str.Substring(last_index, tmp_index - last_index+1));
                last_index = tmp_index+1;
            }
            string rezult = "";
            string tmp = "";
            while(code.Count!=0)
            {
                tmp = code.Pop();
                if (!tmp.Contains(parametr))
                    rezult = tmp + rezult;
            }
            rezult += str.Substring(stop_index - 2);
            //Console.WriteLine(code.Pop());
            return rezult;//code.Pop();
            // (Test[ (]+\w+| *)(,* *\w+ *\by)
            string pattern = @"(([^/""]|^)\b" + method + @"[ (]+\w+| *)(,* *\w+ *\b
"+parametr+@")";
            // паттерн (([^/""]|^)\bTest[ (]+)(\w+ +\w+,? *)*
            // парсим начало и параметры, оставляем что нужно
            // замена $1$3
            string replacement = "$1";
            string rez = Regex.Replace(str, pattern, replacement);
            return rez;
        }

        public string Rename(string str, string method, string new_name)
        {   
            string pattern = @"([^/""]|^)\b("+method+@")[ (]+";
            //string replacement = "Tested(";
            string rez = Regex.Replace(str, pattern, new_name+"(");

            return rez; 
        }
    }
}