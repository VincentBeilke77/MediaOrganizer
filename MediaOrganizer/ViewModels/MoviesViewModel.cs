using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCatalog.Models;

namespace MediaOrganizer.ViewModels
{
    public class MoviesViewModel
    {
        public IEnumerable<MovieModel> Movies { get; set; }
        public string Category { get; set; }
    }
}