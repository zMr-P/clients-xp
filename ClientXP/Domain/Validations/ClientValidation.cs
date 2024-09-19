using ClientXP.Application.Services;
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
                .WithMessage("O CPF deve seguir o padrão 000.000.000-00")
                .Must(ValidCPF)
                .WithMessage("O CPF é invalido");
        }

        private bool ValidCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11) return false;

            int[] multiply1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiply2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum = 0;
            int rest;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(cpf[i].ToString()) * multiply1[i];

            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            if (rest != int.Parse(cpf[9].ToString()))
                return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(cpf[i].ToString()) * multiply2[i];

            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            if (rest != int.Parse(cpf[10].ToString()))
                return false;

            return true;
        }
    }
}


