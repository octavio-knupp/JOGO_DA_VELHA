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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            CentralizarTexto("=== JOGO DA VELHA ===");
            CentralizarTexto("1 - Jogar");
            CentralizarTexto("2 - Instruções");
            CentralizarTexto("3 - Sair");
            CentralizarTexto("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcaoMenu))
                opcaoMenu = 0;

            switch (opcaoMenu)
            {
                case 1:
                    // Escolher modo de jogo
                    Console.Clear();
                    CentralizarTexto("Escolha o modo de jogo:");
                    CentralizarTexto("1 - Jogador vs Jogador");
                    CentralizarTexto("2 - Jogador vs Máquina");
                    CentralizarTexto("Digite sua escolha: ");

                    if (!int.TryParse(Console.ReadLine(), out modoJogo))
                        modoJogo = 0;

                    if (modoJogo == 1)
                    {
                        CentralizarTexto("Iniciando modo: Jogador vs Jogador...");
                        Placar(modoJogo);

                        // depois vamos colocar a lógica do JxJ
                        InicializarTabuleiro(modoJogo);
                        Jogarjxj(modoJogo);
                    }
                    else if (modoJogo == 2)
                    {
                        CentralizarTexto("Iniciando modo: Jogador vs Máquina...");
                        Placar(modoJogo);

                        // Aqui depois você adiciona a lógica do JxM
                        InicializarTabuleiro(modoJogo);
                    }
                    else
                    {
                        CentralizarTexto("Modo inválido!");
                    }

                    Console.ReadKey();
                    break;

                case 2:
                    Console.Clear();
                    CentralizarTexto("=== INSTRUÇÕES ===");
                    CentralizarTexto("- Dois jogadores alternam jogadas.");
                    CentralizarTexto("- Vence quem fizer 3 em linha (horizontal, vertical ou diagonal).");
                    CentralizarTexto("- No modo Máquina, o computador fará jogadas automáticas.");
                    Console.ReadKey();
                    break;

                case 3:
                    CentralizarTexto("Saindo do jogo...");
                    break;

                default:
                    CentralizarTexto("Opção inválida. Tente novamente.");
                    Console.ReadKey();
                    break;
            }

        } while (opcaoMenu != 3);
    }

    static void Jogarjxj(int modoJogo)
    {
        string jogadorAtual = "X";
        int tentativas = 0;

        InicializarTabuleiro(modoJogo);

        while (tentativas < 9)
        {
            Console.Clear();
            Placar(modoJogo);
            MostrarTabuleiro();

            CentralizarTexto($"Vez do jogador {jogadorAtual}");
            CentralizarTexto("Digite a linha (1-3): ");
            int linha = int.Parse(Console.ReadLine()) - 1;

            CentralizarTexto("Digite a coluna (1-3): ");
            int coluna = int.Parse(Console.ReadLine()) - 1;

            // Verifica se a posição está disponível
            if (tabuleiro[linha, coluna] == "   ")
            {
                tabuleiro[linha, coluna] = $" {jogadorAtual} ";
                tentativas++;

                // Verifica vitória
                if (VerificarVencedor(jogadorAtual))
                {
                    Console.Clear();
                    MostrarTabuleiro();
                    CentralizarTexto($"Jogador {jogadorAtual} venceu!");
                    Console.ReadKey();
                    return;
                }

                // Troca o jogador
                jogadorAtual = (jogadorAtual == "X") ? "O" : "X";
            }
            else
            {
                CentralizarTexto("Posição já ocupada! Tente novamente.");
                Console.ReadKey();
            }
        }
    }

    static void Placar(int modoJogo)
    {
        int vitoriaJogador1 = 0;
        int vitoriaJogador2 = 0;
        int vitoriaMaquina = 0;

        if (modoJogo == 1)
        {
            // Placar Jogador VS Jogador
            CentralizarTexto($"Placar: Jogador 1: {vitoriaJogador1} | Jogador 2: {vitoriaJogador2}");
            Console.WriteLine();
        }
        else if (modoJogo == 2)
        {
            // Placar Jogador VS Máquina

            CentralizarTexto($"Placar: Jogador 1: {vitoriaJogador1} | Máquina: {vitoriaMaquina}");
            Console.WriteLine();
        }
    }

    static void InicializarTabuleiro(int modoJogo)
    {
        Console.Clear();
        Placar(modoJogo);

        // Alimentando a matriz
        for (int linha = 0; linha < tabuleiro.GetLength(0); linha++)
        {
            for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++)
            {
                tabuleiro[linha, coluna] = "   "; // casa vazia
            }
        }
    }

    static void MostrarTabuleiro()
    {
        // mostrando o tabuleiro na tela
        for (int linha = 0; linha < tabuleiro.GetLength(0); linha++)
        {
            string linhaTabuleiro = "";
            for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++)
            {
                linhaTabuleiro += tabuleiro[linha, coluna];
                if (coluna < 2) linhaTabuleiro += "|";
            }
            CentralizarTexto(linhaTabuleiro);

            if (linha < 2)
                CentralizarTexto("---+---+---");
        }
    }

    static void CentralizarTexto(string texto)
    {
        int larguraConsole = Console.WindowWidth;
        int posicaoInicial = Math.Max(0, (larguraConsole - texto.Length) / 2);

        Console.SetCursorPosition(posicaoInicial, Console.CursorTop);
        Console.WriteLine(texto);
    }

    static bool VerificarVencedor(string jogador)
    {
        // Ainda vazio, retorna false para não quebrar o código
        return false;
    }
}
