using ETickets.SharedLibrary.Implementaion;
using ProductApi.Application.Intefaces;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Data;

namespace ProductApi.Infrastructure.Repositories
{
    public class ProducerRepository(MovieDbContext context) : GenericClass<Producer>(context), IProducer
    {
    }
}
