using GestionHuil.Controllers.Ressources;
using GestionHuil.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuil.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeRessource>();
            CreateMap<EmployeeRessource, Employee>();
            CreateMap<TriturationAllRessource, Trituration>();
            CreateMap <Trituration,TriturationAllRessource>();
            CreateMap<StockageOliveRessource, StockageOlive>();
            CreateMap<Trituration, TriturationAchatRessourcecs>();

        }
       
    }
}
