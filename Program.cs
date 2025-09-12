﻿using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    // Dicionário para armazenar o ranking dos jogadores.
    static Dictionary<string, int> ranking = new Dictionary<string, int>();

    // Tabuleiro do jogo representado por uma matriz 3x3.
    static string[,] tabuleiro = new string[3, 3];


    // Ponto de entrada do programa.
    static void Main()
    {
        // Carrega o ranking ao iniciar o programa (comentado, mas pronto para uso)
        // CarregarRanking();
        // Menu principal do jogo.
        int opcaoMenu;
        int modoJogo;

        // Loop do menu principal, continua até o usuário escolher sair.
        do
        {
            // Define a cor padrão para a interface
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            CentralizarTexto("=== JOGO DA VELHA ===\n");
            CentralizarTexto("1 - Jogar");
            CentralizarTexto("2 - Instruções");
            CentralizarTexto("3 - Ranking");
            CentralizarTexto("4 - Sair");
            CentralizarTexto("Escolha uma opção:");

            // Lê a opção do menu e trata entradas inválidas
            if (!int.TryParse(Console.ReadLine(), out opcaoMenu))
                opcaoMenu = 0;

            // Processa a opção escolhida
            switch (opcaoMenu)
            {

                // Opção para iniciar o jogo
                case 1:
                    Console.Clear();
                    int modoJogoValido = 0;

                    // Loop até o usuário digitar um modo de jogo válido (1 ou 2)
                    do
                    {
                        CentralizarTexto("Escolha o modo de jogo:\n");
                        CentralizarTexto("1 - Jogador vs Jogador");
                        CentralizarTexto("2 - Jogador vs Máquina");
                        CentralizarTexto("Digite sua escolha: ");

                        // Lê a entrada do usuário
                        string entradaModo = Console.ReadLine();

                        // Tenta converter a entrada para um número inteiro e valida se está entre 1 e 2
                        if (!int.TryParse(entradaModo, out modoJogoValido) || modoJogoValido < 1 || modoJogoValido > 2)
                        {
                            CentralizarTexto("Modo inválido, tente novamente.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }

                    // Continua o loop enquanto a entrada não for válida
                    while (modoJogoValido != 1 && modoJogoValido != 2);

                    // Atribui o modo de jogo válido à variável global
                    modoJogo = modoJogoValido;

                    // Inicia o jogo conforme o modo escolhido
                    if (modoJogo == 1)
                    {
                        Placar(modoJogo);
                        InicializarTabuleiro(modoJogo);
                        Jojarjxj(modoJogo);
                    }

                    // Se o modo escolhido for Jogador vs Máquina, pede para escolher a dificuldade.
                    else if (modoJogo == 2)
                    {
                        // Variável para armazenar a dificuldade escolhida.
                        int dificuldadeValida = 0;

                        // Loop até o usuário digitar uma dificuldade válida (1, 2 ou 3)
                        do
                        {
                            Console.Clear();
                            CentralizarTexto("Escolha a dificuldade:\n");
                            CentralizarTexto("1 - Fácil");
                            CentralizarTexto("2 - Médio");
                            CentralizarTexto("3 - Difícil");
                            CentralizarTexto("Digite sua escolha:");

                            // Lê a entrada do usuário
                            string entradaDif = Console.ReadLine();

                            // Tenta converter a entrada para um número inteiro e valida se está entre 1 e 3
                            if (!int.TryParse(entradaDif, out dificuldadeValida) || dificuldadeValida < 1 || dificuldadeValida > 3)
                            {
                                CentralizarTexto("Dificuldade inválida, tente novamente.");
                                Console.ReadKey();
                            }
                        }

                        // Continua o loop enquanto a entrada não for válida
                        while (dificuldadeValida < 1 || dificuldadeValida > 3);

                        Placar(modoJogo);
                        InicializarTabuleiro(modoJogo);
                        Jojarjxm(modoJogo, dificuldadeValida); // inicia o jogo no modo jogador vs maquina.
                    }

                    // Pausa para o usuário ver o resultado antes de voltar ao menu
                    Console.ReadKey();
                    break;

                case 2:
                    Console.Clear();
                    CentralizarTexto("=== INSTRUÇÕES ===\n");
                    CentralizarTexto("- Dois jogadores alternam jogadas.");
                    CentralizarTexto("- Vence quem fazer 3 em linha (horizontal, vertical ou diagonal).");
                    CentralizarTexto("- No modo Máquina, o computador fará jogadas automáticas.");
                    Console.ReadKey();
                    break;

                case 3:
                    // Opção para exibir o ranking dos jogadores.
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

        }

        // Continua o loop do menu principal até o usuário escolher sair (opção 4)
        while (opcaoMenu != 4);
    }

    // Metodo para jogar entre dois jogadores.
    static void Jojarjxj(int modoJogo)
    {
        // "X" sempre começa jogando.
        string jogadorAtual = "X";

        // Contador de tentativas (máximo 9 jogadas).
        int tentativas = 0;
        InicializarTabuleiro(modoJogo);


        // Loop principal do jogo, continua até que haja um vencedor ou empate.
        while (tentativas < 9)
        {
            Console.Clear();

            // Exibe o placar para o modo Jogador vs Jogador.
            Placar(modoJogo);
            MostrarTabuleiro();

            CentralizarTexto($"Vez do jogador {jogadorAtual}");
            CentralizarTexto("Digite a linha (1-3): ");

            // Lê a linha e coluna escolhidas pelo jogador (ajustando para índice 0)
            int linha = int.Parse(Console.ReadLine()) - 1;

            CentralizarTexto("Digite a coluna (1-3): ");
            int coluna = int.Parse(Console.ReadLine()) - 1;


            // Verifica se a posição está vazia antes de fazer a jogada.
            if (tabuleiro[linha, coluna] == "   ")
            {

                // Atualiza o tabuleiro com a jogada do jogador atual.
                tabuleiro[linha, coluna] = $" {jogadorAtual} ";

                // Incrementa o contador de tentativas.
                tentativas++;


                // Verifica se o jogador atual venceu após a jogada.
                if (VerificarVencedor(jogadorAtual))
                {
                    Console.Clear();
                    MostrarTabuleiro();
                    CentralizarTexto($"Jogador {jogadorAtual} venceu!");

                    // Atualiza a pontuação no ranking
                    AtualizarPontuacao($"Jogador {jogadorAtual}");

                    // Pergunta se deseja continuar ou voltar ao menu, sem limpar a tela
                    if (PerguntarContinuar())
                    {
                        InicializarTabuleiro(modoJogo);
                        Jojarjxj(modoJogo);
                    }
                    return;
                }

                // Alterna para o próximo jogador.
                jogadorAtual = (jogadorAtual == "X") ? "O" : "X";
            }

            // Se a posição já estiver ocupada, avisa o jogador e pede para tentar novamente.
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

        // Pergunta se deseja continuar ou voltar ao menu, sem limpar a tela
        if (PerguntarContinuar())
        {
            InicializarTabuleiro(modoJogo);
            Jojarjxj(modoJogo);
        }
    }

    // Metodo para jogar contra a maquina.
    // A maquina pode jogar em 3 niveis de dificuldade.
    static void Jojarjxm(int modoJogo, int dificuldade)
    {

        // "X" sempre começa jogando.
        string jogadorAtual = "X";

        // Contador de tentativas (máximo 9 jogadas).
        int tentativas = 0;

        // Gerador de números aleatórios para a máquina.
        Random rnd = new Random();

        Placar(modoJogo);
        InicializarTabuleiro(modoJogo);


        // Loop principal do jogo, continua até que haja um vencedor ou empate.
        while (tentativas < 9)
        {
            Console.Clear();
            Placar(modoJogo);
            MostrarTabuleiro();

            // Se for a vez do jogador humano (X).
            // Solicita a jogada do jogador atual.
            if (jogadorAtual == "X")
            {
                CentralizarTexto("Sua vez (Jogador X)");
                CentralizarTexto("Digite a linha (1-3): ");

                // Lê a linha e coluna escolhidas pelo jogador (ajustando para índice 0)
                int linha = int.Parse(Console.ReadLine()) - 1;

                CentralizarTexto("Digite a coluna (1-3): ");
                int coluna = int.Parse(Console.ReadLine()) - 1;


                // Verifica se a posição está vazia antes de fazer a jogada.
                if (tabuleiro[linha, coluna] == "   ")
                {
                    // Atualiza o tabuleiro com a jogada do jogador atual.
                    tabuleiro[linha, coluna] = $" {jogadorAtual} ";
                    tentativas++;
                }

                // Se a posição já estiver ocupada, avisa o jogador e pede para tentar novamente.
                else
                {
                    CentralizarTexto("Posição já ocupada! Tente novamente.");
                    Console.ReadKey();
                    continue;
                }
            }

            // Se for a vez da máquina (O).
            // A máquina joga conforme a dificuldade escolhida.
            else
            {
                CentralizarTexto("Vez da Máquina (O)");
                int linha, coluna;

                // Dificuldade 1: Jogadas aleatórias.
                if (dificuldade == 1)
                {
                    // Escolhe uma posição aleatória vazia.
                    do
                    {
                        linha = rnd.Next(0, 3);
                        coluna = rnd.Next(0, 3);
                    }
                    // Repete até encontrar uma posição vazia
                    while (tabuleiro[linha, coluna] != "   ");
                }

                // Dificuldade 2: Tenta vencer ou bloquear o jogador.
                else if (dificuldade == 2)
                {
                    // Primeiro tenta vencer
                    if (!TentarJogar("O", out linha, out coluna))
                    {
                        // Se não puder vencer, tenta bloquear o jogador
                        do
                        {
                            linha = rnd.Next(0, 3);
                            coluna = rnd.Next(0, 3);
                        }

                        // Repete até encontrar uma posição vazia
                        while (tabuleiro[linha, coluna] != "   ");
                    }
                }

                // Dificuldade 3: Estratégia avançada.
                else
                {
                    // Primeiro tenta vencer
                    if (!TentarJogar("O", out linha, out coluna))
                    {
                        // Se não puder vencer, tenta bloquear o jogador
                        if (!TentarJogar("X", out linha, out coluna))
                        {
                            // Estratégia: se o centro estiver livre, joga lá
                            if (tabuleiro[1, 1] == "   ")
                            {
                                linha = 1; coluna = 1;
                            }
                            // Se o centro estiver ocupado, tenta jogar em um canto
                            else
                            {
                                // Define os cantos do tabuleiro
                                int[,] cantos = new int[,] { { 0, 0 }, { 0, 2 }, { 2, 0 }, { 2, 2 } };
                                // Verifica se algum canto está livre
                                bool jogou = false;

                                // Tenta jogar em um canto livre
                                for (int i = 0; i < cantos.GetLength(0); i++)
                                {
                                    int lc = cantos[i, 0];
                                    int cc = cantos[i, 1];

                                    // Se o canto estiver livre, joga lá
                                    if (tabuleiro[lc, cc] == "   ")
                                    {
                                        linha = lc;
                                        coluna = cc;
                                        jogou = true;
                                        break;
                                    }
                                }

                                // Se nenhum canto estiver livre, joga em qualquer posição livre
                                if (!jogou)
                                {
                                    // Escolhe uma posição aleatória vazia.
                                    do
                                    {
                                        linha = rnd.Next(0, 3);
                                        coluna = rnd.Next(0, 3);
                                    }

                                    // Repete até encontrar uma posição vazia
                                    while (tabuleiro[linha, coluna] != "   ");
                                }
                            }
                        }
                    }
                }

                // Atualiza o tabuleiro com a jogada da máquina.
                tabuleiro[linha, coluna] = $" {jogadorAtual} ";
                // Incrementa o contador de tentativas.
                tentativas++;
            }

            // Verifica se o jogador atual venceu após a jogada.
            if (VerificarVencedor(jogadorAtual))
            {
                Console.Clear();
                MostrarTabuleiro();

                // Mostra a mensagem de vitória conforme quem ganhou.
                if (jogadorAtual == "X")
                {
                    CentralizarTexto("Você venceu!");
                    // Adicionado: Atualiza a pontuação no ranking
                    AtualizarPontuacao("Jogador X");
                }

                // Se a máquina vencer
                // Mensagem para deixar claro que a máquina venceu
                else
                {
                    CentralizarTexto("A máquina venceu!");
                    // Atualiza a pontuação no ranking
                    AtualizarPontuacao("Máquina");
                }
                // Pergunta se deseja continuar ou voltar ao menu, sem limpar a tela
                if (PerguntarContinuar())
                {
                    InicializarTabuleiro(modoJogo);
                    Jojarjxm(modoJogo, dificuldade);
                }
                return;
            }

            // Alterna para o próximo jogador.
            jogadorAtual = (jogadorAtual == "X") ? "O" : "X";
        }

        Console.Clear();
        MostrarTabuleiro();
        CentralizarTexto("Deu velha! Empate.");

        // Pergunta se deseja continuar ou voltar ao menu, sem limpar a tela
        if (PerguntarContinuar())
        {
            InicializarTabuleiro(modoJogo);
            Jojarjxm(modoJogo, dificuldade);
        }
    }

    // Metodo para a maquina tentar jogar em uma posiçao que a faça vencer ou bloquear o jogador.
    static bool TentarJogar(string jogador, out int linhaEscolhida, out int colunaEscolhida)
    {
        // Percorre todas as posições do tabuleiro
        for (int linha = 0; linha < 3; linha++)
        {
            // Percorre todas as colunas da linha atual
            for (int coluna = 0; coluna < 3; coluna++)
            {
                // Verifica se a posição está vazia
                if (tabuleiro[linha, coluna] == "   ")
                {
                    // Simula a jogada do jogador na posição vazia
                    tabuleiro[linha, coluna] = $" {jogador} ";
                    // Verifica se essa jogada faria o jogador vencer
                    bool venceu = VerificarVencedor(jogador);
                    // Desfaz a jogada simulada
                    tabuleiro[linha, coluna] = "   ";

                    // Se a jogada leva à vitória, retorna a posição escolhida
                    if (venceu)
                    {
                        linhaEscolhida = linha;
                        colunaEscolhida = coluna;
                        return true;
                    }
                }
            }
        }
        // Se nenhuma jogada leva à vitória, retorna false
        linhaEscolhida = -1;
        colunaEscolhida = -1;
        return false;
    }

    // Metodo para exibir o placar.
    static void Placar(int modoJogo)
    {
        // Acessa o dicionário e usa 0 como valor padrão se a chave não existir
        int vitoriasJ1 = ranking.ContainsKey("Jogador X") ? ranking["Jogador X"] : 0;
        int vitoriasJ2 = ranking.ContainsKey("Jogador O") ? ranking["Jogador O"] : 0;
        int vitoriasMaquina = ranking.ContainsKey("Máquina") ? ranking["Máquina"] : 0;

        // Exibe o placar conforme o modo de jogo escolhido.
        if (modoJogo == 1)
        {
            CentralizarTexto($"Placar: Jogador X: {vitoriasJ1} | Jogador O: {vitoriasJ2}");
        }
        else if (modoJogo == 2)
        {
            CentralizarTexto($"Placar: Jogador X: {vitoriasJ1} | Máquina: {vitoriasMaquina}");
        }
        Console.WriteLine();
    }

    // Inicializa o tabuleiro com espaços vazios e exibe o placar.
    static void InicializarTabuleiro(int modoJogo)
    {
        Console.Clear();
        Placar(modoJogo);

        // Preenche o tabuleiro com espaços vazios.
        for (int linha = 0; linha < tabuleiro.GetLength(0); linha++)
        {
            // Percorre cada coluna da linha atual
            for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++)
            {
                tabuleiro[linha, coluna] = "   ";
            }
        }
    }

    // Metodo para exibir o tabuleiro com cores e centralizado.
    static void MostrarTabuleiro()
    {
        // Define a cor padrão para a interface
        ConsoleColor corPadrao = ConsoleColor.DarkCyan;
        Console.ForegroundColor = corPadrao;

        // Exibe os números das colunas centralizados
        CentralizarTexto("  1   2   3");

        // Itera sobre as linhas do tabuleiro
        for (int linha = 0; linha < tabuleiro.GetLength(0); linha++)
        {
            // Calcula o espaçamento necessário para centralizar a linha
            string linhaParaCalculo = $"{linha + 1} {tabuleiro[linha, 0]}|{tabuleiro[linha, 1]}|{tabuleiro[linha, 2]}";
            // Calcula a largura do console e o padding necessário
            int larguraConsole = Console.WindowWidth;
            int padding = Math.Max(0, (larguraConsole - linhaParaCalculo.Length) / 2);

            // Adiciona o padding antes de desenhar a linha
            Console.Write(new string(' ', padding));

            // Exibe o número da linha
            Console.Write($"{linha + 1} ");

            // Itera sobre as colunas da linha atual
            for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++)
            {
                // Obtém a peça na posição atual
                string peca = tabuleiro[linha, coluna];

                // Define a cor conforme a peça (X ou O)
                if (peca.Contains("X"))
                {
                    // Cor para o Jogador X
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (peca.Contains("O"))
                {
                    // Cor para o Jogador O
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                // Desenha a peça com a cor apropriada
                Console.Write(peca);
                // Restaura a cor padrão após desenhar a peça
                Console.ForegroundColor = corPadrao;

                // Desenha o separador de colunas, exceto após a última coluna
                if (coluna < 2)
                {
                    Console.Write("|");
                }
            }
            // Move para a próxima linha após desenhar todas as colunas
            Console.WriteLine();

            // Desenha a linha separadora entre as linhas do tabuleiro, exceto após a última linha
            if (linha < 2)
            {
                CentralizarTexto("  ---+---+---");
            }
        }
    }


    // Metodo para centralizar o texto no console.
    static void CentralizarTexto(string texto)
    {
        // Calcula a posição inicial para centralizar o texto
        int larguraConsole = Console.WindowWidth;
        int posicaoInicial = Math.Max(0, (larguraConsole - texto.Length) / 2);

        // Move o cursor para a posição inicial e escreve o texto
        Console.SetCursorPosition(posicaoInicial, Console.CursorTop);
        Console.WriteLine(texto);
    }

    // Metodo para verificar se um jogador venceu.
    static bool VerificarVencedor(string jogador)
    {
        // Verifica todas as linhas, colunas e diagonais para ver se o jogador venceu.
        for (int linha = 0; linha < 3; linha++)
        {
            // Verifica cada linha
            if (tabuleiro[linha, 0] == $" {jogador} " &&
                tabuleiro[linha, 1] == $" {jogador} " &&
                tabuleiro[linha, 2] == $" {jogador} ")
            {
                return true;
            }
        }

        // Verifica cada coluna
        for (int coluna = 0; coluna < 3; coluna++)
        {
            if (tabuleiro[0, coluna] == $" {jogador} " &&
                tabuleiro[1, coluna] == $" {jogador} " &&
                tabuleiro[2, coluna] == $" {jogador} ")
            {
                return true;
            }
        }

        // Verifica as diagonais
        if (tabuleiro[0, 0] == $" {jogador} " &&
            tabuleiro[1, 1] == $" {jogador} " &&
            tabuleiro[2, 2] == $" {jogador} ")
        {
            return true;
        }

        // Verifica a diagonal inversa
        if (tabuleiro[0, 2] == $" {jogador} " &&
            tabuleiro[1, 1] == $" {jogador} " &&
            tabuleiro[2, 0] == $" {jogador} ")
        {
            return true;
        }

        return false;
    }

    // Metodo para exibir o ranking dos jogadores.
    static void MostrarRanking()
    {
        Console.Clear();
        CentralizarTexto("=== RANKING ===");
        CentralizarTexto("-------------------");

        // Verifica se o ranking está vazio
        if (ranking.Count == 0) 
        {
            CentralizarTexto("Nenhum jogador no ranking ainda.");
        }
        // Se não estiver vazio, exibe o ranking ordenado
        else
        {
            // Ordena o ranking por número de vitórias em ordem decrescente
            var rankingOrdenado = ranking.OrderByDescending(p => p.Value);
            int i = 1;
            // Exibe cada jogador e suas vitórias
            foreach (var player in rankingOrdenado)
            {
                CentralizarTexto($"{i}. {player.Key} - Vitórias: {player.Value}");
                i++;
            }
        }
        CentralizarTexto("-------------------");
    }

    // Novo método para perguntar se o jogador deseja continuar ou voltar ao menu
    static bool PerguntarContinuar()
    {
        // Loop até o jogador escolher uma opção válida
        while (true)
        {
            CentralizarTexto("");
            CentralizarTexto("Você deseja continuar a partida ou voltar para o menu?");
            CentralizarTexto("Opções: 1 - Continuar   2 - Voltar para o menu");
            CentralizarTexto("Escolha uma opção: ");
            // Lê a resposta do jogador
            string resposta = Console.ReadLine();

            // Processa a resposta do jogador
            if (resposta == "1")
                return true;
            else if (resposta == "2")
                return false;
            else if (string.IsNullOrEmpty(resposta))
            {
                CentralizarTexto("Você não digitou nada. Pressione qualquer tecla para tentar novamente.");
                Console.ReadKey();
            }
            else
            {
                CentralizarTexto("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                Console.ReadKey();
            }
        }
    }

    // Métodos para carregar e salvar o ranking em um arquivo (comentados, mas prontos para uso)
    static void SalvarRanking()
    {
        // Salva o ranking em um arquivo de texto.
        const string ARQUIVO = "ranking.txt";
        // Converte o dicionário em linhas de texto no formato "nome,pontuação"
        var linhas = ranking.Select(p => $"{p.Key},{p.Value}").ToArray();
        // Escreve as linhas no arquivo
        File.WriteAllLines(ARQUIVO, linhas);
    }

    // static void CarregarRanking()
    static void AtualizarPontuacao(string nomeJogador)
    {
        // Carrega o ranking de um arquivo de texto.
        if (ranking.ContainsKey(nomeJogador))
        {
            // Se o jogador já existir no ranking, incrementa a pontuação.
            ranking[nomeJogador]++;
        }
        // Se o jogador não existir no ranking, adiciona com 1 vitória.
        else
        {
            ranking[nomeJogador] = 1;
        }
        SalvarRanking();
    }
}