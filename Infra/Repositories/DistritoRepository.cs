﻿using System;
using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Infra.Repositories
{
    public class DistritoRepository : IDistritoRepository
    {
        private readonly Context _context;

        public DistritoRepository(Context context)
        {
            _context = context;
        }

        public Distrito Atualizar(Distrito distrito)
        {
            throw new NotImplementedException();
        }

        public Distrito Buscar(string numero)
        {
            throw new NotImplementedException();
        }

        public Distrito Incluir(Distrito distrito)
        {
            return _context.Distrito.Add(distrito);
        }
    }
}