using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {


        public Tabuleiro tabuleiro { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public PartidaDeXadrez()
        {
            tabuleiro = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in capturadas)
            {
                if(peca.cor == cor)
                {
                    aux.Add(peca);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in pecas)
            {
                if (peca.cor == cor)
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tabuleiro.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {
            colocarNovaPeca('d', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tabuleiro, Cor.Branca));
            colocarNovaPeca('f', 2, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('f', 1, new Torre(tabuleiro, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tabuleiro, Cor.Branca));

            colocarNovaPeca('d', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tabuleiro, Cor.Preta));
            colocarNovaPeca('f', 7, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('f', 8, new Torre(tabuleiro, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tabuleiro, Cor.Preta));

           
        }
        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = tabuleiro.retirarPeca(origem);
            peca.incrementarQteDeMovimentos();
            Peca pecaCapturada = tabuleiro.retirarPeca(destino);
            tabuleiro.colocarPeca(peca, destino);
            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem,destino);
            turno++;
            mudaJogador();
        }
        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tabuleiro.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tabuleiro.peca(pos).cor)
            {
                throw new TabuleiroException("A peça escolhida para jogar não é sua!");
            }
            if (!tabuleiro.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça de origem escolhida!");
            }
        }
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tabuleiro.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino invalida!");
            }
        }
        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
                jogadorAtual = Cor.Preta;
            else
                jogadorAtual = Cor.Branca;
        }

    }
}
