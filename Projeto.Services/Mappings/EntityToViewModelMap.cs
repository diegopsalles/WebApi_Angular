using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Projeto.Entities;
using Projeto.Services.Models;

namespace Projeto.Services.Mappings
{
    //REGRA) Herdar Profile
    public class EntityToViewModelMap : Profile
    {
        //construtor -> ctor + 2x[tab]
        public EntityToViewModelMap()
        {
            CreateMap<Cliente, ClienteConsultaViewModel>();
        }
    }
}