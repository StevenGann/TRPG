using System;
using System.Collections.Generic;

namespace TRPG
{
    public class Command
    {
        public string Text = "";
        public List<Token> Tokens;

        public string Pattern
        {
            get
            {
                string result = "";
                foreach (Token token in Tokens)
                {
                    if (token.Value < 10)
                    {
                        result += "0";
                    }

                    result += Convert.ToString(token.Value);
                    result += ",";
                }
                result = result.TrimEnd(',');
                return result;
            }
        }

        public Command()
        {
            Tokens = new List<Token>();
        }
    }

    public class Token
    {
        public string Text = "";
        public List<string> Adjectives;
        public int Value = 0;

        public Token(string _text, int _value)
        {
            Text = _text;
            Value = _value;
            Adjectives = new List<string>();
        }
    }
}