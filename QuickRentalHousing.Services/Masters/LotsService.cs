using Microsoft.EntityFrameworkCore;
using QuickRentalHousing.Domains.Entities.Masters;
using QuickRentalHousing.Domains.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRentalHousing.Services.Masters
{
    public class LotsService : ILotsService
    {
        private readonly IRepository<Lot> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStreetsService _streetsService;
        private readonly ILotHomeownersService _LotHomeownersService;

        public LotsService(IRepository<Lot> repository,
            IUnitOfWork unitOfWork,
            IStreetsService streetsService,
            ILotHomeownersService LotHomeownersService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _streetsService = streetsService;
            _LotHomeownersService = LotHomeownersService;
        }

    }

    public interface ILotsService
    {
       
    }
}
