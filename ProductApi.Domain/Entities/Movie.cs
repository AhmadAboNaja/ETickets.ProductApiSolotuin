using ProductApi.Domain.Data;
using ETickets.SharedLibrary.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Domain.Entities
{
    public class Movie : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImageURL { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }
        //Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }
        //Cenima
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        //Producer
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
    }
}
