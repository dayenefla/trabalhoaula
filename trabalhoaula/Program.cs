using System;
using System.Text.Json;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.WriteLine();
            Console.WriteLine("Escolha uma opção:");
            Console.WriteLine("1) Verificar alfabeto e cadeia (Σ={ a,b})");
            Console.WriteLine("2) Classificador T/I/N por JSON");
            Console.WriteLine("3) Decisor: termina com 'b'?");
            Console.WriteLine("4) Avaliador proposicional (P,Q,R)");
            Console.WriteLine("5) Reconhecedor: L_par_a e a b*");
            Console.WriteLine("0 - Sair");
            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            if (opcao == "0")
            {
                break;
            }

            if (opcao == "1") Modulo1_VerificadorAlfabeto();            
            if (opcao == "2") Modulo2_ClassificadorJSON();           
            if (opcao == "3") Modulo3_TerminaComB();            
            if (opcao == "4") Modulo4_AvaliadorProposicional();            
            if (opcao == "5") Modulo5_Reconhecedor();
            
            else
            {

                Console.ReadLine();
            }
        }
    }

    // Item 1
    // Valida alfabeto
    static bool ValidarAlfabeto(string cadeia)
    {
        if (cadeia == null)
        {
            return false;
        }

        for (int i = 0; i < cadeia.Length; i++)
        {
            char c = cadeia[i];
            if (c != 'a' && c != 'b')
            {
                return false;
            }
        }

        return true;
    }

    // Função pra contar os símbolos da cadeia
    static int ContarSimbolosInvalidos(string cadeia)
    {
        if (string.IsNullOrEmpty(cadeia))
        {
            return 0;
        }

        int cont = 0;
        for (int i = 0; i < cadeia.Length; i++)
        {
            char c = cadeia[i];
            if (c != 'a' && c != 'b')
            {
                cont = cont + 1;
            }
        }

        return cont;
    }

    // Verificação do alfabeto
    static void Modulo1_VerificadorAlfabeto()
    {
        Console.Clear();
        Console.WriteLine("1- Verificador de alfabeto Σ={a,b}");

        // Primeiro um caractere
        string simbolo = "";
        while (true)
        {
            Console.Write("Digite um caractere: ");
            simbolo = Console.ReadLine();
            if (simbolo == null)
            {
                simbolo = "";
            }
            simbolo = simbolo.Trim();

            if (simbolo.Length == 1)
            {
                break; 
            }
            Console.WriteLine("Entrada inválida. Digite exatamente um caractere.");
        }

        char s = simbolo[0];
        if (s == 'a' || s == 'b')
        {
            Console.WriteLine("sim, pertence");
        }
        else
        {
            Console.WriteLine("não pertence");
        }

        // conjunto de caracteres
        Console.Write("Digite vários caracteres: ");
        string cadeia = Console.ReadLine();
        if (cadeia == null)
        {
            cadeia = "";
        }

        if (string.IsNullOrEmpty(cadeia))
        {
            Console.WriteLine("cadeia vazia");
        }
        else
        {
            int invalidos = ContarSimbolosInvalidos(cadeia);
            if (invalidos == 0)
            {
                Console.WriteLine("sim, pertence");
            }
            else
            {
                Console.WriteLine("não pertence — " + invalidos + " símbolo(s) inválido(s)");
            }
        }

        Console.WriteLine("Pressione Enter para voltar ao menu.");
        Console.ReadLine();
    }


    // Item 2
    class Problema
    {
        public string Descricao { get; set; }
        public string RespostaCorreta { get; set; }
    }

    static void Modulo2_ClassificadorJSON()
    {
        Console.Clear();
        Console.WriteLine("2- Classificar Tratável, intratável, não computável:");

        string caminho = "C:\\Users\\dayen\\source\\repos\\trabalhoaula\\trabalhoaula\\problemas.json";

        //tratamento de erro na leitura do arquivo json
        if (!File.Exists(caminho))
        {
            Console.WriteLine("Arquivo problemas.json não encontrado!");
            Console.WriteLine("Crie o arquivo na raiz do projeto.");
            Console.WriteLine("Pressione Enter para voltar ao menu.");
            Console.ReadLine();
            return;
        }

        string json = File.ReadAllText(caminho);
        List<Problema> problemas = JsonSerializer.Deserialize<List<Problema>>(json);

        int acertos = 0;
        int erros = 0;

        foreach (var p in problemas)
        {
            Console.WriteLine("\nProblema: " + p.Descricao);
            Console.Write("Classifique (T=Tratável, I=Intratável, N=Não Computável): ");
            string resposta = Console.ReadLine().ToUpper().Trim();

            if (resposta == p.RespostaCorreta)
            {
                Console.WriteLine("Correto!");
                acertos++;
            }
            else
            {
                Console.WriteLine("Errado! Resposta correta: " + p.RespostaCorreta);
                erros++;
            }
        }

        Console.WriteLine("\nResumo final:");
        Console.WriteLine("Acertos: " + acertos);
        Console.WriteLine("Erros: " + erros);

        Console.WriteLine("Pressione Enter para voltar ao menu.");
        Console.ReadLine();
    }

    
    // Item 3: cadeia que termina com b
    static void Modulo3_TerminaComB()
    {
        Console.Clear();
        Console.WriteLine("3- Programa de decisão: termina com 'b'?");

        Console.Write("Digite uma sequencia de caracteres: ");
        string cadeia = Console.ReadLine();
        if (cadeia == null)
            cadeia = "";

        if (cadeia.Length == 0)
        {
            Console.WriteLine("cadeia vazia");
        }
        else
        {
            // Verifica se os caracteres pertencem ao alfabeto incluindo minusculas e maiusculas
            bool valido = true;
            foreach (char c in cadeia)
            {
                if (!char.IsLetter(c))
                {
                    valido = false;
                    break;
                }
            }

            if (!valido)
            {
                Console.WriteLine("Entrada inválida: cadeia contém símbolo fora do alfabeto.");
            }
            else
            {
                char ultimo = cadeia[cadeia.Length - 1];
                if (ultimo == 'b' || ultimo == 'B')
                    Console.WriteLine("SIM");
                else
                    Console.WriteLine("NAO");
            }
        }

        Console.WriteLine("Pressione Enter para voltar ao menu.");
        Console.ReadLine();
    }

    // Função auxiliar para o item 4 , le 0/1 como booleano
    static bool LerBooleano(string nome)
    {
        while (true)
        {
            Console.Write(nome + " (0 = falso, 1 = verdadeiro): ");
            string entrada = Console.ReadLine() ?? "";
            entrada = entrada.Trim();

            if (entrada == "0") return false;
            if (entrada == "1") return true;

            Console.WriteLine("Entrada inválida. Digite 0 ou 1.");
        }
    }

    // Item 4 
    static void Modulo4_AvaliadorProposicional()
    {
        Console.Clear();
        Console.WriteLine("4- Avaliador proposicional básico");
        Console.WriteLine("Informe valores para P, Q, R (0 = falso, 1 = verdadeiro).");

        bool p = LerBooleano("P");
        bool q = LerBooleano("Q");
        bool r = LerBooleano("R");

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Escolha a fórmula:");
            Console.WriteLine("1 - (P && Q) || R");
            Console.WriteLine("2 - Implicação: P => (Q || R)");
            Console.Write("Opção (1/2): ");

            string op = Console.ReadLine() ?? "";
            op = op.Trim();

            if (op == "1" || op == "2")
            {
                bool resultado = false;

                if (op == "1")
                    resultado = (p && q) || r;
                else
                    resultado = (!p) || (q || r); // P => (Q || R) equivale a !P || (Q || R)

                Console.WriteLine("Avaliação com os valores informados: " + (resultado ? "VERDADEIRO" : "FALSO"));

                Console.Write("Deseja imprimir a tabela-verdade completa desta fórmula? (S/N): ");
                string tt = Console.ReadLine() ?? "";
                tt = tt.Trim().ToUpper();

                if (tt == "S")
                {
                    Console.WriteLine();
                    Console.WriteLine("P Q R | Resultado");
                    for (int pi = 0; pi <= 1; pi++)
                    {
                        for (int qi = 0; qi <= 1; qi++)
                        {
                            for (int ri = 0; ri <= 1; ri++)
                            {
                                bool bp = (pi == 1);
                                bool bq = (qi == 1);
                                bool br = (ri == 1);
                                bool res;

                                if (op == "1")
                                    res = (bp && bq) || br;
                                else
                                    res = (!bp) || (bq || br);

                                string linha = (bp ? "1" : "0") + " " + (bq ? "1" : "0") + " " + (br ? "1" : "0") + " | " + (res ? "1" : "0");
                                Console.WriteLine(linha);
                            }
                        }
                    }
                }

                Console.WriteLine("Pressione Enter para voltar ao menu.");
                Console.ReadLine();
                return;
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }
        }
    }

    // Item 5 
    static void Modulo5_Reconhecedor()
    {
        Console.Clear();
        Console.WriteLine("5— Reconhecedor Σ={a,b}");
        Console.WriteLine("1 - L_par_a (cadeias com número par de 'a')");
        Console.WriteLine("2 - L = { w | w = a b* }");
        Console.Write("Opção (1/2): ");

        string op = Console.ReadLine() ?? "";
        op = op.Trim();

        if (op != "1" && op != "2")
        {
            Console.WriteLine("Opção inválida. Voltando ao menu.");
            Console.ReadLine();
            return;
        }

        Console.Write("Entre com a cadeia: ");
        string cadeia = Console.ReadLine() ?? "";

        if (!ValidarAlfabeto(cadeia))
        {
            Console.WriteLine("REJEITA (alfabeto inválido).");
            Console.WriteLine("Pressione Enter para voltar ao menu.");
            Console.ReadLine();
            return;
        }

        if (op == "1")
        {
            // L_par_a: número par de 'a' (inclui cadeia vazia)
            int cont = 0;
            foreach (char c in cadeia)
                if (c == 'a') cont++;

            Console.WriteLine((cont % 2 == 0) ? "ACEITA" : "REJEITA");
        }
        else
        {
            // L = { w | w = a b* } -> deve começar com 'a' e o resto só 'b', e não pode ser vazio
            if (string.IsNullOrEmpty(cadeia) || cadeia[0] != 'a')
            {
                Console.WriteLine("REJEITA");
            }
            else
            {
                bool ok = true;
                for (int i = 1; i < cadeia.Length; i++)
                    if (cadeia[i] != 'b') { ok = false; break; }

                Console.WriteLine(ok ? "ACEITA" : "REJEITA");
            }
        }

        Console.WriteLine("Pressione Enter para voltar ao menu.");
        Console.ReadLine();
    }
}