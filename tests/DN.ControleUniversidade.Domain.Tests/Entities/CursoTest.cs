﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DN.ControleUniversidade.Domain.Entities;
using System.Linq;

namespace DN.ControleUniversidade.Domain.Tests.Entities
{
    [TestClass]
    public class CursoTest
    {
       

        [TestMethod]
        [TestCategory("Entity Curso")]
        public void DescricaoDeveTerEntre5e50Caracteres() 
        {
            string validacaoEsperada = "A descrição deve conter entre 5 e 50 caracteres";
            var curso = new Curso("mater");


            string validacaoQuebrada = curso.ResultadoValidacao.Erros.FirstOrDefault(x => x.Message == validacaoEsperada).Message;

            Assert.AreEqual(validacaoEsperada, validacaoQuebrada);
        }

        [TestMethod]
        [TestCategory("Entity Curso")]
        public void NaoDeveAceitarCursoSemSemestres()
        {
            var curso = new Curso("Análise de Sistemas");

            Assert.IsTrue(curso.IsValid);

            curso.AtivarCurso();

            Assert.IsFalse(curso.IsValid);
            Assert.IsTrue(curso.ResultadoValidacao.Erros.Any(e => e.Message == "O curso não possui nenhum semestre"));
            Assert.IsTrue(curso.ResultadoValidacao.Erros.Any(e => e.Message == "O curso deve possuir pelo menos 4 semestres"));
            Assert.IsTrue(curso.ResultadoValidacao.Erros.Count() == 2);
        }

        [TestMethod]
        [TestCategory("Entity Curso")]
        public void NaoDeveAceitarCursoComMenosDeQuatroSemestres()
        {
            var curso = new Curso("Ciências da Computação");
            Semestre semestre = new Semestre();
            curso.Semestres.Add(semestre);

            Assert.IsTrue(curso.Semestres.Any());

            //Curso criado com sucesso
            Assert.IsTrue(curso.IsValid);

            curso.AtivarCurso();
            Assert.IsTrue(curso.ResultadoValidacao.Erros.Count() == 1);
            Assert.IsFalse(curso.Ativo);
            Assert.IsFalse(curso.IsValid);
            Assert.IsTrue(curso.ResultadoValidacao.Erros.Any(e => e.Message == "O curso deve possuir pelo menos 4 semestres"));
        }

        [TestMethod]
        [TestCategory("Entity Curso")]
        public void Na_Atualizacao_Do_Curso_Deve_Alterar_DataAtualizacao()
        {
            var curso = new Curso("Ciências da Computação");
            DateTime dataAtuzalicaoCriacao = curso.DataAtualizacao;
            System.Threading.Thread.Sleep(2000);
            curso.AtualizarCurso("Novo Nome de Curso");

            Assert.IsTrue(dataAtuzalicaoCriacao != curso.DataAtualizacao);
        }
    }
}
