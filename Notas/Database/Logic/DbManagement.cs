using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notas.Database.Logic
{
    public class DbManagement
    {
        private NotasContext context;


        public DbManagement(NotasContext context)
        {
            this.context = context;
        }

        public void CheckStart()
        {
            if (context.Notas.Count() == 0)
            {
                context.Notas.Add( new Notas.Models.POCOs.Notas
                {
                    Important = false
                    , Title = "Registro Uno"
                    , Text = "Texto del registro Uno"
                });
                context.Notas.Add( new Models.POCOs.Notas
                {
                    Important = false
                    , Title = "Registro Dos"
                    , Text = "Texto del registro Dos"
                });
                context.Notas.Add( new Models.POCOs.Notas
                {
                    Important = false
                    , Title = "Registro Tres"
                    , Text = "Texto del registro Tres"
                });
                context.Notas.Add( new Models.POCOs.Notas
                {
                    Important = true
                    , Title = "Registro Cuatro"
                    , Text = "Texto del registro Cuatro"
                });
                context.Notas.Add( new Models.POCOs.Notas
                {
                    Important = true
                    , Title = "Registro Cinco"
                    , Text = "Texo del registro Cinco"
                });

                context.SaveChanges();
            }
        }

        public List<Models.POCOs.Notas> GetNotas()
        {
            return context.Notas.OrderBy(x => x.Id).OrderByDescending(x => x.Important).ToList();
        }
        public List<SelectListItem> GetNotasL()
        {
            List<SelectListItem> ret = new List<SelectListItem>();

            var query = GetNotas();

            foreach (var n in query )
                ret.Add
                    (
                        new SelectListItem
                        {
                            Text = n.Title
                            , Value = n.Id.ToString()
                            , Selected = n == query.FirstOrDefault()
                        }
                    );

            return ret;
        }

        public Models.POCOs.Notas GetNota(int id)
        {
            try
            {
                return context.Notas.Where(x => x.Id == id).Single();
            }
            catch { return new Models.POCOs.Notas { Id = -1, Text = "error" }; }
        }

        public int AddNota()
        {
            context.Notas.Add(new Notas.Models.POCOs.Notas { Title = "new reg", Text = "?" });
            context.SaveChanges();

            return context.Notas.OrderByDescending(x => x.Id).FirstOrDefault().Id;
        }

        public void SaveNota(Models.POCOs.Notas nota)
        {
            var n = context.Notas.SingleOrDefault(x => x.Id == nota.Id);

            n.Important = nota.Important;
            n.Title = nota.Title;
            n.Text = nota.Text;

            context.SaveChanges();
        }

        public void DeleteNota(Models.POCOs.Notas nota)
        {
            context.Notas.Remove(context.Notas.Where(x => x.Id == nota.Id).Single());

            context.SaveChanges();
        }
    }
}
