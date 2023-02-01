public interface ParseFunctions
{
    //Métodos para crear cartas, acciones, efectos y condiciones con el código
    public static Card CreateCard(string[] code, Action[] AllActions)
    {   
        char[] c = code[0].ToCharArray();
        char[] d = new char[c.Length-1];
        for(int i = 0; i<d.Length; i++)
        {
            d[i]=c[i];
        }
        string Name = String.Concat(d);
        int H = 0;
        int E = 0;
        int D = 0;
        Action[] Act = new Action[]{};
        for(int i = 0; i<code.Length; i++)
        {
            if(code[i]=="health")
            {
                H=int.Parse(code[i+2]);
            }
            if(code[i]=="energy")
            {
                E=int.Parse(code[i+2]);
            }
            if(code[i]=="damage")
            {
                D=int.Parse(code[i+2]);
            }
            if(code[i]=="actions")
            {
                string[] actions = code[i+2].Split('(');
                string[] analyze = new string []{};
                foreach (string x in actions)
                {
                    if(x!="")
                    {
                        char[] x1 = x.ToCharArray();
                        char[] y1 = new char[x1.Length-1];
                        for(int j = 0; j<y1.Length; j++)
                        {
                            y1[j]=x1[j];
                        }
                        string y = String.Concat(y1);
                        analyze = EnumerableFunctions.Add(analyze, y);
                    }
                }
                foreach(Action g in AllActions)
                {
                    for(int j=0; j<analyze.Length; j++)
                    {
                        if(g.ID==analyze[j])
                        {
                            Act = EnumerableFunctions.AddAction(Act, g);
                        }
                    }
                }
            }
        }
        Card z = new Card(Name, Act, H, E, D);
        return z;
    }
    public static Action CreateAction(string[] code, Condition[] AllConditions, Effect[] AllEffects)
    {
        char[] c = code[0].ToCharArray();
        char[] d = new char[c.Length-1];
        for(int i = 0; i<d.Length; i++)
        {
            d[i]=c[i];
        }
        string Name = String.Concat(d);
        Condition[] Cond = new Condition[]{};
        Effect[] Eff = new Effect[]{};
        for(int i = 0; i<code.Length; i++)
        {
            if(code[i]=="effects")
            {
                string[] effects = code[i+2].Split('(');
                string[] analyze = new string []{};
                foreach (string x in effects)
                {
                    if(x!="")
                    {
                        char[] x1 = x.ToCharArray();
                        char[] y1 = new char[x1.Length-1];
                        for(int j = 0; j<y1.Length; j++)
                        {
                            y1[j]=x1[j];
                        }
                        string y = String.Concat(y1);
                        analyze = EnumerableFunctions.Add(analyze, y);
                    }
                }
                foreach(Effect g in AllEffects)
                {
                    for(int j=0; j<analyze.Length; j++)
                    {
                        if(g.id==analyze[j])
                        {
                            Eff = EnumerableFunctions.AddEffect(Eff, g);
                        }
                    }
                }
            }
            if(code[i]=="conditions")
            {
                string[] conditions = code[i+2].Split('(');
                string[] analyze = new string []{};
                foreach (string x in conditions)
                {
                    if(x!="")
                    {
                        char[] x1 = x.ToCharArray();
                        char[] y1 = new char[x1.Length-1];
                        for(int j = 0; j<y1.Length; j++)
                        {
                            y1[j]=x1[j];
                        }
                        string y = String.Concat(y1);
                        analyze = EnumerableFunctions.Add(analyze, y);
                    }
                }
                foreach(Condition g in AllConditions)
                {
                    for(int j=0; j<analyze.Length; j++)
                    {
                        if(g.id==analyze[j])
                        {
                            Cond = EnumerableFunctions.AddCondition(Cond, g);
                        }
                    }
                }
            }
        }
        Action z = new Action(Name, Cond, Eff);
        return z;
    }
    public static Condition CreateCondition(string[] code)
    {
        char[] c = code[0].ToCharArray();
        char[] d = new char[c.Length-1];
        for(int i = 0; i<d.Length; i++)
        {
            d[i]=c[i];
        }
        string Name = String.Concat(d);
        Condition X = new Condition (Name, code[4]);
        return X;
    }
    public static Effect CreateEffect(string[] code)
    {
        char[] c = code[0].ToCharArray();
        char[] d = new char[c.Length-1];
        for(int i = 0; i<d.Length; i++)
        {
            d[i]=c[i];
        }
        string Name = String.Concat(d);
        Effect X = new Effect (Name, code[4]);
        return X;
    }
    //Métodos para extraer del código la base de cada objeto
    public static string[] CardExtract(string[] code)
    {
        string[] Return = new string[]{};
        for(int i = 0; i<code.Length; i++)
        {
            if(code[i]=="card")
            {
                for(int j = i+1; code[j]!="}"; j++)
                {
                    Return=EnumerableFunctions.Add(Return, code[j]);
                }
                Return=EnumerableFunctions.Add(Return, "}");
            }
        }
        return Return;
    }
    public static string[] ActionExtract(string[] code)
    {
        string[] Return = new string[]{};
        for(int i = 0; i<code.Length; i++)
        {
            if(code[i]=="action")
            {
                for(int j = i+1; code[j]!="}"; j++)
                {
                    Return=EnumerableFunctions.Add(Return, code[j]);
                }
                Return=EnumerableFunctions.Add(Return, "}");
            }
        }
        return Return;
    }
    public static string[] ConditionExtract(string[] code)
    {
        string[] Return = new string[]{};
        for(int i = 0; i<code.Length; i++)
        {
            if(code[i]=="condition")
            {
                for(int j = i+1; code[j]!="}"; j++)
                {
                    Return=EnumerableFunctions.Add(Return, code[j]);
                }
                Return=EnumerableFunctions.Add(Return, "}");
            }
        }
        return Return;
    }   
    public static string[] EffectExtract(string[] code)
    {
        string[] Return = new string[]{};
        for(int i = 0; i<code.Length; i++)
        {
            if(code[i]=="effect")
            {
                for(int j = i+1; code[j]!="}"; j++)
                {
                    Return=EnumerableFunctions.Add(Return, code[j]);
                }
                Return=EnumerableFunctions.Add(Return, "}");
            }
        }
        return Return;
    }
    //Métodos para comprobar si es posible crear elementos con el código
    public static (List<string>,Card[]) CheckCard(string[] CardsCode, Card[] AllCards, List<string> Errors, Action[] AllActions)
    {
        int[] init = new int[]{};
        int[] end = new int[]{};
        for(int i = 0; i<CardsCode.Length; i++)
        {
            if(CardsCode[i][CardsCode[i].Length-1]=='.')
            {
                init=EnumerableFunctions.AddInt(init, i);
            }
            if(CardsCode[i]=="}")
            {
                end=EnumerableFunctions.AddInt(end, i);
            }
        }
        if(init.Length!=end.Length)
        {
            Errors.Add("Su código contiene una carta incompleta, asegúrese de cerrar correctamente ( } ) al terminar de editar su carta");
            return (Errors,new Card[]{});
        }
        for(int i = 0; i<CardsCode.Length; i++)
        {
            if(CardsCode[i]=="actions")
            {
                int count = 0;
                string a = CardsCode[i+2];
                foreach(char b in a)
                {
                    if(b=='(')
                    {
                        count++;
                    }
                }
                if(count>3)
                {
                    Errors.Add("Una carta debe contener máximo 3 acciones");
                }
            }
        }
        for(int i = 0; i<init.Length; i++)
        {
            string[] Code = new string[end[i]-init[i]+1];
            int k = 0;
            for(int j = init[i]; j<=end[i]; j++)
            {
                Code[k]=CardsCode[j];
                k++;
            }
            for(int j=0; j<Code.Length; j++)
            {
                if(Code[j]=="health")
                {
                    int a;
                    bool K = int.TryParse(Code[j+2], out a);
                    if(!K)
                    {
                        Errors.Add("El valor asignado al parámetro (health) en la carta "+Code[0]+" no es un número entero");
                    }
                }
                if(Code[j]=="energy")
                {
                    int a;
                    bool K = int.TryParse(Code[j+2], out a);
                    if(!K)
                    {
                        Errors.Add("El valor asignado al parámetro (energy) en la carta "+Code[0]+" no es un número entero");
                    }
                }
                if(Code[j]=="damage")
                {
                    int a;
                    bool K = int.TryParse(Code[j+2], out a);
                    if(!K)
                    {
                        Errors.Add("El valor asignado al parámetro (damage) en la carta "+Code[0]+" no es un número entero");
                    }
                }
                if(Code[j]=="actions")
                {
                    string act = Code[j+2];
                    string[] act01 = act.Split('(');
                    int count = 0;
                    foreach(string x in act01)
                    {
                        string[] act02 = x.Split(')');
                        for(int m = 0; m<act02.Length; m++)
                        {
                            if(act02[m]!="")
                            {
                                count = 0;
                                foreach(Action y in AllActions)
                                {
                                    if(y.ID==act02[m])
                                    {
                                        count++;
                                    }
                                }
                                if(count==0)
                                {
                                    Errors.Add("La acción "+act02[m]+" no ha sido declarada");
                                }
                            }
                        }
                    }
                }
            }
            if(AllCards.Contains(ParseFunctions.CreateCard(Code, AllActions)))
            {
                Errors.Add("El nombre de esta carta ("+Code[0]+") ya ha sido declarado, intente cambiar el nombre");
            }
            else
            {
                AllCards=EnumerableFunctions.AddCard(AllCards,ParseFunctions.CreateCard(Code, AllActions));
            }
        }
        return (Errors,AllCards);
    }
    public static (List<string>,Action[]) CheckAction(string[] ActionsCode, Action[] AllActions, List<string> Errors, Condition[] AllConditions, Effect[] AllEfects)
    {
        int[] init = new int[]{};
        int[] end = new int[]{};
        for(int i = 0; i<ActionsCode.Length; i++)
        {
            if(ActionsCode[i][ActionsCode[i].Length-1]=='.')
            {
                init=EnumerableFunctions.AddInt(init, i);
            }
            if(ActionsCode[i]=="}")
            {
                end=EnumerableFunctions.AddInt(end, i);
            }
        }
        if(init.Length!=end.Length)
        {
            Errors.Add("Su código contiene una acción incompleta, asegúrese de cerrar correctamente ( } ) al terminar de editar su acción");
            return (Errors,new Action[]{});
        }
        for(int i = 0; i<init.Length; i++)
        {
            string[] Code = new string[end[i]-init[i]+1];
            int k = 0;
            for(int j = init[i]; j<=end[i]; j++)
            {
                Code[k]=ActionsCode[j];
                k++;
                if(Code[k-1]=="conditions")
                {
                    string act = ActionsCode[j+2];
                    string[] act01 = act.Split('(');
                    int count = 0;
                    foreach(string x in act01)
                    {
                        string[] act02 = x.Split(')');
                        for(int m = 0; m<act02.Length; m++)
                        {
                            if(act02[m]!="")
                            {
                                count= 0;
                                foreach(Condition y in AllConditions)
                                {
                                    if(y.id==act02[m])
                                    {
                                        count++;
                                    }
                                }
                                if(count==0)
                                {
                                    Errors.Add("La condicional "+act02[m]+" no ha sido declarada");
                                }
                            }
                        }
                    }
                }
                if(Code[k-1]=="effects")
                {
                    string act = ActionsCode[j+2];
                    string[] act01 = act.Split('(');
                    int count = 0;
                    foreach(string x in act01)
                    {
                        string[] act02 = x.Split(')');
                        for(int m = 0; m<act02.Length; m++)
                        {
                            if(act02[m]!="")
                            {
                                count= 0;
                                foreach(Effect y in AllEfects)
                                {
                                    if(y.id==act02[m])
                                    {
                                        count++;
                                    }
                                }
                                if(count==0)
                                {
                                    Errors.Add("El efecto "+act02[m]+" no ha sido declarado");
                                }
                            }
                        }
                    }
                }
            }
            if(AllActions.Contains(ParseFunctions.CreateAction(Code, AllConditions, AllEfects)))
            {
                Errors.Add("El nombre de esta acción ("+Code[0]+") ya ha sido declarado, intente cambiar el nombre");
            }
            else
            {
                AllActions=EnumerableFunctions.AddAction(AllActions,ParseFunctions.CreateAction(Code, AllConditions, AllEfects));
            }
        }
        return (Errors,AllActions);
    }
    public static (List<string>,Condition[]) CheckCondition(string[] ConditionsCode, Condition[] AllConditions, List<string> Errors)
    {
        int[] init = new int[]{};
        int[] end = new int[]{};
        for(int i = 0; i<ConditionsCode.Length; i++)
        {
            if(ConditionsCode[i][ConditionsCode[i].Length-1]=='.')
            {
                init=EnumerableFunctions.AddInt(init, i);
            }
            if(ConditionsCode[i]=="}")
            {
                end=EnumerableFunctions.AddInt(end, i);
            }
        }
        if(init.Length!=end.Length)
        {
            Errors.Add("Su código contiene una condicional incompleta, asegúrese de cerrar correctamente ( } ) al terminar de editar su condicional");
            return (Errors,new Condition[]{});
        }
        for(int i = 0; i<init.Length; i++)
        {
            string[] Code = new string[end[i]-init[i]+1];
            int k = 0;
            for(int j = init[i]; j<=end[i]; j++)
            {
                Code[k]=ConditionsCode[j];
                k++;
            }
            if(AllConditions.Contains(ParseFunctions.CreateCondition(Code)))
            {
                Errors.Add("El nombre de esta condicional ("+Code[0]+") ya ha sido declarado, intente cambiar el nombre");
            }
            else
            {
                AllConditions = EnumerableFunctions.AddCondition(AllConditions, ParseFunctions.CreateCondition(Code));
            }
        }
        return (Errors,AllConditions);
    }
    public static (List<string>, Effect[]) CheckEffect(string[] EffectsCode, Effect[] AllEffects, List<string> Errors)
    {
        int[] init = new int[]{};
        int[] end = new int[]{};
        for(int i = 0; i<EffectsCode.Length; i++)
        {
            if(EffectsCode[i][EffectsCode[i].Length-1]=='.')
            {
                init=EnumerableFunctions.AddInt(init, i);
            }
            if(EffectsCode[i]=="}")
            {
                end=EnumerableFunctions.AddInt(end, i);
            }
        }
        if(init.Length!=end.Length)
        {
            Errors.Add("Su código contiene un efecto incompleto, asegúrese de cerrar correctamente ( } ) al terminar de editar su efecto");
            return (Errors,new Effect[]{});
        }
        for(int i = 0; i<init.Length; i++)
        {
            string[] Code = new string[end[i]-init[i]+1];
            int k = 0;
            for(int j = init[i]; j<=end[i]; j++)
            {
                Code[k]=EffectsCode[j];
                k++;
            }
            if(AllEffects.Contains(ParseFunctions.CreateEffect(Code)))
            {
                Errors.Add("El nombre de este efecto ("+Code[0]+") ya ha sido declarado, intente cambiar el nombre");
            }
            else
            {
                AllEffects = EnumerableFunctions.AddEffect(AllEffects, ParseFunctions.CreateEffect(Code));
            }
        }
        return (Errors,AllEffects);
    }
}
