﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFrases.DAL;
using WebFrases.MODELO;

namespace WebFrases
{
    public partial class Categoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AtualizaGrid();
        }
        public void AtualizaGrid()
        {
            DALCategoria dal = new DALCategoria();
            gvDados.DataSource = dal.Localizar();
            gvDados.DataBind();
        }

        private void LimparCampos()
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
                DALCategoria dal = new DALCategoria();
                ModeloCategoria obj = new ModeloCategoria();
                obj.Nome = txtNome.Text;

                if (btnSalvar.Text == "Inserir")
                {
                    dal.Inserir(obj);
                    msg = "<script> ShowMsg('Atenção:','O código gerado foi: " + obj.Id.ToString() + "'); </script>";
                }
                else
                {
                    //alterar
                    obj.Id = Convert.ToInt32(txtId.Text);
                    dal.Alterar(obj);
                    msg = "<script> ShowMsg('Atenção:','Registro alterado corretamente!!!'); </script>";
                }
                //Response.Write(msg);
                PlaceHolder1.Controls.Add(new LiteralControl(msg));
                this.LimparCampos();
            }

            catch (Exception erro)
            {
                String msg1="<script> ShowMsg('Atenção:','" + erro.Message + "'); </script>";
                PlaceHolder1.Controls.Add(new LiteralControl(msg1));
            }
            AtualizaGrid();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.LimparCampos();
        }

        protected void gvDados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            int cod = Convert.ToInt32(gvDados.Rows[index].Cells[2].Text);
            DALCategoria dal = new DALCategoria();
            dal.Excluir(cod);
            AtualizaGrid();
            LimparCampos();
        }

        protected void gvDados_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int index = gvDados.SelectedIndex;
                int cod = Convert.ToInt32(gvDados.Rows[index].Cells[2].Text);
                DALCategoria dal = new DALCategoria();
                ModeloCategoria c = dal.GetRegistro(cod);
                if (c.Id != 0)
                {
                    txtId.Text = c.Id.ToString();
                    txtNome.Text = c.Nome;
                    
                    btnSalvar.Text = "Alterar";
                }
            }
            catch 
            {

            }
           
        }
    }
}