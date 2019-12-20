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

        static void CheckFunctions(List<Token> tokens)
        {
            int length = tokens.Count;
            int index = 0;
            Token nextToken;
            Stack<Token> stack = new Stack<Token>();

            while (index < length)
            {
                nextToken = tokens[index];
                if (nextToken.kind == 1)
                { 
                    index += 1;
                    nextToken = tokens[index];

                    if (nextToken.kind == 1)
                    {// void
                        index += 2;
                        nextToken = tokens[index];

                        if (nextToken.kind == 30)
                        { 
                            string functionName = tokens[index - 1].val;
                            int line = tokens[index - 1].line;
                            int col = tokens[index - 1].col;
                            while (nextToken.kind != 31)
                            {
                                index++;
                                nextToken = tokens[index];
                            } // )
                            index++;
                            nextToken = tokens[index];
                            if (nextToken.kind == 34)
                            { // {
                                int start = nextToken.line;
                                stack.Push(nextToken);
                                while (stack.Count != 0)
                                {
                                    index++;
                                    nextToken = tokens[index];
                                    if (nextToken.kind == 34)
                                    {
                                        stack.Push(nextToken);
                                    }else if (nextToken.kind == 35)
                                    {
                                        stack.Pop();
                                    }
                                }
                                int end = nextToken.line;
                                if (end - start > 24)
                                {
                                    Console.WriteLine("Function {0}: in Line {1} and Col {2} has more than 24 lines.",
                                        functionName, line, col);
                                }
                                if (!Char.IsUpper(functionName.First()))
                                    Console.WriteLine("Function {0}: Name should start with uppercase letter", functionName);

                            }
                        }
                    }
                }
                index += 1;
            }
        }
 
        static void CheckFunctionsParameters (List<Token> tokens)
        {
            int length = tokens.Count;
            int index = 0;
            Token nextToken;
            Stack<Token> stack = new Stack<Token>();

            while (index < length)
            {
                nextToken = tokens[index];
                if (nextToken.kind == 1)
                {
                    index += 1;
                    nextToken = tokens[index];

                    if (nextToken.kind == 1)
                    {// void
                        index += 2;
                        nextToken = tokens[index];

                        if (nextToken.kind == 30)
                        {
                            string functionName = tokens[index - 1].val;
                            int line = tokens[index - 1].line;
                            int col = tokens[index - 1].col;
                            if (nextToken.kind != 31)
                            {
                                index += 2;
                                nextToken = tokens[index];
                                //Console.WriteLine(nextToken.val);
                                int start = nextToken.col;
                                stack.Push(nextToken);
                                while (nextToken.kind != 31)
                                {                                  
                                    index++;
                                    nextToken = tokens[index];
                                    if (nextToken.kind == 41)
                                    {
                                        index += 2;
                                        nextToken = tokens[index];
                                        stack.Push(nextToken);
                                    }
                                }
                            }
                            nextToken = tokens[index];
                            if (nextToken.kind == 31)
                            {
                                int count = stack.Count();
                                if (count > 4)
                                {
                                    Console.WriteLine("Function {0}: in Line {1} and Col {2} has more than 4 Parameters.",
                                    functionName, line, col);
                                }
                            }
                        }
                        
                    }
                }
                index += 1;
            }
        }
        static void Main(string[] args)
        {
            string fileLocation = @"C:\Users\Asus\Desktop\c# project\sample.cs";
            // Using coco scanner
            Scanner scanner = new Scanner(fileLocation);
            int ind = 1;
            Token t = scanner.clean(scanner.NextToken());
            List<Token> tokens = new List<Token>();
            while (t.kind != 0)
            {
                tokens.Add(scanner.clean(t));

                t = scanner.clean(scanner.NextToken());
            }

            CheckFunctions(tokens);
            CheckFunctionsParameters(tokens);
            Console.ReadKey();

        }

    }
}
