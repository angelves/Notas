using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notas.Models
{
    public class IndexModel
    {
        public List<SelectListItem> NotasL { get; set; }

        public POCOs.Notas SelectedNota { get; set; }

    }
}
