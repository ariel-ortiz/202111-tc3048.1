using System;
using System.IO;
using System.Text.RegularExpressions;

public class SinComentarios {
    public static void Main() {
        var regex = new Regex(@"([/][*].*?[*][/])|(.)",
            RegexOptions.Singleline);
        var text = File.ReadAllText("hello.c");
        foreach (Match match in regex.Matches(text)) {
            if (match.Groups[1].Success) {
                // encontró un comentario
            } else {
                Console.Write(match.Value);
            }
        }
    }
}

