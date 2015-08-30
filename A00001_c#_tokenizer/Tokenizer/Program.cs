namespace SimpleConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Program
    {
        //http://blog.zhaojie.me/2009/10/code-for-fun-tokenizer-answer-1.html
        static void Main()
        {
            var hello = Parse("CPU-3.0G--Color-red-green-black--Price-2000-4000--Weight-'5-'--keywords-'levi''s'");

            Console.WriteLine("press enter to exit...");
            Console.ReadLine();
        }

        delegate StateParser StateParser(char ch);

        static List<List<string>> Parse(string text)
        {
            StateParser p1 = null; // 用于解析token的起始字符
            StateParser p2 = null; // 用于解析作为分隔符的“-”的下一个字符
            StateParser p3 = null; // 用于解析token中或结尾的单引号的下一个字符
            StateParser p4 = null; // 用于解析单引号外的token字符
            StateParser p5 = null; // 用于解析单引号内的token字符

            var currentToken = new StringBuilder();
            var currentTokenGroup = new List<string>();
            var result = new List<List<string>>();

            // 解析token的起始字符
            p1 = ch =>
            {
                if (ch == '-')
                {
                    // 如果token中需要包含单引号或“-”，
                    // 那么这个token在表示的时候一定需要用一对单引号包裹起来
                    throw new ArgumentException();
                }

                if (ch == '\'')
                {
                    // 如果起始字符是单引号，
                    // 则开始解析单引号内的token字符
                    return p5;
                }
                else
                {
                    // 如果是普通字符，则作为当前token的字符，
                    // 并开始解析单引号外的token字符
                    currentToken.Append(ch);
                    return p4;
                }

            };

            // 解析作为分隔符的“-”的下一个字符
            p2 = ch =>
            {
                if (ch == '-')
                {
                    // 如果当前字符为“-”，说明一个token group结束了（因为前一个字符也是“-”），
                    // 则将当前的token group加入结果集，并且准备新的token group
                    result.Add(currentTokenGroup);
                    currentTokenGroup = new List<string>();
                    return p1;
                }
                else if (ch == '\'')
                {
                    // 如果当前字符为单引号，则说明新的token以单引号包裹
                    // 则开始解析单引号内的token字符
                    return p5;
                }
                else
                {
                    // 如果是普通字符，则算作当前token的字符，
                    // 并继续解析单引号外的token字符
                    currentToken.Append(ch);
                    return p4;
                }
            };

            // 解析token内部或结尾的单引号的下一个字符
            p3 = ch =>
            {
                if (ch == '\'')
                {
                    // 如果当前字符为单引号，则说明连续两个单引号，
                    // 所以表明token中出现了“单个”单引号，并且当前token一定包裹在单引号内，
                    // 因此继续解析单引号内的token字符
                    currentToken.Append('\'');
                    return p5;
                }
                else if (ch == '-')
                {
                    // 如果当前字符为一个分隔符，则说明上一个token已经结束了
                    // 于是将当前token加入当前token group，准备新的token，
                    // 并解析分隔符后的下一个字符
                    currentTokenGroup.Add(currentToken.ToString());
                    currentToken = new StringBuilder();
                    return p2;
                }
                else
                {
                    // 单引号后面只可能是另一个单引号或者一个分隔符，
                    // 否则说明输入错误，则抛出异常
                    throw new ArgumentException();
                }
            };

            // 用于解析单引号外的token字符，
            // 即一个没有特殊字符（分隔符或单引号）的token
            p4 = ch =>
            {
                if (ch == '\'')
                {
                    // 如果token中出现了单引号，则抛出异常
                    throw new ArgumentException();
                }

                if (ch == '-')
                {
                    // 如果出现了分隔符，则表明当前token结束了，
                    // 于是将当前token加入当前token group，准备新的token，
                    // 并解析分隔符的下一个字符
                    currentTokenGroup.Add(currentToken.ToString());
                    currentToken = new StringBuilder();
                    return p2;
                }
                else
                {
                    // 对于其他字符，则当作token中的普通字符处理
                    // 继续解析单引号外的token字符
                    currentToken.Append(ch);
                    return p4;
                }
            };

            // 用于解析单引号内的token字符
            p5 = ch =>
            {
                if (ch == '\'')
                {
                    // 对于被单引号包裹的token内的第一个单引号，
                    // 需要解析其下一个字符，才能得到其真实含义
                    return p3;
                }
                else
                {
                    // 对于普通字符，则添加到当前token内
                    currentToken.Append(ch);
                    return p5;
                }
            };

            StateParser pn = p1;
            foreach (char c in text)
            {
                pn=pn(c);
            }
         //   text.Aggregate(p1, (sp, ch) => sp(ch));

            currentTokenGroup.Add(currentToken.ToString());
            result.Add(currentTokenGroup);

            return result;
        }
    }
}