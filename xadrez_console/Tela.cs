﻿namespace xadrez_console;

public static class Tela
{
    public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
    {
        for (int v = 0; v < tabuleiro.Linhas; v++)
        {
            Console.Write(8 - v + " ");
            for (int j = 0; j < tabuleiro.Colunas; j++)
            {
                if (tabuleiro.Peca(v, j) == null)
                    Console.Write("- ");
                else
                    ImprimirPeca(tabuleiro.Peca(v, j));
            }
            Console.WriteLine();
        }
        Console.WriteLine("  a b c d e f g h");
    }
    public static void ImprimirPeca(Peca peca)
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
