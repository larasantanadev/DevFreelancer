using DevFreela.Core.DTOs; // Importa os DTOs de Skill
using DevFreela.Core.Repositories; // Importa o repositório de habilidades
using MediatR; // Importa o MediatR para lidar com a consulta
using System.Collections.Generic; // Importa para utilizar List<>
using System.Threading; // Importa para lidar com threads
using System.Threading.Tasks; // Importa para lidar com tarefas assíncronas

namespace DevFreela.Application.Queries.GetAllSkills
{
    // Manipulador de consulta para obter todas as habilidades
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillDTO>>
    {
        // Repositório de habilidades
        private readonly ISkillRepository _skillRepository;

        // Construtor que recebe o repositório de habilidades
        public GetAllSkillsQueryHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }





        // Método para lidar com a consulta para obter todas as habilidades
        public async Task<List<SkillDTO>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            // Chama o método GetAll do repositório para obter todas as habilidades
            return await _skillRepository.GetAllAsync();
        }
    }
}

                                                          


