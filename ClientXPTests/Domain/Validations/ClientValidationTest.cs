using ClientXP.Domain.Entities;
using ClientXP.Domain.Validations;

namespace ClientXPTests.Domain.Validations
{
    public class ClientValidationTest
    {
        private readonly ClientValidation _validator;
        public ClientValidationTest()
        {
            _validator = new ClientValidation();
        }
        [Fact]
        public void ShouldHaveErrorWhenNameEmpty()
        {
            var client = new Client
            {
                Name = "",
                Email = "test@example.com",
                CPF = "111.222.333-55"
            };
            var result = _validator.Validate(client);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e =>
            e.PropertyName == "Name" && e.ErrorMessage == "O nome é obrigatório");
        }
        [Fact]
        public void ShouldPassWhenClientIsValid()
        {
            var client = new Client
            {
                Name = "Paulo",
                Email = "paulo@example.com",
                CPF = "663.951.240-80"
            };
            var result = _validator.Validate(client);
            Assert.True(result.IsValid);
        }
    }
}
