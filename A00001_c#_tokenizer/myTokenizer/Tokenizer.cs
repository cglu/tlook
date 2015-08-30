using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myTokenizer
{
    public class Tokenizer
    {
        delegate StateParser StateParser(char ch);
        delegate void SplitElement(char ch);
        delegate void FormatString();

        private const char T = 'T';
        private const char R = 'R';
        private const char J = 'J';
        private const string OPERATOR = "+-*/";
        private const char SPLIT_QUERY_TAG = '|';
        private const char SPLIT_CONDITION_TAG = ','; 
        private const char QUERY_TAG = '>';
        private const char ATT_ACCESS_TAG = '#';
        private const char POINT_TAG='.';
        private const char LETF_CLOSE_TAG = '(';
        private const char RIGHT_CLOSE_TAG = ')';
        /// <summary>
        /// 解析函数
        /// 例如:(T(表3.3).R(1,J1,J2).A*J1)+1/J2.Code
        /// 产生结果:
        ///         List<stirng>列表
        ///           成员:>表示执行查询操作 table|row|column,#表示需要访问对象属性，J表示取对象本身
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<string> Parser(string text)
        {
            StateParser p1 = null;
            StateParser p2 = null;
            StateParser p3 = null;
            StateParser p4 = null;
            StateParser p5 = null;
            var result = new List<string>();
            StringBuilder currentToken = new StringBuilder();
            SplitElement se = (ch) =>
            {
                if (currentToken.Length > 0)
                    result.Add(currentToken.ToString());//之前项
                currentToken = new StringBuilder();
                currentToken.Append(ch);//运算符号
                result.Add(currentToken.ToString());
                currentToken = new StringBuilder();//新项
            };
            FormatString fs = () =>
            {
                string tempAll = currentToken.ToString();
                string[] strAlls = tempAll.Split(SPLIT_QUERY_TAG);
                string tempR = strAlls[1];//R部分
                string[] strRs = tempR.Split(SPLIT_CONDITION_TAG);
                string strLast = strRs[strRs.Length - 1];
                if (strLast[0]>=65)
                {
                    //J1==>J1={$J1}
                   // strLast = string.Format("{0}='{1}'",strLast.StartsWith(""));#【功能扩展】
                    strLast = strLast + "='{$" + strLast + "}'";
                }
                else
                {
                    //1==> JN=J1
                    strLast = "JN='" + strLast + "'";
                }
                strRs[strRs.Length - 1] = strLast;
                strAlls[1] = string.Join(" AND ", strRs);
                currentToken = new StringBuilder(string.Join(SPLIT_QUERY_TAG.ToString(), strAlls));
            };

            //处理项的开头
            p1 = ch =>
            {
                if (ch == POINT_TAG)
                {
                    // new exption;
                    return p1;
                }
                else if (ch ==T)
                {
                    currentToken.Append(QUERY_TAG);//查询
                    return p2;
                }
                else if (ch == R)
                {
                    currentToken.Append(SPLIT_QUERY_TAG);
                    return p3;
                }
                else if (ch == J)
                {
                    // currentToken=new StringBuilder();
                    currentToken.Append(ch);
                    return p5;
                }
                else if (OPERATOR.IndexOf(ch) != -1)
                {
                    se(ch);
                    return p1;
                }
                else if (RIGHT_CLOSE_TAG == ch || LETF_CLOSE_TAG == ch)
                {
                    se(ch);
                    return p1;
                }
                else
                {
                    currentToken.Append(ch);//数字直接插入
                    return p1;
                }
            };
            //表解析T(xxx)
            p2 = ch =>
            {
                if (ch == LETF_CLOSE_TAG)
                {
                    return p2;
                }
                else
                    if (ch == RIGHT_CLOSE_TAG)
                    {
                        return p1;
                    }
                    else
                    {
                        // app(char);
                        currentToken.Append(ch);
                        return p2;
                    }

            };
            //R处理
            p3 = ch =>
            {
                if (ch == LETF_CLOSE_TAG)
                {
                    return p3;
                }
                else
                    if (ch == RIGHT_CLOSE_TAG)
                    {
                        return p3;
                    }
                    else if (ch == POINT_TAG)
                    {
                        fs();//最后一项格式化
                        currentToken.Append(SPLIT_QUERY_TAG);
                        return p4;//列处理,交给p1,
                    }
                    else if (ch == SPLIT_CONDITION_TAG)
                    {
                        fs();
                        currentToken.Append(ch);

                        return p3;
                    }
                    else
                    {
                        currentToken.Append(ch);
                        return p3;
                    }
            };
            //解析列
            p4 = ch =>
            {
                if (OPERATOR.IndexOf(ch) != -1)
                {
                    se(ch);
                    return p1;
                }
                else if (RIGHT_CLOSE_TAG == ch || LETF_CLOSE_TAG == ch)
                {
                    se(ch);
                    return p1;
                }
                else
                {
                    currentToken.Append(ch);
                    return p4;
                }
            };
            //解析变量
            p5 = ch =>
            {
                if (OPERATOR.IndexOf(ch) != -1)
                {
                    se(ch);
                    return p1;
                }
                else if (RIGHT_CLOSE_TAG == ch || LETF_CLOSE_TAG == ch)
                {
                    se(ch);
                    return p1;
                }
                else if (ch == POINT_TAG)
                {
                    currentToken.Insert(0, ATT_ACCESS_TAG);//#表示属性访问
                    currentToken.Append(ch);
                    return p5;
                }
                else
                    currentToken.Append(ch);
                return p5;

            };
            StateParser pn = p1;
            foreach (char ch in text)
            {
                pn = pn(ch);
            }
            result.Add(currentToken.ToString());
            return result;
        }
    }
}
