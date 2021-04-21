/*

Gramática BNF del lenguaje de expresiones simples:

    Expr ::= Expr "+" Term
    Expr ::= Term
    Term ::= Term "*" PowTerm
    Term ::= PowTerm
    PowTerm ::= Fact "**" PowTerm
    PowTerm ::= Fact
    Fact ::= "int"
    Fact ::= "(" Expr ")"

Gramática LL(1) equivalente:

    (0) Prog ::= Expr "EOF"
    (1) Expr ::= Term ("+" Term)*
    (2) Term ::= PowTerm ("*" PowTerm)*
    (3) PowTerm ::= Fact ("**" PowTerm)?
    (4) Fact ::= "int" | "(" Expr ")"

*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public enum TokenCategory {
    INT, PLUS, TIMES, POW, OPEN_PAR, CLOSE_PAR, EOF, BAD_TOKEN
}

public class Token {
    public TokenCategory Category { get; }
    public String Lexeme { get; }

    public Token(TokenCategory category, String lexeme) {
        Category = category;
        Lexeme = lexeme;
    }

    public override String ToString() {
        return $"[{Category}, \"{Lexeme}\"]";
    }
}

public class Scanner {
    readonly String input;
    static readonly Regex regex = new Regex(
        @"(\d+)|([+])|([*][*])|([*])|([(])|([)])|(\s)|(.)");

    public Scanner(String input) {
        this.input = input;
    }

    public IEnumerable<Token> Scan() {
        var result = new LinkedList<Token>();

        foreach (Match m in regex.Matches(input)) {
            if (m.Groups[1].Success) {
                result.AddLast(new Token(TokenCategory.INT, m.Value));
            } else if (m.Groups[2].Success) {
                result.AddLast(new Token(TokenCategory.PLUS, m.Value));
            } else if (m.Groups[3].Success) {
                result.AddLast(new Token(TokenCategory.POW, m.Value));
            } else if (m.Groups[4].Success) {
                result.AddLast(new Token(TokenCategory.TIMES, m.Value));
            } else if (m.Groups[5].Success) {
                result.AddLast(new Token(TokenCategory.OPEN_PAR, m.Value));
            } else if (m.Groups[6].Success) {
                result.AddLast(new Token(TokenCategory.CLOSE_PAR, m.Value));
            } else if (m.Groups[7].Success) {
                // skip
            } else if (m.Groups[8].Success) {
                result.AddLast(new Token(TokenCategory.BAD_TOKEN, m.Value));
            }
        }
        result.AddLast(new Token(TokenCategory.EOF, null));

        return result;
    }
}

public class SyntaxError: Exception {}

public class Parser {
    IEnumerator<Token> tokenStream;

    public Parser(IEnumerator<Token> tokenStream) {
        this.tokenStream = tokenStream;
        this.tokenStream.MoveNext();
    }

    public TokenCategory Current {
        get {
            return tokenStream.Current.Category;
        }
    }

    public Token Expect(TokenCategory category) {
        if (Current == category) {
            Token current = tokenStream.Current;
            tokenStream.MoveNext();
            return current;
        } else {
            throw new SyntaxError();
        }
    }

    // (0)
    public int Prog() {
        var result = Expr();
        Expect(TokenCategory.EOF);
        return result;
    }

    // (1)
    public int Expr() {
        var result = Term();
        while (Current == TokenCategory.PLUS) {
            Expect(TokenCategory.PLUS);
            result += Term();
        }
        return result;
    }

    // (2)
    public int Term() {
        var result = PowTerm();
        while (Current == TokenCategory.TIMES) {
            Expect(TokenCategory.TIMES);
            result *= PowTerm();
        }
        return result;
    }

    // (3)
    public int PowTerm() {
        var result = Fact();
        if (Current == TokenCategory.POW) {
            Expect(TokenCategory.POW);
            result = (int) Math.Pow(result, PowTerm());
        }
        return result;
    }

    // (4)
    public int Fact() {
        switch (Current) {

        case TokenCategory.INT:
            var token = Expect(TokenCategory.INT);
            return Int32.Parse(token.Lexeme);

        case TokenCategory.OPEN_PAR:
            Expect(TokenCategory.OPEN_PAR);
            var result = Expr();
            Expect(TokenCategory.CLOSE_PAR);
            return result;

        default:
            throw new SyntaxError();
        }
    }

}

public class Node: IEnumerable<Node> {

    IList<Node> children = new List<Node>();

    public Node this[int index] {
        get {
            return children[index];
        }
    }

    public Token AnchorToken { get; set; }

    public void Add(Node node) {
        children.Add(node);
    }

    public IEnumerator<Node> GetEnumerator() {
        return children.GetEnumerator();
    }

    System.Collections.IEnumerator
    System.Collections.IEnumerable.GetEnumerator() {
        throw new NotImplementedException();
    }

    public override string ToString() {
        return $"{GetType().Name} {AnchorToken}";
    }

    public string ToStringTree() {
        var sb = new StringBuilder();
        TreeTraversal(this, "", sb);
        return sb.ToString();
    }

    static void TreeTraversal(Node node, string indent, StringBuilder sb) {
        sb.Append(indent);
        sb.Append(node);
        sb.Append('\n');
        foreach (var child in node.children) {
            TreeTraversal(child, indent + "  ", sb);
        }
    }
}

public class Driver {
    public static void Main() {
        Console.Write("> ");
        var line = Console.ReadLine();
        var parser = new Parser(new Scanner(line).Scan().GetEnumerator());
        try {
            var result = parser.Prog();
            Console.WriteLine(result);
        } catch (SyntaxError) {
            Console.WriteLine("Bad syntax!");
        }
    }
}
