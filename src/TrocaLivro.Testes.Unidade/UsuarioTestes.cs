using System;
using TrocaLivro.Dominio.Entidades;
using Xunit;

namespace TrocaLivro.Testes.Unidade
{
    public class UsuarioTestes
    {
        [Theory]
        [InlineData(10, 10)]
        public void UsuarioComPontosDeveEfeturaDebitoDePontosComSucesso(int pontosDisponiveis, int pontosDebitados)
        {
            var usuario = new Usuario("Michael", "michael", "michael@gia.com.br", "Costa dos Reis");
            int pontosEsperados = pontosDisponiveis - pontosDebitados;
            usuario.AdicionarPontos(pontosDisponiveis);
            usuario.DebitarPontos(pontosDebitados);

            Assert.Equal(usuario.Pontos, pontosEsperados);
        }

        [Theory]
        [InlineData(10)]
        public void UsuarioComPontosInsuficientesAoDebitarLancaException(int pontosDisponiveis)
        {
            var usuario = new Usuario("Michael", "michael", "michael@gia.com.br", "Costa dos Reis");
            int pontosDebitados = pontosDisponiveis * 2; 

            usuario.AdicionarPontos(pontosDisponiveis);
            
            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    usuario.DebitarPontos(pontosDebitados);
                });
        }
    }
}