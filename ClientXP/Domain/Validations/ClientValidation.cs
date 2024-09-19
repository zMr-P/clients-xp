using ClientXP.Domain.Entities;
using FluentValidation;

namespace ClientXP.Domain.Validations
{
    public class ClientValidation : AbstractValidator<Client>
    {
        public ClientValidation()
        {
            RuleFor(client => client.Name)
                .NotEmpty()
                .WithMessage("O nome é obrigatório")
                .Length(2, 100)
                .WithMessage("O nome deve ter entre 2 e 100 caracteres");
            RuleFor(client => client.Email)
                .NotEmpty()
                .WithMessage("O email é obrigatório")
                .EmailAddress()
                .WithMessage("O email não é valido");
            RuleFor(client => client.CPF)
                .NotEmpty()
                .WithMessage("O CPF é obrigatório")
                //Regex CPF\\ - mr.p
                .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$")
                .WithMessage("O CPF deve seguir o padrão 000.000.000-00");
        }
    }
}

