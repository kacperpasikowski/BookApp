using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.helpers;

namespace API.Services
{
    public interface ISearchService
    {
        Task<PagedList<SearchResultDto>> SearchAsync(UserParams userParams, string query);

    }
}