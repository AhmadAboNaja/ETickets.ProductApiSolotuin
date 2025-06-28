using ProductApi.Domain.Entities;

namespace ProductApi.Application.DTOs.Conversions
{
    public static class ProducerConversion
    {
        public static Producer ToEntity(CreateProducerDTO producerDTO) => new()
        {
            ProfilePictureURL = producerDTO.ProfilePictureURL,
            FullName = producerDTO.FullName,
            Bio = producerDTO.Bio
        };
        public static Producer ToEntity(UpdateProducerDTO producerDTO) => new()
        {
            Id = producerDTO.Id,
            ProfilePictureURL = producerDTO.ProfilePictureURL,
            FullName = producerDTO.FullName,
            Bio = producerDTO.Bio
        };
        public static Producer ToEntity(ProducerDTO producerDTO) => new()
        {
            Id = producerDTO.Id,
            ProfilePictureURL = producerDTO.ProfilePictureURL,
            FullName = producerDTO.FullName,
            Bio = producerDTO.Bio
        };
        public static (ProducerDTO?, IEnumerable<ProducerDTO>?) FromEntity(Producer? producer, IEnumerable<Producer>? producers)
        {
            if (producer is not null)
            {
                var singlProducer = new ProducerDTO(
                    producer!.Id,
                    producer.ProfilePictureURL,
                    producer.FullName,
                    producer.Bio,
                    MovieResponseConverisons.FromEntity(null, producer.Movies).Item2
                    );
                return (singlProducer, null);
            }
            if (producers is not null)
            {
                var _producers = producers!.Select(a =>
                new ProducerDTO(
                    a!.Id,
                    a.ProfilePictureURL,
                    a.FullName,
                    a.Bio,
                    MovieResponseConverisons.FromEntity(null, a.Movies).Item2
                    )
                ).ToList();
                return (null, _producers);
            }
            return (null, null);
        }
    }
}
