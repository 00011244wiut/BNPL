﻿using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface ILegalDataRepository : IGenericRepository<LegalDataEntity>
{
    Task<LegalDataEntity> MockLegalData(string city);
}