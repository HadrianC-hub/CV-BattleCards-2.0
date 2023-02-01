//Esta es la clase designada para realizar el analisis sintáctico y léxico del código
public interface LanguageAnalyzer
{
    //Análisis sintáctico
    public static List<string> SintaxisAnalyzer (string MainCode, List<string> Keys, char[] Symbol, List<string> Words)
    {
        string Evaluate(string word, List<string> Keys, char[] Symbol, List<string> Words)    //Este método evalua el tipo de una palabra o caracter en el lenguaje
        {
            if(word==Symbol[0].ToString())
            {
                return "open";
            }
            if(word==Symbol[1].ToString())
            {
                return "closed";
            }
            if(word==Symbol[2].ToString())
            {
                return "assign";
            }
            if(word==Symbol[3].ToString())
            {
                return "separated";
            }
            if(Keys.Contains(word))
            {
                return "key";
            }
            if(Words.Contains(word))
            {
                return "word";
            }
            if(word[word.Length-1]=='.')
            {
                return "definition";
            }
            else
            {
                return "value";
            }
        }
        string[] code = CodeFunctions.CodeTransform(MainCode);
        List<string> Errors = new List<string>(){};
        if(code.Length==0)
        {
            Errors.Add("El código está vacío");
            return Errors;
        }
        if(code.Length==1)
        {
            Errors.Add("Su código solo contiene un caracter... No somos adivinos jefe, escriba un código decente.");
            return Errors;
        }
        string previous = "null";
        string actual = Evaluate(code[0],Keys,Symbol,Words);
        string next = Evaluate(code[1],Keys, Symbol, Words);
        if(actual!="key")
        {
            Errors.Add("Su código debe comenzar con una llave de edición (clave) = (card/action/condition/effect)");
            return Errors;
        }
        if(next!="definition")
        {
            Errors.Add("Su primera clave debe estar definida, para insertar una definición introduzca el símbolo (.) al final del valor de definición (nombre de la clase)");
            return Errors;
        }
        for(int i = 2; i<code.Length-1; i++)
        {
            previous = actual;
            actual = next;
            next = Evaluate(code[i], Keys, Symbol, Words);
            if(!KeyWords.Check(previous, actual, next))
            {
                if(actual=="key")
                {
                    Errors.Add("La key "+code[i]+" debe estar definida, para ello declare el nombre con el símbolo (.) al final");
                    i++;
                    previous = actual;
                    actual = next;
                    next = Evaluate(code[i], Keys, Symbol, Words);
                }
                if(actual=="definition")
                {
                    Errors.Add("La definición "+code[i]+" debe ser continuada por una asignación (use ({}) para definir claves || Use (:) para definir propiedades (stats))");
                    i++;
                    previous = actual;
                    actual = next;
                    next = Evaluate(code[i], Keys, Symbol, Words);
                }
                if(actual=="open")
                {
                    Errors.Add("La asignación "+code[i]+" es inválida");
                    i++;
                    previous = actual;
                    actual = next;
                    next = Evaluate(code[i], Keys, Symbol, Words);
                }
                if(actual=="word")
                {
                    Errors.Add("El parámetro "+code[i]+" debe poseer una asignación");
                    i++;
                    previous = actual;
                    actual = next;
                    next = Evaluate(code[i], Keys, Symbol, Words);
                }
                if(actual=="assign")
                {
                    Errors.Add("Debe asignar un valor al parámetro "+code[i-1]);
                    i++;
                    previous = actual;
                    actual = next;
                    next = Evaluate(code[i], Keys, Symbol, Words);
                }
                if(actual=="value")
                {
                    Errors.Add("Debe introducir el separador (,) tras el valor "+code[i+1]+" antes de asignar otro parámetro o finalizar las asignaciones");
                    i++;
                    previous = actual;
                    actual = next;
                    next = Evaluate(code[i], Keys, Symbol, Words);
                }
                if(actual=="closed")
                {
                    Errors.Add("Tras cerrar la asignación debe introducir otra key o finalizar el código si así lo desea");
                    i++;
                    previous = actual;
                    actual = next;
                    next = Evaluate(code[i], Keys, Symbol, Words);
                }
                if(actual=="separated")
                {
                    Errors.Add("Debe introducir otro parámetro o concluir la edición de la clave (})");
                    i++;
                    previous = actual;
                    actual = next;
                    next = Evaluate(code[i], Keys, Symbol, Words);
                }
            }
        } 
        return Errors;
    }
    //Análisis léxico
    public static List<string> LexicalAnalyzer (string code, List<string>ErrorList)
    {
        if(ErrorList.Count!=0)
        {
            ErrorList.Add("No se puede analizar el contenido debido a problemas de compilación");
            return ErrorList;
        }
        string[] Code = code.Split('}');
        Code = EnumerableFunctions.EliminateNull(Code);
        List<Context> ContextElements = new List<Context>();
        List<string> ScopeElements = new List<string>();
        List<string> CardElements = LanguageScope.CardElements;
        List<string> ActionElements = LanguageScope.ActionElements;
        List<string> ConditionElements = LanguageScope.ConditionElements;
        List<string> EffectElements = LanguageScope.EffectElements;
        foreach(string x in Code)
        {
            CardElements = LanguageScope.CardElements;
            ActionElements = LanguageScope.ActionElements;
            ConditionElements = LanguageScope.ConditionElements;
            EffectElements = LanguageScope.EffectElements;
            ScopeElements = new List<string>();
            string[] RCode = CodeFunctions.CodeTransform(x);
            if(RCode.Length>2)
            {
                Context Var = new Context(RCode[0], RCode[1]);
                if(ContextElements.Contains(Var))
                {
                    ErrorList.Add("La llave "+Var.key+" definida como "+Var.definition+" ya forma parte de este contexto");
                }
                else
                {
                    ContextElements.Add(Var);
                    string[] CCode = new string[RCode.Length-3];
                    for(int i = 0; i<CCode.Length; i++)
                    {
                        CCode[i]=RCode[i+3];
                    }
                    for(int i = 0; i<CCode.Length; i++)
                    {
                        if(CCode[i]==":")
                        {
                            if(ScopeElements.Contains(CCode[i-1]))
                            {
                                ErrorList.Add("El parámetro "+CCode[i-1]+" ya forma parte de esta definición");
                            }
                            else
                            {
                                ScopeElements.Add(CCode[i-1]);
                            }
                        }
                    }
                }
                if(RCode[0]=="card")
                {
                    foreach(string line in RCode)
                    {
                        if(CardElements.Contains(line))
                        {
                            CardElements.Remove(line);
                        }
                    }
                    if(CardElements.Count!=0)
                    {
                        ErrorList.Add("Es necesario definir todos los parámetros que debe contener una carta");
                    }
                }
                if(RCode[0]=="action")
                {
                    foreach(string line in RCode)
                    {
                        if(ActionElements.Contains(line))
                        {
                            ActionElements.Remove(line);
                        }
                    }
                    if(ActionElements.Count!=0)
                    {
                        ErrorList.Add("Es necesario definir todos los parámetros que debe contener una acción");
                    }
                }
                if(RCode[0]=="effect")
                {
                    foreach(string line in RCode)
                    {
                        if(EffectElements.Contains(line))
                        {
                            EffectElements.Remove(line);
                        }
                    }
                    if(EffectElements.Count!=0)
                    {
                        ErrorList.Add("Es necesario definir todos los parámetros que debe contener un efecto");
                    }
                }
                if(RCode[0]=="condition")
                {
                    foreach(string line in RCode)
                    {
                        if(ConditionElements.Contains(line))
                        {
                            ConditionElements.Remove(line);
                        }
                    }
                    if(ConditionElements.Count!=0)
                    {
                        ErrorList.Add("Es necesario definir todos los parámetros que debe contener una condicional");
                    }
                }
            } 
        }
        return ErrorList;
    }
}