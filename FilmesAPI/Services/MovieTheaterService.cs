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
        private readonly FilmesContext _DbContext;
        private readonly IMapper _mapper;

        public MovieTheaterService(FilmesContext dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _mapper = mapper;
        }

        public ReadMovieTheaterDTO Create(MovieTheaterDTO movieTheaterDTO)
        {
            VerifyAddressAvailability(movieTheaterDTO.AddressId);

            MovieTheater movieTheater = _mapper.Map<MovieTheater>(movieTheaterDTO);

            _DbContext.Add(movieTheater);
            _DbContext.SaveChanges();

            ReadMovieTheaterDTO readDTO = _mapper.Map<ReadMovieTheaterDTO>(movieTheater);

            return readDTO;
        } 

        public List<ReadMovieTheaterDTO> GetAll()
        {
            List<MovieTheater> movieTheatersList = _DbContext.MovieTheaters.ToList();
            List<ReadMovieTheaterDTO> readDTOList = _mapper.Map<List<ReadMovieTheaterDTO>>(movieTheatersList);
            
            return readDTOList;
        }

        public ReadMovieTheaterDTO GetById(int id)
        {
            MovieTheater movieTheater = _DbContext.MovieTheaters.FirstOrDefault(m => m.Id == id);

            if(movieTheater == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIETHEATER);
            }

            ReadMovieTheaterDTO readDTO = _mapper.Map<ReadMovieTheaterDTO>(movieTheater);

            return readDTO;
        }

        public ReadMovieTheaterDTO Update(int id, MovieTheaterDTO movieTheaterDTO)
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

            ReadMovieTheaterDTO readDTO = _mapper.Map<ReadMovieTheaterDTO>(movieTheater);

            return readDTO;
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
