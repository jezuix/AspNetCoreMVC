using Xunit;
using MyFinance.Models;

namespace ProjetoTeste
{
    public class UnitTestModels
    {
        [Fact]
        public void TesteLoginUsuario()
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            usuarioModel.Email = "teste@teste.com";
            usuarioModel.Senha = "123456";
            Assert.True(usuarioModel.ValidarLogin());
        }

        [Fact]
        public void TesteRegistrarUsuario()
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            usuarioModel.Nome = "Teste";
            usuarioModel.Data_Nascimento = "1988/04/10";
            usuarioModel.Email = "teste22@teste2.com";
            usuarioModel.Senha = "123456";
            usuarioModel.RegistrarUsuario();

            Assert.True(usuarioModel.ValidarLogin());
        }
    }
}
