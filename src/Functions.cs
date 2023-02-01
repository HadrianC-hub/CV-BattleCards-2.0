public interface EnumerableFunctions //Métodos de modificación de arrays y listas
{
    public static char[] AddChar(char[] main, char element) //Agregar un char al array
    {
        char[] second = new char[main.Length+1];
        for(int i = 0; i<main.Length; i++)
        {
            second[i] = main[i];
        }
        second[second.Length-1] = element;
        return second;
    }
    public static int[] AddInt(int[] main, int element) //Agregar un entero al array
    {
        int[] second = new int[main.Length+1];
        for(int i = 0; i<main.Length; i++)
        {
            second[i] = main[i];
        }
        second[second.Length-1] = element;
        return second;
    }
    public static string[] EliminateNull(string[] a)    //Elimina cualquier caracter nulo de un array de string
    {
        string[] b = new string[]{};    
        foreach(string x in a)
        {
            if(x!="")
            {
                string[] c = new string[b.Length+1];
                for(int i = 0; i<b.Length; i++)
                {
                    c[i] = b[i];
                }
                c[c.Length-1]=x;
                b=c;
            }
        }
        return b;
    }
    public static string[] Add(string[] main, string element)   //Agregar un elemento al array de string
    {
        if(element=="")
        {
            return main;
        }
        string[] second = new string[main.Length+1];
        for(int i = 0; i<main.Length; i++)
        {
            second[i] = main[i];
        }
        second[second.Length-1] = element;
        return second;
    }
    public static Card[] AddCard(Card[] main, Card element) //Agregar una carta al array
    {
        Card[] second = new Card[main.Length+1];
        for(int i = 0; i<main.Length; i++)
        {
            second[i] = main[i];
        }
        second[second.Length-1] = element;
        return second;
    }
    public static Card[] RemoveFrom(Card[] a, Card b)   //Eliminar una carta de un array de cartas
    {
        if(a.Contains(b))
        {
            Card[] c = new Card[]{};
            foreach(Card x in a)
            {
                if(x.Name!=b.Name)
                {
                    c=AddCard(c,x);
                }
            }
            return c;
        }
        else
        {
            return a;
        }
    }
    public static Card[] DeleteCards (Card[] a, Card[] b) //Borrar las cartas del fajo una vez repartidas
    {
        Card[] newa = new Card[]{};
        for(int i = 0; i < a.Length; i++)
        {
            if(!b.Contains(a[i]))
            {
                newa = EnumerableFunctions.AddCard(newa, a[i]);
            }
        }
        return newa;
    }
    public static Action[] AddAction(Action[] main, Action element) //Agregar una acción al array
    {
        Action[] second = new Action[main.Length+1];
        for(int i = 0; i<main.Length; i++)
        {
            second[i] = main[i];
        }
        second[second.Length-1] = element;
        return second;
    }
    public static Condition[] AddCondition(Condition[] main, Condition element) //Agregar una condicion al array
    {
        Condition[] second = new Condition[main.Length+1];
        for(int i = 0; i<main.Length; i++)
        {
            second[i] = main[i];
        }
        second[second.Length-1] = element;
        return second;
    }
    public static Effect[] AddEffect(Effect[] main, Effect element) //Agregar un efecto al array
    {
        Effect[] second = new Effect[main.Length+1];
        for(int i = 0; i<main.Length; i++)
        {
            second[i] = main[i];
        }
        second[second.Length-1] = element;
        return second;
    }
}
public interface CodeFunctions   //Métodos para modificar el código a un formato legible
{
    public static string SetAndConcat(char[] array, int init, int end)  //Extraer una parte del texto
    {
        char[] ret = new char[end-init+1];
        int a = 0;
        for(int i = init; i<=end; i++)
        {
            ret[a]=array[i];
            a++;
        }
        return String.Concat(ret);
    }
    public static string[] StringSeparator(string x)    //Separador de caracteres del código
    {
        string[] Return = new string[]{};
        char[] word = x.ToCharArray();
        char[] count = new char[]{};
        for(int i = 0; i<word.Length; i++)
        {
            if(word[i]=='{')
            {
                Return = EnumerableFunctions.Add(Return,String.Concat(count));
                count = new char []{};
                Return = EnumerableFunctions.Add(Return,"{");
            }
            if(word[i]=='}')
            {
                Return = EnumerableFunctions.Add(Return,String.Concat(count));
                count = new char []{};
                Return = EnumerableFunctions.Add(Return,"}");
            }
            if(word[i]==':')
            {
                Return = EnumerableFunctions.Add(Return,String.Concat(count));
                count = new char []{};
                Return = EnumerableFunctions.Add(Return,":");
            }
            if(word[i]==',')
            {
                Return = EnumerableFunctions.Add(Return,String.Concat(count));
                count = new char []{};
                Return = EnumerableFunctions.Add(Return,",");
            }
            if(word[i]=='[')
            {
                Return = EnumerableFunctions.Add(Return,String.Concat(count));
                count = new char []{};
                Return = EnumerableFunctions.Add(Return,"[");
            }
            if(word[i]==']')
            {
                Return = EnumerableFunctions.Add(Return,String.Concat(count));
                count = new char []{};
                Return = EnumerableFunctions.Add(Return,"]");
            }
            if(word[i]!=':'&&word[i]!=','&&word[i]!='{'&&word[i]!='}')
            {
                count = EnumerableFunctions.AddChar(count, word[i]);
            }
            if(i+1==word.Length)
            {
                Return = EnumerableFunctions.Add(Return,String.Concat(count));
            }
        }
        return Return;
    }
    public static string[] CodeTransform(string code)   //Transformador del código a un formato legible
    {
        string[] Return = new string[]{};
        string[] Code = code.Split(new char[]{' ','\n','\t','\r'});
        foreach(string x in Code)
        {
            string[] y = CodeFunctions.StringSeparator(x);
            foreach(string z in y)
            {
                Return = EnumerableFunctions.Add(Return, z);
            }
        }
        return Return;
    }
}
public interface Hand    //Métodos de modificación de la mano del jugador
{   
    public static (Card[], Card[], Card[]) RepCards (Card[] Cards, Card[] Player1Cards, Card[] Player2Cards) //Metodo para repartir alatoriamente las cartas a inicio del juego
    {
        //repartirlas, eliminarlas del fajo 
        int m = Cards.Length;
        //repartir p1
        int position1;
        int position2;
        int position3;
        int position4;
        int position5;
        (position1, position2, position3, position4, position5) = Randoms(m);
        Player1Cards[0] = Cards[position1];
        Player1Cards[1] = Cards[position2];
        Player1Cards[2] = Cards[position3];
        Player1Cards[3] = Cards[position4];
        Player1Cards[4] = Cards[position5];
        Cards = EnumerableFunctions.DeleteCards(Cards, Player1Cards);
        m = Cards.Length;
        //repartir p2
        (position1, position2, position3, position4, position5) = Randoms(m);
        Player2Cards[0] = Cards[position1];
        Player2Cards[1] = Cards[position2];
        Player2Cards[2] = Cards[position3];
        Player2Cards[3] = Cards[position4];
        Player2Cards[4] = Cards[position5];
        Cards = EnumerableFunctions.DeleteCards(Cards, Player2Cards);
        return (Cards, Player1Cards, Player2Cards);
    }
    public static int Compare (int m, int[] x)  //Metodo para obtener un número aleatorio no repetido
    {
        Random r = new Random();
        int y = r.Next(0,m);
        if(x.Contains(y))
        {
            return Compare(m,x);
        }
        return y;
    }
    public static (int, int, int, int, int) Randoms(int m) //Generar los numeros aleatorios de las cartas de cada jugador
    {
        Random r = new Random();
        int[] x = new int[]{};
        int frst = r.Next(0,m);
        x=EnumerableFunctions.AddInt(x,frst);
        int scnd = Compare(m,x);
        x=EnumerableFunctions.AddInt(x,scnd);
        int thrd = Compare(m,x);
        x=EnumerableFunctions.AddInt(x,thrd);
        int frth = Compare(m,x);
        x=EnumerableFunctions.AddInt(x,frth);
        int fvth = Compare(m,x);
        
        return (frst,scnd,thrd,frth,fvth);
    }
}
public interface InGameEvaluators
{
    public static Card Regulate(Card a) //Establecer medidas de estadísticas correctas para las cartas
    {
        Card b = a;
        if(b.Health<0)
        {
            b.Health=0;
        }
        if(b.Health>b.BaseHealth)
        {
            b.Health=b.BaseHealth;
        }
        if(b.Energy<0)
        {
            b.Energy=0;
        }
        if(b.Energy>b.BaseEnergy)
        {
            b.Energy=b.BaseEnergy;
        }
        if(b.Damage<0)
        {
            b.Damage=0;
        }
        if(b.Damage>b.BaseDamage)
        {
            b.Damage=b.BaseDamage;
        }
        return b;
    }
    public static (Card, Card) ExecuteEffect(Card Caster, Card Target, Action Act)  //Metodo que ejecuta el efecto entre dos cartas
    {
        Dictionary<string, int> Index = new Dictionary<string, int>(){};
        Index.Add("caster.health",Caster.Health);
        Index.Add("caster.energy",Caster.Energy);
        Index.Add("caster.damage",Caster.Damage);
        Index.Add("target.health",Target.Health);
        Index.Add("target.energy",Target.Energy);
        Index.Add("target.damage",Target.Damage);
        Effect[] Action = Act.Effects;
        Card NewCaster = Caster;
        Card NewTarget = Target;
        foreach(Effect x in Action)
        {
            string[] Splitted = x.Expression.Split('=');
            if(Splitted[0]=="caster.health")
            {
                NewCaster.Health=AnalyzeRightMember(Splitted[1],NewCaster,NewTarget,Index);
            }
            else if(Splitted[0]=="caster.energy")
            {
                NewCaster.Energy=AnalyzeRightMember(Splitted[1],NewCaster,NewTarget,Index);
            }
            else if(Splitted[0]=="caster.damage")
            {
                NewCaster.Damage=AnalyzeRightMember(Splitted[1],NewCaster,NewTarget,Index);
            }
            else if(Splitted[0]=="target.health")
            {
                NewTarget.Health=AnalyzeRightMember(Splitted[1],NewCaster,NewTarget,Index);
            }
            else if(Splitted[0]=="target.energy")
            {
                NewTarget.Energy=AnalyzeRightMember(Splitted[1],NewCaster,NewTarget,Index);
            }
            else if(Splitted[0]=="target.damage")
            {
                NewTarget.Damage=AnalyzeRightMember(Splitted[1],NewCaster,NewTarget,Index);
            }
        }
        NewCaster=Regulate(NewCaster);
        NewTarget=Regulate(NewTarget);
        return(NewCaster,NewTarget);
    } 
    public static int AnalyzeRightMember(string member, Card Caster, Card Target, Dictionary<string,int>Index)  //Analizar expresión matemática
    {
        if(member[0]=='(')
        {
            int count = 1;
            int mark = 1;
            for(int i = 1; i<member.Length; i++)
            {
                if(member[i]=='(')
                {
                    count++;
                }
                if(member[i]==')')
                {
                    count--;
                }
                if(count==0)
                {
                    mark=i;
                    break;
                }
            }
            char[] Memb = member.ToCharArray();
            string NewMember=CodeFunctions.SetAndConcat(Memb,1,mark-1);
            if(mark==member.Length-1)
            {
                return AnalyzeRightMember(NewMember, Caster, Target, Index);
            }
            else if(member[mark+1]=='*')
            {
                string RestMember = CodeFunctions.SetAndConcat(Memb,mark+1,Memb.Length-1);
                return (AnalyzeRightMember(NewMember, Caster, Target, Index))*(AnalyzeRightMember(RestMember,Caster,Target,Index));
            }
            else if(member[mark+1]=='/')
            {
                string RestMember = CodeFunctions.SetAndConcat(Memb,mark+1,Memb.Length-1);
                return (AnalyzeRightMember(NewMember, Caster, Target, Index))/(AnalyzeRightMember(RestMember,Caster,Target,Index));
            }
            else if(member[mark+1]=='+')
            {
                string RestMember = CodeFunctions.SetAndConcat(Memb,mark+1,Memb.Length-1);
                return (AnalyzeRightMember(NewMember, Caster, Target, Index))+(AnalyzeRightMember(RestMember,Caster,Target,Index));
            }
            else if(member[mark+1]=='-')
            {
                string RestMember = CodeFunctions.SetAndConcat(Memb,mark+1,Memb.Length-1);
                return (AnalyzeRightMember(NewMember, Caster, Target, Index))-(AnalyzeRightMember(RestMember,Caster,Target,Index));
            }

        }
        else if(member.Contains('+')||member.Contains('-')||member.Contains('*')||member.Contains('/'))
        {
            char mark = '*';
            int posmark = 1;
            for(int i = 0; i<member.Length; i++)
            {
                if(member[i]=='+')
                {
                    mark='+';
                    posmark=i;
                    break;
                }
                if(member[i]=='-')
                {
                    mark='-';
                    posmark=i;
                    break;
                }
                if(member[i]=='*')
                {
                    mark='*';
                    posmark=i;
                    break;
                }
                if(member[i]=='/')
                {
                    mark='/';
                    posmark=i;
                    break;
                }
            }
            string MI = CodeFunctions.SetAndConcat(member.ToCharArray(),0,posmark-1);
            string MD = CodeFunctions.SetAndConcat(member.ToCharArray(),posmark+1,member.Length-1);
            
            if(mark=='+')
            {
                return (AnalyzeRightMember(MI, Caster, Target, Index))+(AnalyzeRightMember(MD,Caster,Target,Index));
            }
            if(mark=='-')
            {
                return (AnalyzeRightMember(MI, Caster, Target, Index))-(AnalyzeRightMember(MD,Caster,Target,Index));
            }
            if(mark=='*')
            {
                return (AnalyzeRightMember(MI, Caster, Target, Index))*(AnalyzeRightMember(MD,Caster,Target,Index));
            }
            if(mark=='/')
            {
                return (AnalyzeRightMember(MI, Caster, Target, Index))/(AnalyzeRightMember(MD,Caster,Target,Index));
            }
        }
        else
        {
            int a;
            if(Index.TryGetValue(member,out a))
            {
                return a;
            }
            else if(int.TryParse(member, out a))
            {
                return a;
            }
            else
            {
                return -1;
            }
        }
        return 5;
    }
    public static Action[] EvaluateExpression(Card Caster, Card Target, Card[] Board, Card[] P1Hand, Card[] P2Hand) //Evaluador de expresiones condicionales
    {
        Action[] newa = new Action[]{};
        foreach(Action old in Caster.Actions)
        {
            bool Possible = true;
            foreach(Condition oldC in old.Conditions)
            {
                if(!InGameCondition.Check(oldC,Caster,Target,Board,P1Hand,P2Hand))
                {
                    Possible=false;
                }
            }
            if(Possible)
            {
                newa=EnumerableFunctions.AddAction(newa,old);
            }
        }
        return newa;
    }
    public static bool CanAttack(int[] Atk, int[] Tgt)  //Método que evalúa si es posible efectuar un ataque en el campo
    {
        if(Atk.Length==0||Tgt.Length==0)
        {
            return false;
        }
        foreach(int atk in Atk)
        {
            foreach(int tgt in Tgt)
            {
                if(atk==1)
                {
                    if(tgt==2||tgt==4||tgt==5)
                    {
                        return true;
                    }
                }
                if(atk==2)
                {
                    if(tgt==1||tgt==4||tgt==5||tgt==6||tgt==3)
                    {
                        return true;
                    }
                }
                if(atk==3)
                {
                    if(tgt==2||tgt==6||tgt==5)
                    {
                        return true;
                    }
                }
                if(atk==4)
                {
                    if(tgt==1||tgt==2||tgt==5||tgt==8||tgt==7)
                    {
                        return true;
                    }
                }
                if(atk==5)
                {
                    if(tgt==1||tgt==2||tgt==3||tgt==4||tgt==6||tgt==7||tgt==8||tgt==9)
                    {
                        return true;
                    }
                }
                if(atk==6)
                {
                    if(tgt==3||tgt==2||tgt==5||tgt==8||tgt==9)
                    {
                        return true;
                    }
                }
                if(atk==7)
                {
                    if(tgt==8||tgt==4||tgt==5)
                    {
                        return true;
                    }
                }
                if(atk==8)
                {
                    if(tgt==7||tgt==4||tgt==5||tgt==6||tgt==9)
                    {
                        return true;
                    }
                }
                if(atk==9)
                {
                    if(tgt==8||tgt==6||tgt==5)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
public interface InGameCondition //En esta interfaz se encuentran los métodos necesarios para evaluar condiciones de ejecucion de acciones durante el juego 
{
    public static bool Check (Condition x, Card Caster, Card Target, Card[] Board, Card[] P1Hand, Card[] P2Hand)
    {
        bool ret = false;
        Dictionary<string, int> Index = new Dictionary<string, int>(){};
        Index.Add("caster.health",Caster.Health);
        Index.Add("caster.energy",Caster.Energy);
        Index.Add("caster.damage",Caster.Damage);
        Index.Add("target.health",Caster.Health);
        Index.Add("target.energy",Caster.Energy);
        Index.Add("target.damage",Caster.Damage);
        Index.Add("field.cards",Board.Length);
        string[] Expression = x.Expression.Split('|');
        foreach(string exp in Expression)
        {
            string comparer = GetComparer(exp);
            string[] Splitted = exp.Split(comparer);
            int LeftIndex=AnalyzeLeftMember(Splitted[0]);
            if(LeftIndex==0)
            {
                if(comparer=="=")
                {
                    ret = Index.GetValueOrDefault(Splitted[0])==InGameEvaluators.AnalyzeRightMember(Splitted[1],Caster,Target,Index);
                }
                if(comparer=="<")
                {
                    ret = Index.GetValueOrDefault(Splitted[0])<InGameEvaluators.AnalyzeRightMember(Splitted[1],Caster,Target,Index);
                }
                if(comparer==">")
                {
                    ret = Index.GetValueOrDefault(Splitted[0])>InGameEvaluators.AnalyzeRightMember(Splitted[1],Caster,Target,Index);
                }
                if(comparer=="<=")
                {
                    ret = Index.GetValueOrDefault(Splitted[0])<=InGameEvaluators.AnalyzeRightMember(Splitted[1],Caster,Target,Index);
                }
                if(comparer=="=>")
                {
                    ret = Index.GetValueOrDefault(Splitted[0])>=InGameEvaluators.AnalyzeRightMember(Splitted[1],Caster,Target,Index);
                }
            }
            else
            {
                if(LeftIndex<=6)
                {
                    //Friend
                    if(LeftIndex<=3)
                    {
                        ret = AnyComparer(P1Hand,comparer,InGameEvaluators.AnalyzeRightMember(Splitted[1],Caster,Target,Index),LeftIndex);
                    }
                    //Enemy
                    else
                    {
                        ret = AnyComparer(P2Hand,comparer,InGameEvaluators.AnalyzeRightMember(Splitted[1],Caster,Target,Index),LeftIndex);
                    }
                }
                else
                {
                    //Friend
                    if(LeftIndex<=9)
                    {
                        ret = AllComparer(P1Hand,comparer,InGameEvaluators.AnalyzeRightMember(Splitted[1],Caster,Target,Index),LeftIndex);
                    }
                    //Enemy
                    else
                    {
                        ret = AllComparer(P2Hand,comparer,InGameEvaluators.AnalyzeRightMember(Splitted[1],Caster,Target,Index),LeftIndex);
                    }
                }
            }
        }
        return ret;
    }
    public static int AnalyzeLeftMember (string LeftMember)
    {
        if(LeftMember=="player.anycard.health")
        {
            return 1;
        }
        if(LeftMember=="player.anycard.energy")
        {
            return 2;
        }
        if(LeftMember=="player.anycard.damage")
        {
            return 3;
        }
        if(LeftMember=="player.allcards.health")
        {
            return 7;
        }
        if(LeftMember=="player.allcards.energy")
        {
            return 8;
        }
        if(LeftMember=="player.allcards.damage")
        {
            return 9;
        }
        if(LeftMember=="enemy.anycard.health")
        {
            return 4;
        }
        if(LeftMember=="enemy.anycard.energy")
        {
            return 5;
        }
        if(LeftMember=="enemy.anycard.damage")
        {
            return 6;
        }
        if(LeftMember=="enemy.allcards.health")
        {
            return 10;
        }
        if(LeftMember=="enemy.allcards.energy")
        {
            return 11;
        }
        if(LeftMember=="enemy.allcards.damage")
        {
            return 12;
        }
        return 0;
    }
    public static bool AnyComparer (Card[] List, string comparer, int RightMember, int LeftIndex)
    {
        foreach(Card x in List)
        {
            if(LeftIndex==1||LeftIndex==4)
            {
                if(comparer=="=")
                {
                    if(x.Health==RightMember)
                    {
                        return true;
                    }
                }
                if(comparer=="<")
                {
                    if(x.Health<RightMember)
                    {
                        return true;
                    }
                }
                if(comparer==">")
                {
                    if(x.Health>RightMember)
                    {
                        return true;
                    }
                }
                if(comparer=="<=")
                {
                    if(x.Health<=RightMember)
                    {
                        return true;
                    }
                }
                if(comparer=="=>")
                {
                    if(x.Health>=RightMember)
                    {
                        return true;
                    }
                }
            }
            if(LeftIndex==2||LeftIndex==5)
            {
                if(comparer=="=")
                {
                    if(x.Energy==RightMember)
                    {
                        return true;
                    }
                }
                if(comparer=="<")
                {
                    if(x.Energy<RightMember)
                    {
                        return true;
                    }
                }
                if(comparer==">")
                {
                    if(x.Energy>RightMember)
                    {
                        return true;
                    }
                }
                if(comparer=="<=")
                {
                    if(x.Energy<=RightMember)
                    {
                        return true;
                    }
                }
                if(comparer=="=>")
                {
                    if(x.Energy>=RightMember)
                    {
                        return true;
                    }
                }
            }
            if(LeftIndex==3||LeftIndex==6)
            {
                if(comparer=="=")
                {
                    if(x.Damage==RightMember)
                    {
                        return true;
                    }
                }
                if(comparer=="<")
                {
                    if(x.Damage<RightMember)
                    {
                        return true;
                    }
                }
                if(comparer==">")
                {
                    if(x.Damage>RightMember)
                    {
                        return true;
                    }
                }
                if(comparer=="<=")
                {
                    if(x.Damage<=RightMember)
                    {
                        return true;
                    }
                }
                if(comparer=="=>")
                {
                    if(x.Damage>=RightMember)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public static bool AllComparer (Card[] List, string comparer, int RightMember, int LeftIndex)
    {
        bool ret = true;
        foreach(Card x in List)
        {
            if(LeftIndex==7||LeftIndex==10)
            {
                if(comparer=="=")
                {
                    if(!(x.Health==RightMember))
                    {
                        ret=false;
                    }
                }
                if(comparer=="<")
                {
                    if(!(x.Health<RightMember))
                    {
                        ret=false;
                    }
                }
                if(comparer==">")
                {
                    if(!(x.Health>RightMember))
                    {
                        ret=false;
                    }
                }
                if(comparer=="<=")
                {
                    if(!(x.Health<=RightMember))
                    {
                        ret=false;
                    }
                }
                if(comparer=="=>")
                {
                    if(!(x.Health>=RightMember))
                    {
                        ret=false;
                    }
                }
            }
            if(LeftIndex==11||LeftIndex==8)
            {
                if(comparer=="=")
                {
                    if(!(x.Energy==RightMember))
                    {
                        ret=false;
                    }
                }
                if(comparer=="<")
                {
                    if(!(x.Energy<RightMember))
                    {
                        ret=false;
                    }
                }
                if(comparer==">")
                {
                    if(!(x.Energy>RightMember))
                    {
                        ret=false;
                    }
                }
                if(comparer=="<=")
                {
                    if(!(x.Energy<=RightMember))
                    {
                        ret=false;
                    }
                }
                if(comparer=="=>")
                {
                    if(!(x.Energy>=RightMember))
                    {
                        ret=false;
                    }
                }
            }
            if(LeftIndex==12||LeftIndex==9)
            {
                if(comparer=="=")
                {
                    if(!(x.Damage==RightMember))
                    {
                        ret=false;
                    }
                }
                if(comparer=="<")
                {
                    if(!(x.Damage<RightMember))
                    {
                        ret=false;
                    }
                }
                if(comparer==">")
                {
                    if(!(x.Damage>RightMember))
                    {
                        ret=false;
                    }
                }
                if(comparer=="<=")
                {
                    if(!(x.Damage<=RightMember))
                    {
                        ret=false;
                    }
                }
                if(comparer=="=>")
                {
                    if(!(x.Damage>=RightMember))
                    {
                        ret=false;
                    }
                }
            }
        }
        return ret;
    }
    static string GetComparer (string exp)
    {
        string comparer = "";
        if(exp.Contains('='))
        {
            comparer="=";
        }
        if(exp.Contains('<'))
        {
            comparer="<";
        }
        if(exp.Contains('>'))
        {
            comparer=">";
        }
        if(exp.Contains("<="))
        {
            comparer="<=";
        }
        if(exp.Contains("=>"))
        {
            comparer="=>";
        }
        return comparer;
    }
}
