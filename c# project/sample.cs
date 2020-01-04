static void CheckFunctionsParameters (int b,int a,int c,int d)
{
}

static void CheckFunctions (int b,int a,int c)
{
}

static void Main (string[] args)
{
}

static void Check (int b,int a,int c,int d,int f)
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
                                int start = nextToken.col;
                                stack.Push(nextToken);
                                if (nextToken.kind == 100)
                                {
                                    index++;
                                    nextToken = tokens[index];
                                    stack.Push(nextToken);  
                                }
                            } 
                            index++;
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


static void Checkssss (int b,int a,int c,int d,int f,int g)
{
}