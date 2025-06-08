using System;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using Newtonsoft.Json;
using SistemaUsuarios.Web.Models;

namespace SistemaUsuarios.Web.Pages
{
    public partial class Usuario : Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            Models.Usuario usuario = new Models.Usuario
            {
                Nombre = txtNombre.Text,
                FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text),
                Sexo = ddlSexo.SelectedValue
            };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44386/");

                string json = JsonConvert.SerializeObject(usuario);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/usuario/agregar", content);
                if (response.IsSuccessStatusCode)
                {
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Text = "Usuario guardado correctamente.";

                    string script = @"
                            if(confirm('¿Desea agregar otro usuario?')) {
                                // Limpiar campos
                                document.getElementById('" + txtNombre.ClientID + @"').value = '';
                                document.getElementById('" + txtFechaNacimiento.ClientID + @"').value = '';
                                document.getElementById('" + ddlSexo.ClientID + @"').selectedIndex = 0;
                                // Limpiar mensaje
                                document.getElementById('" + lblMensaje.ClientID + @"').innerText = '';
                            } else {
                                window.location.href = 'UsuarioConsulta.aspx';
                            }
                        ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "guardadoExitoso", script, true);
                }
                else
                {
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Text = "Error al guardar el usuario.";
                }
            }


        }

        private bool ValidarCampos()
        {
            lblMensaje.CssClass = "text-danger";

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                lblMensaje.Text = "El nombre es requerido.";
                return false;
            }

            if (string.IsNullOrEmpty(txtFechaNacimiento.Text))
            {
                lblMensaje.Text = "La fecha de nacimiento es requerida.";
                return false;
            }

            if (string.IsNullOrEmpty(ddlSexo.SelectedValue))
            {
                lblMensaje.Text = "Debe seleccionar un sexo.";
                return false;
            }

            return true;
        }
    }
}
