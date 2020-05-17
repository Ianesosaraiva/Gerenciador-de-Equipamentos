using GerenciadorDeEquipamentos.Controllers;
using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GerenciadorDeEquipamentos.Security
{
    public class ProvedorDeSeguranca : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var bd = new shield01Entities();

            //captura o id da pessoa e converte para inteiro
            var pessoaId = Convert.ToInt32(username);

            //busca no banco pela pessoa mencionada no username
            var acessoSalvo = bd.Pessoas.FirstOrDefault(x => x.PessoaId == pessoaId).Acessos.Descricao;

            //busca por todos os acessos salvos

            string[] acesso = new string[1];
            acesso[0] = acessoSalvo;

            return acesso;

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}