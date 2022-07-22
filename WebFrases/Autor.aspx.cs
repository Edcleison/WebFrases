using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrases.DAL;
using WebFrases.MODELO;

namespace WebFrases
{
    public partial class Autor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AtualizaGrid();
        }
        public void AtualizaGrid()
        {
            DALAutor dal = new DALAutor
                ();
            gvDados.DataSource = dal.Localizar();
            gvDados.DataBind();
        }
        public void LimparCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            btnSalvar.Text = "Inserir";
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                String msg = "";
                String caminho = Server.MapPath(@"IMAGENS\AUTORES\");
                DALAutor dal = new DALAutor();
                ModeloAutor obj = new ModeloAutor();
                obj.Nome = txtNome.Text;
                //faz o upload da foto e salva o nome no obj
                if (fuFoto.PostedFile.FileName != "")
                {
                    obj.Foto = DateTime.Now.Millisecond.ToString() + fuFoto.PostedFile.FileName;
                    String img = caminho + obj.Foto;
                    fuFoto.PostedFile.SaveAs(img);
                }
                if (btnSalvar.Text == "Inserir")
                {
                    dal.Inserir(obj);
                    msg = "<script> ShowMsg('Atenção:','O código gerado foi: " + obj.Id.ToString() + "'); </script>";
                }
                else
                {
                    //alterar
                    obj.Id = Convert.ToInt32(txtId.Text);
                    //verificar se existe foto existe e deletar
                    ModeloAutor aold = dal.GetRegistro(obj.Id);
                    if (aold.Foto != "")
                    {
                        File.Delete(caminho + aold.Foto);
                    }
                    dal.Alterar(obj);
                    msg = "<script> ShowMsg('Atenção:','Registro alterado corretamente!!!'); </script>";
                }
                //Response.Write(msg);
                PlaceHolder1.Controls.Add(new LiteralControl(msg));
                this.LimparCampos();
            }

            catch (Exception erro)
            {
                String msg1 = "<script> ShowMsg('Atenção:','" + erro.Message + "'); </script>";
                PlaceHolder1.Controls.Add(new LiteralControl(msg1));
            }
            AtualizaGrid();
        }

        protected void gvDados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String caminho = Server.MapPath(@"IMAGENS\AUTORES\");
            int index = Convert.ToInt32(e.RowIndex);
            int cod = Convert.ToInt32(gvDados.Rows[index].Cells[2].Text);
            DALAutor dal = new DALAutor();
            //verificar se existe foto existe e deletar
            ModeloAutor aold = dal.GetRegistro(cod);
            if (aold.Foto != "")
            {
                File.Delete(caminho + aold.Foto);
            }
            dal.Excluir(cod);
            this.LimparCampos();
            AtualizaGrid();  
        }

        protected void gvDados_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int index = gvDados.SelectedIndex;
                int cod = Convert.ToInt32(gvDados.Rows[index].Cells[2].Text);
                DALAutor dal = new DALAutor();
                ModeloAutor a = dal.GetRegistro(cod);
                if (a.Id != 0)
                {
                    txtId.Text = a.Id.ToString();
                    txtNome.Text = a.Nome;

                    btnSalvar.Text = "Alterar";
                }
            }
            catch
            {

            }



        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        
    }
}
