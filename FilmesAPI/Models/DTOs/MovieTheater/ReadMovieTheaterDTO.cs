﻿using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class ReadMovieTheaterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}