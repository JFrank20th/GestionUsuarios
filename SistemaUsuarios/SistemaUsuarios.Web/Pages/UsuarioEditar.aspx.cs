using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaUsuarios.Web.Pages
{
    public partial class UsuarioEditar : System.Web.UI.Page
    {
        private string apiBaseUrl = "https://localhost:44386/"; 

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                await CargarUsuario(id);
            }
        }

        private async Task CargarUsuario(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                HttpResponseMessage response = await client.GetAsync("api/usuario/consultar");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var usuarios = JsonConvert.DeserializeObject<List<Models.Usuario>>(json);
                    var usuario = usuarios.Find(u => u.Id == id);

                    if (usuario != null)
                    {
                        lblIdValor.Text = usuario.Id.ToString();
                        txtNombre.Text = usuario.Nombre;
                        txtFechaNacimiento.Text = usuario.FechaNacimiento.ToString("yyyy-MM-dd");
                        ddlSexo.SelectedValue = usuario.Sexo;
                    }
                }
            }
        }

        protected async void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            Models.Usuario usuario = new Models.Usuario
            {
                Id = int.Parse(lblIdValor.Text),
                Nombre = txtNombre.Text,
                FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text),
                Sexo = ddlSexo.SelectedValue
            };

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                string json = JsonConvert.SerializeObject(usuario);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync("api/usuario/modificar", content);
                if (response.IsSuccessStatusCode)
                {
                    lblMensaje.Text = "Usuario actualizado correctamente.";
                    lblMensaje.CssClass = "text-success";
                }
                else
                {
                    lblMensaje.Text = "Error al actualizar usuario.";
                    lblMensaje.CssClass = "text-danger";
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