//Clase de obtención de las cartas del juego
public interface GameLaunch
{
    public static (Card[], List<string>) Get (string code)
    {
        List<string> Errors = LanguageAnalyzer.SintaxisAnalyzer(code, KeyWords.Keys, KeyWords.Symbol, KeyWords.Words);
        Errors = LanguageAnalyzer.LexicalAnalyzer(code,Errors);
        (Card[] Cards, Action[] Actions, Effect[] Effects, Condition[] Conditions, Errors) = Parsing.ParsingCode(Errors, code);
        return Parsing.GetCards(Errors,Cards,Actions,Effects,Conditions);
    }
}