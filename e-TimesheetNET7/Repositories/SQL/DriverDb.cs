﻿using Dapper;
using e_TimesheetNET7.Config;
using e_TimesheetNET7.Models.Contract;
using e_TimesheetNET7.Models.Driver;
using System.Transactions;

namespace e_TimesheetNET7.Repositories.SQL
{
    public interface IDriverDb
    {
        Task<HeaderContract> GetHeader(string noContract);
        Task<DetailContract> GetDetail(string noContract);
        Task<DetailDetailContract> GetDetail2(string noContract);
        Task<bool> InsertDriverPermit(DriverGbLimoRequest request);
        Task<DriverItem> GetDriver(string nip);
    }

    public class DriverDb : IDriverDb
    {
        private readonly IConnectionFactoryDb _connect;
        public DriverDb(IConnectionFactoryDb connect)
        {
            _connect = connect;
        }

        public async Task<DriverItem> GetDriver(string nip)
        {
            DriverItem driver = new DriverItem();
            var conn = await _connect.CreateODBCConnectionAsync();
            try
            {                
                var query = @"select NIP, KdGolongan2, NoHp from SAP_MTPengemudi where NIP like ? ";
                var param = new DynamicParameters();
                param.Add("NIP", $"%{nip}%");
                driver = await conn.QueryFirstOrDefaultAsync<DriverItem>(query, param);

                return driver;
            }
            catch
            {
                throw;
            }           
        }

        public async Task<HeaderContract> GetHeader(string noContract)
        {
            throw new NotImplementedException();
        }

        public Task<DetailContract> GetDetail(string noContract)
        {
            throw new NotImplementedException();
        }

        public Task<DetailDetailContract> GetDetail2(string noContract)
        {
            throw new NotImplementedException();
        }        

        public async Task<bool> InsertDriverPermit(DriverGbLimoRequest request)
        {
            var conn = await _connect.GbLimoConnection();
            var transaction = await conn.BeginTransactionAsync();

            try
            {
                var query = @"INSERT INTO [LMO_MTIzinPengemudi] ([Id],[NIP],[NoKontrak],[NoItem],[NoDetil],[NIPPengganti],[TanggalIzin],[Status]) VALUES (?,?,?,?,?,?,?,?)";
                var param = new DynamicParameters();
                param.Add("Id", request.Id);
                param.Add("NIP", request.NIP);
                param.Add("NoKontrak", request.NoKontrak);
                param.Add("NoItem", request.NoItem);
                param.Add("NoDetil", request.NoDetil);
                param.Add("NIPPengganti", request.NIPPengganti);
                param.Add("TanggalIzin", request.TanggalIzin);
                param.Add("Status", request.Status);                
                var result = await conn.ExecuteAsync(query, param, transaction);
                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
