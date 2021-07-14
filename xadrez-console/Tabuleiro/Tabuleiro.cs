using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas;
        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }

        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }
        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna];
        }
        public void colocarPeca(Peca peca, Posicao pos)
        {
            if (existePeca(pos))
                throw new TabuleiroException("Ja existe uma peça nesta posição!");
            pecas[pos.linha, pos.coluna] = peca;
            peca.posicao = pos;
        }
        public bool existePeca(Posicao pos)
        {
            valdarPosicao(pos);
            return peca(pos) != null;
        }
        public bool posicaoEhValida(Posicao pos)
        {
            if (pos.linha < 0 || pos.coluna < 0 || pos.linha >= linhas || pos.coluna >= colunas)
            {
                return false;
            }
            return true;
        }
        public void valdarPosicao(Posicao pos)
        {
            if (!posicaoEhValida(pos))
            {
                throw new TabuleiroException("Posição invalida!");
            }
        }

    }
}
