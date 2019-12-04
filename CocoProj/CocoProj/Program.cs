using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace at.jku.ssw.Coco
{
    class Program
    {
        static Dictionary<int, String> kinds = new Dictionary<int, string> {
            { 1, "Identifier" },
            { 41, "operator" },
            { 100, "semicolon" },
            { 30, "openning parantheses" },
            { 31, "closing parantheses" },
            { 34, "openning curly brackets" },
            { 35, "closing curly brackets" },
            { 3, "string literal" },
            { 18, "dot" },
            { 2, "integer" },
            { 17, "assignment operator" },
        };

        static List<Token> findLongMethodStartToken(List<Token> bracketTokens)
        {
            Stack<Token> S = new Stack<Token>();
            List<Token> longTokens = new List<Token>();
            foreach (Token t in bracketTokens)
            {
                if (t.val == "{")
                {
                    S.Push(t);
                }
                else
                {
                    Token tt = S.Pop();
                    int lineDiff = t.line - tt.line;
                    if (lineDiff > 24)
                        longTokens.Add(tt);
                }
            }
            return longTokens;
        }

        static void Main(string[] args)
        {
            Scanner scanner = new Scanner(args[0]);
            int ind = 1;
            Token t = scanner.clean(scanner.NextToken());
            List<Token> tokens = new List<Token>();
            while (t.kind != 0)
            {
                tokens.Add(scanner.clean(t));
                String kind = "" + t.kind;
                if (kinds.ContainsKey(t.kind))
                {
                    kind = kinds[t.kind];
                }
                Console.WriteLine("Token No " + ind++ + " -> (" + t.line + "," + t.col + ") " + kind + " " + t.val + " ");
                t = scanner.clean(scanner.NextToken());
            }
            List<Token> badTokens = findLongMethodStartToken(tokens.Where(tok => tok.kind == 34 || tok.kind == 35).ToList());
            if (badTokens.Count == 0)
            {
                Console.WriteLine("No methods with more than 24 lines were found");
            }
            else
            {
                foreach (Token badToken in badTokens)
                {
                    Console.WriteLine("You have a long method at " + badToken.line + " " + badToken.col);
                }
            }
            Console.Read();
            //Console.WriteLine(parser.errors.count + " errors detected");
            int detect = 3 - 4;
            detect = 3 * 4;
            bool boolean = (3 == 3);
            detect = 5 / 3;
            detect = 5 % 3;

        }

    }
}
