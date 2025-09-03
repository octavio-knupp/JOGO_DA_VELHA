﻿class Program
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
            CentralizarTexto("3 - Ranking");
            CentralizarTexto("4 - Sair");
            CentralizarTexto("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcaoMenu))
                opcaoMenu = 0;

            switch (opcaoMenu)
            {
                case 1:
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
                        InicializarTabuleiro(modoJogo);
                        Jogarjxj(modoJogo);
                    }
                    else if (modoJogo == 2)
                    {
                        CentralizarTexto("Iniciando modo: Jogador vs Máquina...");
                        Console.Clear();
                        CentralizarTexto("Escolha a dificuldade");
                        CentralizarTexto("1 - Fácil");
                        CentralizarTexto("2 - Médio");
                        CentralizarTexto("3 - Difícil");
                        CentralizarTexto("Digite sua escolha");
                        int dificuldade;
                        if (!int.TryParse(Console.ReadLine(), out dificuldade))
                            dificuldade = 1;

                        CentralizarTexto("Iniciando modo: Jogador vs Máquina...");
                        Placar(modoJogo);
                        InicializarTabuleiro(modoJogo);
                        Jogarjxm(modoJogo, dificuldade);
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
                    Console.Clear();
                    MostrarRanking();
                    Console.ReadKey();
                    break;

                case 4:
                    CentralizarTexto("Saindo do jogo...");
                    break;

                default:
                    CentralizarTexto("Opção inválida. Tente novamente.");
                    Console.ReadKey();
                    break;
            }

        } while (opcaoMenu != 4);
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

            if (tabuleiro[linha, coluna] == "   ")
            {
                tabuleiro[linha, coluna] = $" {jogadorAtual} ";
                tentativas++;

                if (VerificarVencedor(jogadorAtual))
                {
                    Console.Clear();
                    MostrarTabuleiro();
                    CentralizarTexto($"Jogador {jogadorAtual} venceu!");
                    // Removida espera com ReadKey para mostrar imediatamente as opções
                    if (PerguntarContinuar())
                    {
                        InicializarTabuleiro(modoJogo);
                        Jogarjxj(modoJogo);
                    }
                    return;
                }

                jogadorAtual = (jogadorAtual == "X") ? "O" : "X";
            }
            else
            {
                CentralizarTexto("Posição já ocupada! Tente novamente.");
                Console.ReadKey();
            }
        }

        // Se saiu do laço sem vencedor -> empate
        Console.Clear();
        MostrarTabuleiro();
        CentralizarTexto("Deu velha! Empate.");
        // Removida espera com ReadKey para mostrar imediatamente as opções
        if (PerguntarContinuar())
        {
            InicializarTabuleiro(modoJogo);
            Jogarjxj(modoJogo);
        }
    }

    static void Jogarjxm(int modoJogo, int dificuldade)
    {
        // Jogador homo Sapiens
        string jogadorAtual = "X";
        int tentativas = 0;
        Random rnd = new Random();

        Placar(modoJogo);
        InicializarTabuleiro(modoJogo);

        while (tentativas < 9)
        {
            Console.Clear();
            Placar(modoJogo);
            MostrarTabuleiro();

            if (jogadorAtual == "X")
            {
                // Jogador homo
                CentralizarTexto("Sua vez (Jogador X)");
                CentralizarTexto("Digite a linha (1-3): ");
                int linha = int.Parse(Console.ReadLine()) - 1;

                CentralizarTexto("Digite a coluna (1-3): ");
                int coluna = int.Parse(Console.ReadLine()) - 1;

                if (tabuleiro[linha, coluna] == "   ")
                {
                    tabuleiro[linha, coluna] = $" {jogadorAtual} ";
                    tentativas++;
                }
                else
                {
                    CentralizarTexto("Posição já ocupada! Tente novamente.");
                    Console.ReadKey();
                    continue;
                }
            }
            else
            {
                // Jogada da máquina
                CentralizarTexto("Vez da Máquina (O)");
                int linha, coluna;

                // FÁCIL
                if (dificuldade == 1)
                {
                    do
                    {
                        linha = rnd.Next(0, 3);
                        coluna = rnd.Next(0, 3);
                    } while (tabuleiro[linha, coluna] != "   ");
                }
                // MÉDIO
                else if (dificuldade == 2)
                {
                    // tenta ganhar
                    if (!TentarJogar("O", out linha, out coluna))
                    {
                        do
                        {
                            linha = rnd.Next(0, 3);
                            coluna = rnd.Next(0, 3);
                        } while (tabuleiro[linha, coluna] != "   ");
                    }
                }


                // DIFÍCIL
                else
                {
                    // tenta ganhar
                    if (!TentarJogar("O", out linha, out coluna))
                    {
                        // tenta bloquear
                        if (!TentarJogar("X", out linha, out coluna))
                        {
                            // Senão, pega centro ou canto
                            if (tabuleiro[1, 1] == "   ")
                            {
                                linha = 1; coluna = 1;
                            }
                            else
                            {
                                // FIX: usar new int[,] e laço for para matriz 2D
                                int[,] cantos = new int[,] { { 0, 0 }, { 0, 2 }, { 2, 0 }, { 2, 2 } }; // FIX
                                bool jogou = false;

                                for (int i = 0; i < cantos.GetLength(0); i++) // FIX
                                {
                                    int lc = cantos[i, 0];
                                    int cc = cantos[i, 1];
                                    if (tabuleiro[lc, cc] == "   ")
                                    {
                                        linha = lc;
                                        coluna = cc;
                                        jogou = true;
                                        break;
                                    }
                                }

                                if (!jogou)
                                {
                                    do
                                    {
                                        linha = rnd.Next(0, 3);
                                        coluna = rnd.Next(0, 3);
                                    } while (tabuleiro[linha, coluna] != "   ");
                                }
                            }
                        }
                    }
                }

                tabuleiro[linha, coluna] = $" {jogadorAtual} ";
                tentativas++;
            }

            // Verifica vencedor
            if (VerificarVencedor(jogadorAtual))
            {
                Console.Clear();
                MostrarTabuleiro();
                if (jogadorAtual == "X")
                    CentralizarTexto("Você venceu!");
                else
                    CentralizarTexto("A máquina venceu!");
                // Removida espera com ReadKey para mostrar imediatamente as opções
                if (PerguntarContinuar())
                {
                    InicializarTabuleiro(modoJogo);
                    Jogarjxm(modoJogo, dificuldade);
                }
                return;
            }

            jogadorAtual = (jogadorAtual == "X") ? "O" : "X";
        }

        Console.Clear();
        MostrarTabuleiro();
        CentralizarTexto("Deu velha! Empate.");
        // Removida espera com ReadKey para mostrar imediatamente as opções
        if (PerguntarContinuar())
        {
            InicializarTabuleiro(modoJogo);
            Jogarjxm(modoJogo, dificuldade);
        }
    }

    static bool TentarJogar(string jogador, out int linhaEscolhida, out int colunaEscolhida)
    {
        for (int linha = 0; linha < 3; linha++)
        {
            for (int coluna = 0; coluna < 3; coluna++)
            {
                if (tabuleiro[linha, coluna] == "   ")
                {
                    tabuleiro[linha, coluna] = $" {jogador} ";
                    bool venceu = VerificarVencedor(jogador);
                    tabuleiro[linha, coluna] = "   ";

                    if (venceu)
                    {
                        linhaEscolhida = linha;
                        colunaEscolhida = coluna;
                        return true;
                    }
                }
            }
        }
        linhaEscolhida = -1;
        colunaEscolhida = -1;
        return false;
    }

    static void Placar(int modoJogo)
    {
        int vitoriaJogador1 = 0;
        int vitoriaJogador2 = 0;
        int vitoriaMaquina = 0;

        if (modoJogo == 1)
        {
            CentralizarTexto($"Placar: Jogador 1: {vitoriaJogador1} | Jogador 2: {vitoriaJogador2}");
            Console.WriteLine();
        }
        else if (modoJogo == 2)
        {
            CentralizarTexto($"Placar: Jogador 1: {vitoriaJogador1} | Máquina: {vitoriaMaquina}");
            Console.WriteLine();
        }
    }

    static void InicializarTabuleiro(int modoJogo)
    {
        Console.Clear();
        Placar(modoJogo);

        for (int linha = 0; linha < tabuleiro.GetLength(0); linha++)
        {
            for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++)
            {
                tabuleiro[linha, coluna] = "   ";
            }
        }
    }

    static void MostrarTabuleiro()
    {
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
        for (int linha = 0; linha < 3; linha++)
        {
            if (tabuleiro[linha, 0] == $" {jogador} " &&
                tabuleiro[linha, 1] == $" {jogador} " &&
                tabuleiro[linha, 2] == $" {jogador} ")
            {
                return true;
            }
        }

        for (int coluna = 0; coluna < 3; coluna++)
        {
            if (tabuleiro[0, coluna] == $" {jogador} " &&
                tabuleiro[1, coluna] == $" {jogador} " &&
                tabuleiro[2, coluna] == $" {jogador} ")
            {
                return true;
            }
        }

        if (tabuleiro[0, 0] == $" {jogador} " &&
            tabuleiro[1, 1] == $" {jogador} " &&
            tabuleiro[2, 2] == $" {jogador} ")
        {
            return true;
        }

        if (tabuleiro[0, 2] == $" {jogador} " &&
            tabuleiro[1, 1] == $" {jogador} " &&
            tabuleiro[2, 0] == $" {jogador} ")
        {
            return true;
        }

        return false;
    }

    static void MostrarRanking()
    {
        // Função ainda vazia
    }

    static bool PerguntarContinuar()
    {
        while (true)
        {
            Console.Clear();
            CentralizarTexto("Você deseja continuar a partida ou voltar para o menu?");
            CentralizarTexto("Opções: 1 - Continuar   2 - Voltar para o menu");
            CentralizarTexto("Escolha uma opção: ");
            string resposta = Console.ReadLine();

            if (resposta == "1")
                return true;
            else if (resposta == "2")
                return false;
            else
            {
                CentralizarTexto("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                Console.ReadKey();
            }
        }
    }
}