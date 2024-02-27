using System.Diagnostics;

namespace xadrez_console;

public static class Tela
{
    public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
    {
        for (int v = 0; v < tabuleiro.Linhas; v++)
        {
            Console.Write(8 - v + " ");
            for (int j = 0; j < tabuleiro.Colunas; j++)
            {
                ImprimirPeca(tabuleiro.Peca(v, j));
            }
            Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
    }
    public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
    {
        for (int v = 0; v < tabuleiro.Linhas; v++)
        {
            Console.Write(8 - v + " ");
            for (int j = 0; j < tabuleiro.Colunas; j++)
            {
                if (posicoesPossiveis[v, j])
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                ImprimirPeca(tabuleiro.Peca(v, j));
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
        Console.BackgroundColor = ConsoleColor.Black;
    }
    public static void ImprimirPeca(Peca peca)
    {
        if (peca == null)
        {
            Console.Write("- ");
        }
        else
        {
            if (peca.Cor == Cor.Branca)
            {
                Console.Write(peca + " ");
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca + " ");
                Console.ForegroundColor = aux;
            }
        }
    }
    public static PosicaoXadrez LerPosicaoXadrez()
    {
        string s = Console.ReadLine();
        char coluna = s[0];
        int linha = int.Parse(s[1] + "");
        return new PosicaoXadrez(coluna, linha);
    }
}
