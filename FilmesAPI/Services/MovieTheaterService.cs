using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Models.Enums;
using FilmesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services
{
    public class MovieTheaterService
    {
        private readonly IMovieTheaterRepository _repository;
        private readonly IMapper _mapper;

        public MovieTheaterService(IMovieTheaterRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ReadMovieTheaterDTO Create(MovieTheaterDTO movieTheaterDTO)
        {
            VerifyAddressAvailability(movieTheaterDTO.AddressId);

            MovieTheater movieTheater = _mapper.Map<MovieTheater>(movieTheaterDTO);

            _repository.Add(movieTheater);

            ReadMovieTheaterDTO readDTO = _mapper.Map<ReadMovieTheaterDTO>(movieTheater);

            return readDTO;
        } 

        public List<ReadMovieTheaterDTO> GetAll(string? movieName)
        {
            List<MovieTheater> movieTheatersList = _repository.GetAll();

            if(movieName != null)
            {
                IEnumerable<MovieTheater> query = from movieTheater in movieTheatersList
                                                  where movieTheater.Sessions.Any(session => session.Movie.Name == movieName)
                                                  select movieTheater;

                movieTheatersList = query.ToList();
            }

            List<ReadMovieTheaterDTO> readDTOList = _mapper.Map<List<ReadMovieTheaterDTO>>(movieTheatersList);
            
            return readDTOList;
        }

        public ReadMovieTheaterDTO GetById(int id)
        {
            MovieTheater movieTheater = _repository.GetById(id);

            if (movieTheater == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIETHEATER);
            }

            ReadMovieTheaterDTO readDTO = _mapper.Map<ReadMovieTheaterDTO>(movieTheater);

            return readDTO;
        }

        public ReadMovieTheaterDTO Update(int id, MovieTheaterDTO movieTheaterDTO)
        {
            MovieTheater movieTheater = _repository.GetById(id);

            if (movieTheater == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIETHEATER);
            }

            if(movieTheater.AddressId != movieTheaterDTO.AddressId)
            {
                VerifyAddressAvailability(movieTheaterDTO.AddressId);
            }

            _mapper.Map(movieTheaterDTO, movieTheater);

            _repository.Update(movieTheater);

            ReadMovieTheaterDTO readDTO = _mapper.Map<ReadMovieTheaterDTO>(movieTheater);

            return readDTO;
        }

        public void Delete(int id)
        {
            MovieTheater movieTheater = _repository.GetById(id);

            if(movieTheater == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIETHEATER);
            }

            _repository.Delete(movieTheater);
        }

        private void VerifyAddressAvailability(int addressId)
        {
            if(_repository.VerifyIfAddressIsBeingUsed(addressId))
            {
                throw new ElementBeingUsedException(ElementType.ADDRESS);
            }
        }
    }
}
