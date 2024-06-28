using MediatR;
using RealEstateApplication.Core.Application.Interfaces.Repositories;
using RealEstateApplication.Core.Domain.Entities;
using RealEstateApplication.Application.Exceptions;
using RealEstateApplication.Application.Wrappers;
using System.Net;

namespace RealEstateApplication.Core.Application.Features.TypeOfSales.Command.DeleteTypeOfSaleById
{
    public class DeleteTypeOfSaleByIdCommand:IRequest <Response<Unit>>
    {
        public int Id { get; set; }
    }
    public class DeleteTypeOfSaleByIdCommandHandler : IRequestHandler<DeleteTypeOfSaleByIdCommand,  Response<Unit>>
    {
        private readonly ITypeOfSaleRepository _typeOfSaleRepository;

        public DeleteTypeOfSaleByIdCommandHandler(ITypeOfSaleRepository typeOfSaleRepository)
        {
            _typeOfSaleRepository = typeOfSaleRepository;
        }
        public async Task<Response<Unit>> Handle(DeleteTypeOfSaleByIdCommand command, CancellationToken cancellationToken)
        {
            var typeOfSale = await _typeOfSaleRepository.GetByIdAsync(command.Id);
            if (typeOfSale is null) throw new ApiException("Type of sale not found", (int)HttpStatusCode.NoContent);
            await DeleteRelationships(typeOfSale.Id);
            await _typeOfSaleRepository.DeleteAsync(typeOfSale);
            return new Response<Unit>(Unit.Value);
        }
        private async Task DeleteRelationships(int idTypeOfSale)
        {

        }
    }
}
