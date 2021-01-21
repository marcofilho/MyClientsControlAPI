using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Models;

namespace DevIO.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<TelefoneViewModel, Telefone>();
            CreateMap<EnderecoViewModel, Endereco>();
            CreateMap<MidiaSocialViewModel, MidiaSocial>();

            CreateMap<Telefone, TelefoneViewModel>()
                .ForMember(dest => dest.NomeCliente, opt => opt.MapFrom(src => src.Cliente.Nome));
            CreateMap<Endereco, EnderecoViewModel>()
              .ForMember(dest => dest.NomeCliente, opt => opt.MapFrom(src => src.Cliente.Nome));
            CreateMap<MidiaSocial, MidiaSocialViewModel>()
              .ForMember(dest => dest.NomeCliente, opt => opt.MapFrom(src => src.Cliente.Nome));
        }
    }
}