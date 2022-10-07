using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services
{
    public class MovieTheaterService
    {
        private FilmesContext _DbContext;
        private IMapper _mapper;

        public MovieTheaterService(FilmesContext dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _mapper = mapper;
        }

        public MovieTheater Create(MovieTheaterDTO movieTheaterDTO)
        {
            VerifyAddressAvailability(movieTheaterDTO.AddressId);

            MovieTheater newMovie = _mapper.Map<MovieTheater>(movieTheaterDTO);

            _DbContext.Add(newMovie);
            _DbContext.SaveChanges();

            return newMovie;
        } 

        public List<ReadMovieTheaterDTO> GetAll()
        {
            List<MovieTheater> movieTheaterList = _DbContext.MovieTheaters.ToList();
            List<ReadMovieTheaterDTO> movieTheaterDTOList = _mapper.Map<List<ReadMovieTheaterDTO>>(movieTheaterList);
            
            return movieTheaterDTOList;
        }

        public ReadMovieTheaterDTO GetById(int id)
        {
            MovieTheater movieTheater = _DbContext.MovieTheaters.FirstOrDefault(m => m.Id == id);

            if(movieTheater == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIETHEATER);
            }

            ReadMovieTheaterDTO movieTheaterDTO = _mapper.Map<ReadMovieTheaterDTO>(movieTheater);

            return movieTheaterDTO;
        }

        public MovieTheater Update(int id, MovieTheaterDTO movieTheaterDTO)
        {

            MovieTheater movieTheater = _DbContext.MovieTheaters.FirstOrDefault(m => m.Id == id);

            if (movieTheater == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIETHEATER);
            }

            if(movieTheater.AddressId != movieTheaterDTO.AddressId)
            {
                VerifyAddressAvailability(movieTheaterDTO.AddressId);
            }

            _mapper.Map(movieTheaterDTO, movieTheater);

            _DbContext.SaveChanges();

            return movieTheater;
        }

        public void Delete(int id)
        {
            MovieTheater movieTheater = _DbContext.MovieTheaters.FirstOrDefault(mt => mt.Id == id);

            if(movieTheater == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIETHEATER);
            }

            _DbContext.Remove(movieTheater);
            _DbContext.SaveChanges();
        }

        private void VerifyAddressAvailability(int addressId)
        {
            MovieTheater addressRelatedMovieTheater = _DbContext.MovieTheaters.FirstOrDefault(movieTheater => movieTheater.AddressId == addressId);

            if(addressRelatedMovieTheater != null)
            {
                throw new ElementBeingUsedException(ElementType.ADDRESS);
            }
        }
    }
}
