﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebFrases.MODELO;

namespace WebFrases.DAL
{
    public class DALFrase
    {
        private System.Configuration.ConnectionStringSettings connString;

        public DALFrase()
        {
            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            connString = rootWebConfig.ConnectionStrings.ConnectionStrings["sisFrasesConnectionString"];
        }
        public void Inserir(ModeloFrase obj)
        {
            //cria um objeto de conexão
            SqlConnection con = new SqlConnection();
            con.ConnectionString = this.connString.ToString();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = con;
                cmd.CommandText = "Insert into frases (frase,autor,categoria) values (@frase,@autor,@categoria);select @@IDENTITY;";
                cmd.Parameters.AddWithValue("frase", obj.Texto);
                cmd.Parameters.AddWithValue("autor", obj.Autor);
                cmd.Parameters.AddWithValue("categoria", obj.Categoria);
                con.Open();
                obj.Id = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void Alterar(ModeloFrase obj)
        {
            //cria um objeto de conexão
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connString.ToString();
            SqlCommand cmd = new SqlCommand();

            try
            {


                cmd.Connection = con;
                cmd.CommandText = "update frases set frase=@frase,autor=@autor,categoria=@categoria where id = @id;";
                cmd.Parameters.AddWithValue("frase", obj.Texto);
                cmd.Parameters.AddWithValue("autor", obj.Autor);
                cmd.Parameters.AddWithValue("categoria", obj.Categoria);
                cmd.Parameters.AddWithValue("id", obj.Id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void Excluir(int cod)
        {
            //cria um objeto de conexão
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connString.ToString();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandText = "delete from frases where id = " + cod.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable Localizar()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select f.id,f.frase,f.autor,f.categoria, a.nome,c.categoria as nomeCategoria from frases f inner join autores a on(f.autor=a.id)inner join categorias c on(f.categoria=c.id)", connString.ConnectionString);
            try
            {
                da.Fill(tabela);
                return tabela;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }

        public DataTable Localizar(String valor)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from frases where frase like '%" +
                valor + "%'", connString.ConnectionString);
            try
            {
                da.Fill(tabela);
                return tabela;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

        }

        public ModeloFrase GetRegistro(int cod)
        {
            ModeloFrase obj = new ModeloFrase();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connString.ToString();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            try
            {
                cmd.CommandText = "select * from frases where id = @id";
                cmd.Parameters.AddWithValue("@id", cod);
                con.Open();
                SqlDataReader registro = cmd.ExecuteReader();
                if (registro.HasRows)
                {
                    registro.Read();
                    obj.Id = Convert.ToInt32(registro["id"]);
                    obj.Texto = Convert.ToString(registro["frase"]);
                    obj.Autor = Convert.ToInt32(registro["autor"]);
                    obj.Categoria = Convert.ToInt32(registro["categoria"]);
                }
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            con.Close();
            return obj;
        }

    }
}