using System;

class Program
{
    static string[,] tabuleiro = new string[3, 3]; // tabuleiro agora é global

    static void Main()
    {
        int opcaoMenu;
        int modoJogo;

        do
        {
            Console.Clear();
            Console.WriteLine("=== JOGO DA VELHA ===");
            Console.WriteLine("1 - Jogar");
            Console.WriteLine("2 - Instruções");
            Console.WriteLine("3 - Sair");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcaoMenu))
                opcaoMenu = 0;

            switch (opcaoMenu)
            {
                case 1:
                    // Escolher modo de jogo
                    Console.Clear();
                    Console.WriteLine("Escolha o modo de jogo:");
                    Console.WriteLine("1 - Jogador vs Jogador");
                    Console.WriteLine("2 - Jogador vs Máquina");
                    Console.Write("Digite sua escolha: ");

                    if (!int.TryParse(Console.ReadLine(), out modoJogo))
                        modoJogo = 0;

                    if (modoJogo == 1)
                    {
                        Console.WriteLine("Iniciando modo: Jogador vs Jogador...");
                        InicializarTabuleiro();
                        string jogadorX = "X";
                        string jogadorO = "O";
                        int tentativas = 0;

                        while (tentativas < 9)
                        {
                            // Aqui depois você adiciona a lógica do JxJ
                            tentativas++;
                        }
                    }
                    else if (modoJogo == 2)
                    {
                        Console.WriteLine("Iniciando modo: Jogador vs Máquina...");
                        InicializarTabuleiro();
                        // Aqui depois você adiciona a lógica do JxM
                    }
                    else
                    {
                        Console.WriteLine("Modo inválido!");
                    }

                    Console.ReadKey();
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("=== INSTRUÇÕES ===");
                    Console.WriteLine("- Dois jogadores alternam jogadas.");
                    Console.WriteLine("- Vence quem fizer 3 em linha (horizontal, vertical ou diagonal).");
                    Console.WriteLine("- No modo Máquina, o computador fará jogadas automáticas.");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("Saindo do jogo...");
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    Console.ReadKey();
                    break;
            }

        } while (opcaoMenu != 3);
    }

    static void InicializarTabuleiro()
    {
        // Alimentando a matriz.
        Console.Clear();
        Console.WriteLine("Ganha o jogador que fizer:");
        Console.WriteLine("3 em linha (horizontal, vertical ou diagonal).");

        for (int linha = 0; linha < tabuleiro.GetLength(0); linha++)
        {
            for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++)
            {
                tabuleiro[linha, coluna] = " 0 "; // casa vazia
            }
        }

        // mostrando o tabuleiro na tela
        for (int linha = 0; linha < tabuleiro.GetLength(0); linha++)
        {
            for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++)
            {
                Console.Write($"{tabuleiro[linha, coluna]}");
                if (coluna < 2) Console.Write("|");
            }
            Console.WriteLine();
            if (linha < 2) Console.WriteLine("---+---+---");
        }
    }
}
