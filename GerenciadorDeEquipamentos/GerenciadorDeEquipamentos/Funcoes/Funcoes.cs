using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
namespace GerenciadorDeEquipamentos.Funcoes
{
    public class Funcoes
    {
        public static void EnviaEmail(int configId, string DestinatarioEmail, string TextoMensagem, bool HtmlMensagem)
        {
            if (DestinatarioEmail != null && DestinatarioEmail != "")
            {
                using (var bd = new shield01Entities())
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        var InfoEmail = bd.EmailConfig.FirstOrDefault(x => x.ConfigId == configId);

                        mail.From = new MailAddress(InfoEmail.EmailUsuario, InfoEmail.EnderecoEmail);
                        mail.To.Add(DestinatarioEmail);
                        mail.Subject = InfoEmail.EnderecoEmail + " (Mensagem automática, não responder)";
                        mail.Body = TextoMensagem;

                        mail.IsBodyHtml = HtmlMensagem;

                        // Utilizar apenas se houver anexo de Arquivos 
                        // mail.Attachments.Add(new Attachment(Server.MapPath(@"~\contratos\fadba_" + num_processo + "_" + num_candidato + ".pdf")));

                        using (SmtpClient smtp = new SmtpClient(InfoEmail.NomeServidor, Convert.ToInt32(InfoEmail.NumeroPorta)))
                        {
                            smtp.Credentials = new NetworkCredential(InfoEmail.EmailUsuario, InfoEmail.EmailSenha);
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                    }
                }
            }

        }
    }
}