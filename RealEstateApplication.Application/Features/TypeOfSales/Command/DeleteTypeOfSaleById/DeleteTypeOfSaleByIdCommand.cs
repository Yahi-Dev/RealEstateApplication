using MediatR;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Domain.Entities;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Wrappers;
using System.Net;

namespace RealEstateApplication.Application.Features.TypeOfSales.Command.DeleteTypeOfSaleById
{
    public class DeleteTypeOfSaleByIdCommand:IRequest <Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteTypeOfSaleByIdCommandHandler : IRequestHandler<DeleteTypeOfSaleByIdCommand,  Response<int>>
    {
        private readonly ITypeOfSaleRepository _typeOfSaleRepository;

        public DeleteTypeOfSaleByIdCommandHandler(ITypeOfSaleRepository typeOfSaleRepository)
        {
            _typeOfSaleRepository = typeOfSaleRepository;
        }
        public async Task<Response<int>> Handle(DeleteTypeOfSaleByIdCommand command, CancellationToken cancellationToken)
        {
            var typeOfSale = await _typeOfSaleRepository.GetByIdAsync(command.Id);
            if (typeOfSale is null) throw new ApiException("Type of sale not found", (int)HttpStatusCode.NoContent);
            await DeleteRelationships(typeOfSale.Id);
            await _typeOfSaleRepository.DeleteAsync(typeOfSale);
            return new Response<int>(1);
        }
        private async Task DeleteRelationships(int idTypeOfSale)
        {

        }
    }
}
