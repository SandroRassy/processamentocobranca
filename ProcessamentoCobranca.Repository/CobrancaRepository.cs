﻿using MongoDB.Driver;
using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Repository.Base;
using ProcessamentoCobranca.Repository.Context;
using ProcessamentoCobranca.Repository.Interfaces;

namespace ProcessamentoCobranca.Repository
{
    public sealed class CobrancaRepository : Repository<Cobranca>, ICobrancaRepository
    {
        public CobrancaRepository(IMongoCollection<Cobranca> collectionName) : base(collectionName)
        {
        }

        public CobrancaRepository(IConnectionFactory connectionFactory, string databaseName, string collectionName)
            : base(connectionFactory, databaseName, collectionName)
        {
        }

        public IQueryable<Cobranca> QueryCPF(string cpf)
        {
            var retorno = _collectionName.AsQueryable<Cobranca>().Where(w => w.CPF == cpf);
            return retorno;
        }

        public Cobranca QueryFilter(DateTime dataVencimento, string cpf)
        {
            var retorno = new Cobranca();

            if (!String.IsNullOrEmpty(cpf))
                retorno = _collectionName.AsQueryable<Cobranca>().FirstOrDefault(w => w.CPF == cpf);
            else
                retorno = _collectionName.AsQueryable<Cobranca>().FirstOrDefault(w => w.DataVencimento == dataVencimento);

            return retorno;
        }

        public IQueryable<Cobranca> QueryRefMes(DateTime dataInicio, DateTime dataFim, string cpf)
        {
            var retorno = _collectionName.AsQueryable<Cobranca>().Where(w => w.CPF == cpf && w.DataVencimento >= dataInicio && w.DataVencimento <= dataFim);
            return retorno;
        }

        public IQueryable<Cobranca> QueryRefMes(DateTime dataInicio, DateTime dataFim)
        {
            var retorno = _collectionName.AsQueryable<Cobranca>().Where(w => w.DataVencimento >= dataInicio && w.DataVencimento <= dataFim);
            return retorno;
        }
    }
}
