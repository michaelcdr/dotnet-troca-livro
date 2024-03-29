﻿using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public class Categoria : Entidade
    {
        public int Id { get; set; }
        public string Nome { get; private set; }
        public List<SubCategoria> SubCategorias { get; private set; }

        public override bool TaValido()
        {
            return true;
        }

        public Categoria()
        {
            this.SubCategorias = new List<SubCategoria>();
        }

        public Categoria(string nome)
        {
            this.Nome = nome;
            this.SubCategorias = new List<SubCategoria>();
        }

        public void AdicionarSubCategorias(List<SubCategoria> subCategorias)
        {
            if (this.SubCategorias == null) this.SubCategorias = new List<SubCategoria>();

            this.SubCategorias.AddRange(subCategorias);
        }
    }
}