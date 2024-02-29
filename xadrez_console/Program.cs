using System.Security.Cryptography;

namespace xadrez_console;

class Program
{
    static void Main(string[] args)
    {
        // bool x = true;
        // Console.WriteLine(!x);
        try
        {
            PartidaDeXadrez partida = new PartidaDeXadrez();
            while (!partida.Terminada)
            {
                try
                {
                    Console.Clear();
                    Tela.ImprimirPartida(partida);

                    Console.Write("\nOrigem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    partida.ValidarOrigem(origem);

                    bool[,] posicoesPossiveis = partida._tabuleiro.Peca(origem).MovimentosPossiveis();
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida._tabuleiro, posicoesPossiveis);


                    Console.Write("\nDestino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                    partida.ValidarDestino(origem, destino);

                    partida.RealizaJogada(origem, destino);
                }
                catch (TabuleiroException x)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(x.Message);
                    Thread.Sleep(2000);
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
            Console.Clear();
            Tela.ImprimirPartida(partida);

        }
        catch (TabuleiroException x)
        {
            Console.WriteLine(x.Message);
        }

        int[,] matriz = new int[3, 4];
    }
}
